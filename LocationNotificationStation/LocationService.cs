using CommunityToolkit.Mvvm.Messaging;

namespace LocationNotificationStation;

public class LocationService
{
    private readonly bool stopping = false;
    private readonly IMessenger messenger;

    public LocationService(IMessenger messenger)
    {
        this.messenger = messenger;
    }

    public async Task Run(CancellationToken token)
    {
        await Task.Run(async () =>
        {
            while (!stopping)
            {
                token.ThrowIfCancellationRequested();
                try
                {
                    await Task.Delay(2000);

                    var request = new GeolocationRequest(GeolocationAccuracy.High);
                    var location = await Geolocation.GetLocationAsync(request);
                    if (location != null)
                    {
                        var message = new Messages.LocationMessage(new(location.Latitude, location.Longitude));

                        this.messenger.Send(message);
                    }
                }
                catch (Exception ex)
                {
                    var errormessage = new Messages.LocationErrorMessage();
                    this.messenger.Send(errormessage);
                }
            }
            return;
        }, token);
    }
}
