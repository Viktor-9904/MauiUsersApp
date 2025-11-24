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
            IsEmailErrorMessageVisible = false;
            OnPropertyChanged(nameof(IsEmailErrorMessageVisible));
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
            IsPasswordErrorMessageVisible = false;
            OnPropertyChanged(nameof(IsPasswordErrorMessageVisible));
        }
    }

    public bool IsInputValid =>
        !string.IsNullOrWhiteSpace(Email) && IsValidEmail(Email) &&
        !string.IsNullOrWhiteSpace(Password);

    private string emailErrorMessage;

    public string EmailErrorMessage
    {
        get { return emailErrorMessage; }
        set
        {
            emailErrorMessage = value;
            OnPropertyChanged(nameof(EmailErrorMessage));
        }
    }

    private bool isEmailErrorMessageVisible;

    public bool IsEmailErrorMessageVisible
    {
        get { return isEmailErrorMessageVisible; }
        set
        {
            isEmailErrorMessageVisible = value;
            OnPropertyChanged(nameof(IsEmailErrorMessageVisible));
        }
    }

    private bool isPasswordErrorMessageVisible;

    public bool IsPasswordErrorMessageVisible
    {
        get { return isPasswordErrorMessageVisible; }
        set
        {
            isPasswordErrorMessageVisible = value;
            OnPropertyChanged(nameof(IsPasswordErrorMessageVisible));
        }
    }

    private string passwordErrorMessage;

    public string PasswordErrorMessage
    {
        get { return passwordErrorMessage; }
        set
        {
            passwordErrorMessage = value;
            OnPropertyChanged(nameof(PasswordErrorMessage));
        }
    }

    public LoginPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private async void Login_Clicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.DisplayAlert("Info", $"Login Button Clicked.\nEmail = {Email}\nPassword={Password}", "OK");
    }

    private void EmailEntry_Unfocused(object sender, FocusEventArgs e)
    {
        if (string.IsNullOrEmpty(Email))
        {
            EmailErrorMessage = "Email is Required!";
            IsEmailErrorMessageVisible = true;
            return;
        }

        if (!IsValidEmail(Email))
        {
            EmailErrorMessage = "Invalid Email!";
            IsEmailErrorMessageVisible = true;
            return;
        }

        EmailErrorMessage = string.Empty;
        IsEmailErrorMessageVisible = false;
    }

    private void PasswordEntry_Unfocused(object sender, FocusEventArgs e)
    {
        if (string.IsNullOrEmpty(Password))
        {
            PasswordErrorMessage = "Password is Required!";
            IsPasswordErrorMessageVisible = true;
            return;
        }

        PasswordErrorMessage = string.Empty;
        IsPasswordErrorMessageVisible = false;
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