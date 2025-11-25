using MauiUsersApp.ViewModels;

namespace MauiUsersApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
        Task<bool> LoginAsync(string email, string password);
        Task ChangeActiveStatusByUserIdAsync(int userId);
        Task<UserViewModel> SaveUserChangesAsync(UserViewModel user);
    }
}
