using MauiUsersApp.ViewModels;

namespace MauiUsersApp.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }    
}