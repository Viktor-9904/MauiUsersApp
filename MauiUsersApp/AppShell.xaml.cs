using MauiUsersApp.Views;

namespace MauiUsersApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ModifyUserPage), typeof(ModifyUserPage));
        }
    }
}
