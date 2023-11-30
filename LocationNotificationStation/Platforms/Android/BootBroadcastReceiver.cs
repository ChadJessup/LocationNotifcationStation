using Android.App;
using Android.Content;

namespace LocationNotificationStation;

[BroadcastReceiver(Name = BootBroadcastReceiver.Name, Enabled = true, DirectBootAware = true, Exported = true)]
[IntentFilter([Intent.ActionBootCompleted])]
public class BootBroadcastReceiver : BroadcastReceiver
{
    // Value in AndroidManifest.xml has to match.
    public const string Name = "com.goodenough.LocationNotificationStation.BootBroadcastReceiver";

    public override void OnReceive(Context? context, Intent? intent)
    {
        if (intent?.Action?.Equals(Intent.ActionBootCompleted) ?? false || context is not null)
        {
            Intent main = new(context!, typeof(MainActivity));
            main.AddFlags(ActivityFlags.NewTask);
            context!.StartActivity(main);
        }
    }
}
