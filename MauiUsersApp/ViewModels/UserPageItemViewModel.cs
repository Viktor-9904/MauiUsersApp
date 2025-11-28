using MauiUsersApp.Services.Interfaces;
using MauiUsersApp.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace MauiUsersApp.ViewModels
{
    public class UserPageItemViewModel : INotifyPropertyChanged
    {
        private readonly IUserService userService;

        public UserPageItemViewModel(IUserService userService, bool isActive)
        {
            this.userService = userService;
            this.isActive = isActive;

            EditUserCommand = new Command(async () => OnEditUser());
        }

        private int id;
        private string fullname;
        private string email;
        private string password;
        private bool isActive;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        public string FullName
        {
            get { return fullname; }
            set
            {
                fullname = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }


        public bool IsActive
        {
            get { return isActive; }
            set
            {
                if (isActive == value)
                {
                    return;
                }

                isActive = value;
                OnPropertyChanged();
                _ = SaveActiveStatus();
            }
        }

        public ICommand EditUserCommand { get; }

        public async Task OnEditUser()
        {
            ModifyUserViewModel user = new ModifyUserViewModel
            {
                Id = Id,
                FullName = FullName,
                Email = Email,
                Password = Password,
                IsActive = IsActive,
                IsNew = false,
            };

            await Shell.Current.Navigation.PushAsync(new ModifyUserPage(user, userService));
        }

        public async Task SaveActiveStatus()
        {
            await this.userService.ChangeActiveStatusByUserIdAsync(Id, IsActive);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

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
