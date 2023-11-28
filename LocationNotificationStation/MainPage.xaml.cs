﻿using CommunityToolkit.Mvvm.Input;
using LocationNotificationStation.ViewModels;

namespace LocationNotificationStation;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel vm)
	{
		InitializeComponent();

        this.BindingContext = vm;
	}
}

