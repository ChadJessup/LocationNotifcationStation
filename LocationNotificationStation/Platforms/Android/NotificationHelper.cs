using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;

namespace LocationNotificationStation;

public class NotificationHelper
{
    private static readonly string foregroundChannelId = "9001";
    private static readonly Context context = Android.App.Application.Context;

    public Notification GetServiceStartedNotification()
    {
        var intent = new Intent(context, typeof(MainActivity));
        intent.AddFlags(ActivityFlags.SingleTop);
        intent.PutExtra("Title", "Message");

        var pendingIntent = PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.UpdateCurrent);

        var notificationBuilder = new NotificationCompat.Builder(context, foregroundChannelId)
            .SetContentTitle("Location Notification Station")
            .SetContentText("Your location is being tracked")
       //     .SetSmallIcon(Resource.Drawable.location)
            .SetOngoing(true)
            .SetContentIntent(pendingIntent);

        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
#pragma warning disable CA1416 // Validate platform compatibility
            NotificationChannel notificationChannel = new(foregroundChannelId, "Title", NotificationImportance.High)
            {
                Importance = NotificationImportance.High
            };

            notificationChannel.EnableLights(false);
            notificationChannel.EnableVibration(false);
            notificationChannel.SetShowBadge(false);
            //notificationChannel.SetVibrationPattern([100, 200, 300]);

            if (context.GetSystemService(Context.NotificationService) is NotificationManager notificationManager)
            {
                notificationBuilder.SetChannelId(foregroundChannelId);
                notificationManager.CreateNotificationChannel(notificationChannel);
            }
        }
#pragma warning restore CA1416 // Validate platform compatibility

        return notificationBuilder.Build();
    }
}
