using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace MauiUsersApp.Views;

public partial class LoginPage : ContentPage
{
    private string email;

    public string Email
    {
        get { return email; }
        set
        {
            email = value;
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(IsInputValid));
        }
    }

    private string password;

    public string Password
    {
        get { return password; }
        set
        {
            password = value;
            OnPropertyChanged(nameof(Password));
            OnPropertyChanged(nameof(IsInputValid));
        }
    }

    public bool IsInputValid =>
        !string.IsNullOrWhiteSpace(Email) && IsValidEmail(Email) &&
        !string.IsNullOrWhiteSpace(Password);

    public LoginPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private async void Login_Clicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.DisplayAlert("Info", $"Login Button Clicked.\nEmail = {Email}\nPassword={Password}", "OK");
    }

    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
    }
}