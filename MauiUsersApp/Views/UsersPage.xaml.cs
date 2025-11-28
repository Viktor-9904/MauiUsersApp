using MauiUsersApp.ViewModels;

namespace MauiUsersApp.Views;

public partial class UsersPage : ContentPage
{
	public UsersPage(UsersPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is UsersPageViewModel vm)
        {
            _ = vm.LoadUsersAsync();
        }
    }
}