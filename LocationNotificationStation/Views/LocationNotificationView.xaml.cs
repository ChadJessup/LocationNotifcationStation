using CommunityToolkit.Mvvm.ComponentModel;
using LocationNotificationStation.Models;
using LocationNotificationStation.ViewModels;

namespace LocationNotificationStation.Views;

[ContentProperty(nameof(Content))]
public partial class LocationNotificationView : ContentView
{
    //public static readonly BindableProperty NotificationProperty = BindableProperty.Create(
    //    nameof(Notification),
    //    typeof(LocationNotification),
    //    typeof(LocationNotificationView));
    //
    public LocationNotificationView()
    {
        InitializeComponent();
    }

//    public LocationNotification Notification
//    { 
//        get => (LocationNotification)GetValue(NotificationProperty);
//        set => SetValue(NotificationProperty, value);
//    }

    protected override void OnParentSet()
    {
        base.OnParentSet();
    }
}