using System.Text.RegularExpressions;
using System.Windows.Input;

using MauiUsersApp.Services.Interfaces;

using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiUsersApp.ViewModels
{
    [QueryProperty(nameof(SelectedUser), "SelectedUser")]
    public partial class ModifyUserPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ModifyUserViewModel selectedUser;
        private readonly IUserService userService;

        public ModifyUserPageViewModel(
            ModifyUserViewModel user,
            IUserService userService)
        {
            SelectedUser = user;

            this.userService = userService;

            SaveCommand = new Command(async () => await OnSave());
            CancelCommand = new Command(async () => await OnCancel());
        }

        public string PageTitle => selectedUser.IsNew ? "Add User" : "Edit User";

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        private async Task OnSave()
        {
            if (string.IsNullOrWhiteSpace(selectedUser.FullName) ||
                string.IsNullOrWhiteSpace(selectedUser.Email) ||
                string.IsNullOrWhiteSpace(selectedUser.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Please fill in all required fields!",
                    "OK");
                return;
            }

            if (!IsValidEmail(selectedUser.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Please enter a valid email address!",
                    "OK");
                return;
            }

            UserViewModel newUser = new UserViewModel()
            {
                FullName = selectedUser.FullName,
                Email = selectedUser.Email,
                Password = selectedUser.Password,
                IsActive = selectedUser.IsActive
            };

            await this.userService.AddUserAsync(newUser);

            await Application.Current.MainPage.DisplayAlert("Saved", $"User {SelectedUser.FullName} saved!", "OK");
            await Shell.Current.GoToAsync("..");
        }

        private async Task OnCancel()
        {
            await Shell.Current.GoToAsync("..");
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
