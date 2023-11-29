using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationNotificationStation;

// https://github.com/jfversluis/XFBackgroundLocationSample/blob/main/XFBackgroundLocationSample.Android/AndroidLocationService.cs

[Service]
public class AndroidLocationService : Service
{
    public const int SERVICE_RUNNING_NOTIFICATION_ID = 10001;
    private CancellationTokenSource _cts = new CancellationTokenSource();

    public override IBinder? OnBind(Intent? intent)
    {
        return null;
    }

    public override StartCommandResult OnStartCommand(Intent? intent, [GeneratedEnum] StartCommandFlags flags, int startId)
    {
        Notification notification = new NotificationHelper().GetServiceStartedNotification();
        StartForeground(SERVICE_RUNNING_NOTIFICATION_ID, notification);


        Task.Run(() =>
        {
            try
            {
                var locationService = new LocationService(WeakReferenceMessenger.Default);
                locationService.Run(_cts.Token).Wait();
            }
            catch (Android.OS.OperationCanceledException)
            {
            }
            finally
            {
                if (_cts.IsCancellationRequested)
                {
                    var message = new StopServiceMessage();
                    MainThread.BeginInvokeOnMainThread(
                        () => WeakReferenceMessenger.Default.Send(message, "ServiceStopped")
                    );
                }
            }
        }, _cts.Token);

        return StartCommandResult.Sticky;
    }


    public override void OnDestroy()
    {
        if (_cts != null)
        {
            _cts.Token.ThrowIfCancellationRequested();
            _cts.Cancel();
        }

        base.OnDestroy();
    }
}
