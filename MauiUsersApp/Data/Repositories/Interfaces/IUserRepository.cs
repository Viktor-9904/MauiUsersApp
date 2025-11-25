using MauiUsersApp.Data.Models;
using MauiUsersApp.ViewModels;

namespace MauiUsersApp.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> LoginUser(string username, string password);
        Task AddUserAsync(UserViewModel user);
        Task EditUserAsync(UserViewModel user);
        Task ChangeUserActiveStatusByIdAsync(int userId);
        Task SaveChangesAsync();
    }
}
