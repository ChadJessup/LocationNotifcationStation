using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.Core.App;

namespace LocationNotificationStation;

public class NotificationHelper
{
    private static string foregroundChannelId = "9001";
    private static Context context = global::Android.App.Application.Context;

    public Notification GetServiceStartedNotification()
    {
        var intent = new Intent(context, typeof(MainActivity));
        intent.AddFlags(ActivityFlags.SingleTop);
        intent.PutExtra("Title", "Message");

        var pendingIntent = PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.UpdateCurrent);

        var notificationBuilder = new NotificationCompat.Builder(context, foregroundChannelId)
            .SetContentTitle("Xamarin.Forms Background Tracking Example")
            .SetContentText("Your location is being tracked")
       //     .SetSmallIcon(Resource.Drawable.location)
            .SetOngoing(true)
            .SetContentIntent(pendingIntent);

        if (global::Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            NotificationChannel notificationChannel = new NotificationChannel(foregroundChannelId, "Title", NotificationImportance.High);
            notificationChannel.Importance = NotificationImportance.High;
            notificationChannel.EnableLights(true);
            notificationChannel.EnableVibration(true);
            notificationChannel.SetShowBadge(true);
            notificationChannel.SetVibrationPattern(new long[] { 100, 200, 300 });

            if (context.GetSystemService(Context.NotificationService) is NotificationManager notificationManager)
            {
                notificationBuilder.SetChannelId(foregroundChannelId);
                notificationManager.CreateNotificationChannel(notificationChannel);
            }
        }

        return notificationBuilder.Build();
    }
}
