using MauiUsersApp.ViewModels;

namespace MauiUsersApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
        Task<bool> LoginAsync(string email, string password);
        Task ChangeActiveStatusByUserIdAsync(int userId, bool isActive);
        Task<UserViewModel> SaveUserChangesAsync(UserViewModel user);
    }
}
