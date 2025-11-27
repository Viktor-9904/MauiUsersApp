using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Windows.Input;

using MauiUsersApp.Services.Interfaces;

namespace MauiUsersApp.ViewModels
{
    public class UsersPageViewModel : INotifyPropertyChanged
    {
        private readonly IUserService userService;

        public UsersPageViewModel(IUserService userService)
        {
            this.userService = userService;

            RefreshCommand = new Command(async () => await LoadUsersAsync());
            EditUserCommand = new Command(async () => await EditUserAsync());

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
        public ICommand EditUserCommand { get; }

        private async Task LoadUsersAsync()
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

        private async Task EditUserAsync()
        {
            await Shell.Current.GoToAsync("//EditUserPage");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
