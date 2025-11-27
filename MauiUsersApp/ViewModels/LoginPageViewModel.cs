using MauiUsersApp.Services.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace MauiUsersApp.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        private readonly IUserService userService;  
        public LoginPageViewModel(IUserService userService)
        {
            this.userService = userService;

            EmailUnfocusedCommand = new Command(OnEmailUnfocused);
            PasswordUnfocusedCommand = new Command(OnPasswordUnfocused);
            LoginCommand = new Command(OnLogin);
        }

        private string email;
        private string password;
        private string emailErrorMessage;
        private string passwordErrorMessage;

        private bool isEmailErrorMessageVisible;
        private bool isPasswordErrorMessageVisible;

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

        public string EmailErrorMessage
        {
            get { return emailErrorMessage; }
            set
            {
                emailErrorMessage = value;
                OnPropertyChanged(nameof(EmailErrorMessage));
            }
        }

        public bool IsEmailErrorMessageVisible
        {
            get { return isEmailErrorMessageVisible; }
            set
            {
                isEmailErrorMessageVisible = value;
                OnPropertyChanged(nameof(IsEmailErrorMessageVisible));
            }
        }

        public bool IsPasswordErrorMessageVisible
        {
            get { return isPasswordErrorMessageVisible; }
            set
            {
                isPasswordErrorMessageVisible = value;
                OnPropertyChanged(nameof(IsPasswordErrorMessageVisible));
            }
        }

        public string PasswordErrorMessage
        {
            get { return passwordErrorMessage; }
            set
            {
                passwordErrorMessage = value;
                OnPropertyChanged(nameof(PasswordErrorMessage));
            }
        }

        public bool IsInputValid =>
            !string.IsNullOrWhiteSpace(Email) && IsValidEmail(Email) &&
            !string.IsNullOrWhiteSpace(Password);

        public ICommand EmailUnfocusedCommand { get; }
        public ICommand PasswordUnfocusedCommand { get; }
        public ICommand LoginCommand { get; }

        private void OnEmailUnfocused()
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

        private void OnPasswordUnfocused()
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

        private async void OnLogin()
        {
            bool loginSuccess = await this.userService.LoginAsync(Email, Password);
            if (loginSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Info", "Logged in successully!", "OK");
                await Shell.Current.GoToAsync("//UsersPage");
            }
            else
            {
                EmailErrorMessage = "Invalid Email or Password!";
                PasswordErrorMessage = "Invalid Email or Password!";

                IsEmailErrorMessageVisible = true;
                IsPasswordErrorMessageVisible = true;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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
}
