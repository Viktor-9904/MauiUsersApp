using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiUsersApp.ViewModels
{
    public partial class ModifyUserViewModel : ObservableObject
    {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private string fullName = string.Empty;

        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private bool isActive;

        [ObservableProperty]
        private bool isNew;
    }
}