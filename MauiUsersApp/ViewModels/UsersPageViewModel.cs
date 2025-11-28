using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Windows.Input;

using MauiUsersApp.Services.Interfaces;
using MauiUsersApp.Views;

namespace MauiUsersApp.ViewModels
{
    public class UsersPageViewModel : INotifyPropertyChanged
    {
        private readonly IUserService userService;

        public UsersPageViewModel(IUserService userService)
        {
            this.userService = userService;

            RefreshCommand = new Command(async () => await LoadUsersAsync());
            CreateUserCommand = new Command(async () => await CreateUser());

            Users = new ObservableCollection<UserPageItemViewModel>();

            _ = LoadUsersAsync();
        }

        public ObservableCollection<UserPageItemViewModel> Users { get; }

        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshCommand { get; }
        public ICommand CreateUserCommand { get; }

        public async Task LoadUsersAsync()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;
            Users.Clear();

            IEnumerable<UserViewModel> users = await this.userService.GetAllUsersAsync();

            foreach (var user in users)
            {
                Users.Add(new UserPageItemViewModel(userService, user.IsActive)
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Password = user.Password,
                });
            }

            IsBusy = false;
        }

        private async Task CreateUser()
        {
            ModifyUserViewModel user = new ModifyUserViewModel
            {
                IsNew = true,
            };

            await Shell.Current.Navigation.PushAsync(new ModifyUserPage(user, userService));

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
