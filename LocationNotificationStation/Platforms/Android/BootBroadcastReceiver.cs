using Android.App;
using Android.Content;

namespace LocationNotificationStation;

[BroadcastReceiver(Name = "com.goodenough.BootBroadcastReceiver", Enabled = true, DirectBootAware = true, Exported = true)]
[IntentFilter([Intent.ActionBootCompleted])]
public class BootBroadcastReceiver : BroadcastReceiver
{
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
