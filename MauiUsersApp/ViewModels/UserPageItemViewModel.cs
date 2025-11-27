using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using MauiUsersApp.Services.Interfaces;

namespace MauiUsersApp.ViewModels
{
    public class UserPageItemViewModel : INotifyPropertyChanged
    {
        private readonly IUserService userService;

        public UserPageItemViewModel(IUserService userService, bool isActive)
        {
            this.userService = userService;
            this.isActive = isActive;
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

        public async Task SaveActiveStatus()
        {
            await this.userService.ChangeActiveStatusByUserIdAsync(Id, IsActive);
        }

        public ICommand ToggleActiveStatusCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
