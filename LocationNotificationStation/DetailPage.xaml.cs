using LocationNotificationStation.ViewModels;

namespace LocationNotificationStation;

public partial class DetailPage : ContentPage
{
	public DetailPage(DetailPageViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}