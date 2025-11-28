using MauiUsersApp.Services.Interfaces;
using MauiUsersApp.ViewModels;

namespace MauiUsersApp.Views;

public partial class ModifyUserPage : ContentPage
{
	private readonly IUserService userService;
	public ModifyUserPage(
		ModifyUserViewModel user,
		IUserService userService)
	{
		InitializeComponent();
        BindingContext = new ModifyUserPageViewModel(user, userService);
    }
}