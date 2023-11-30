using LocationNotificationStation.ViewModels;

namespace LocationNotificationStation;

public partial class MainPage : ContentPage
{
    private readonly MainPageViewModel vm;

    public MainPage(
        MainPageViewModel vm)
    {
        InitializeComponent();

        this.BindingContext = vm;
        this.vm = vm;
        vm.GetLocationsCommand.Execute(this);
    }

    protected override async void OnAppearing()
    {
        await this.vm.CheckPermissions();
    }
}
