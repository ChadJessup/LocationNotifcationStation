using CommunityToolkit.Mvvm.Messaging;

namespace LocationNotificationStation;

public class LocationService
{
    private readonly bool stopping = false;
    private readonly IMessenger messenger;

    private Location? lastLocation;

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

                    if (ShouldGetUpdatedLocation(lastLocation))
                    {
                        var request = new GeolocationRequest(GeolocationAccuracy.Best);
                        var location = await Geolocation.GetLocationAsync(request);
                        if (location is not null)
                        {
                            var message = new LocationMessage(new(location.Latitude, location.Longitude));

                            this.messenger.Send(message);
                            lastLocation = location;
                        }
                    }
                }
                catch (Exception ex)
                {
                    var errormessage = new LocationErrorMessage();
                    this.messenger.Send(errormessage);
                }
            }
            return;
        }, token);
    }

    private bool ShouldGetUpdatedLocation(Location? lastLocation)
    {
        if (lastLocation is null)
        {
            return true;
        }

        // TODO: only check for location if last one is a certain age

        return true;
    }
}
