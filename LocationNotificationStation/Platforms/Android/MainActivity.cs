using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using CommunityToolkit.Mvvm.Messaging;

namespace LocationNotificationStation;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    private readonly IMessenger messenger;
    private Intent? serviceIntent;
    private const int RequestCode = 5469;

    public MainActivity()
    {
        this.messenger = WeakReferenceMessenger.Default;
    }

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        serviceIntent = new Intent(this, typeof(AndroidLocationService));
        SetServiceMethods();

        if (Build.VERSION.SdkInt >= BuildVersionCodes.M && !Android.Provider.Settings.CanDrawOverlays(this))
        {
//            var intent = new Intent(Android.Provider.Settings.ActionManageOverlayPermission);
//            intent.SetFlags(ActivityFlags.NewTask);
//            this.StartActivity(intent);
        }
    }

    protected override void OnActivityResult(int requestCode, Result resultCode, Intent? data)
    {
        if (requestCode == RequestCode)
        {
            if (Android.Provider.Settings.CanDrawOverlays(this))
            {

            }
        }

        base.OnActivityResult(requestCode, resultCode, data);
    }


    private void SetServiceMethods()
    {
        this.messenger.Register<MainActivity, Messages.StartServiceMessage>(
            this, (r, message) =>
            {
                if (!IsServiceRunning(typeof(AndroidLocationService)))
                {
                    if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
                    {
                        StartForegroundService(serviceIntent);
                    }
                    else
                    {
                        StartService(serviceIntent);
                    }
                }
            });

        this.messenger.Register<MainActivity, Messages.StopServiceMessage>(
            this, (r, message) =>
        {
            if (IsServiceRunning(typeof(AndroidLocationService)))
            {
                StopService(serviceIntent);
            }
        });
    }

    private bool IsServiceRunning(System.Type cls)
    {
        ActivityManager? manager = (ActivityManager?)GetSystemService(Context.ActivityService);
        foreach (var service in manager?.GetRunningServices(int.MaxValue) ?? [])
        {
            if (service?.Service?.ClassName.Equals(Java.Lang.Class.FromType(cls).CanonicalName) ?? false)
            {
                return true;
            }
        }

        return false;
    }

}
