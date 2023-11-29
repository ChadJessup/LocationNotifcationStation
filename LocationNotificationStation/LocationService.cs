﻿using CommunityToolkit.Mvvm.Messaging;

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
                        var message = new LocationMessage(new(location.Latitude, location.Longitude));



                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            this.messenger.Send(message, "Location");
                        });
                    }
                }
                catch (Exception ex)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        var errormessage = new LocationErrorMessage();
                        this.messenger.Send(errormessage, "LocationError");
                    });
                }
            }
            return;
        }, token);
    }
}
