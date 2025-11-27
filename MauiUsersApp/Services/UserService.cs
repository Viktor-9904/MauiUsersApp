using MauiUsersApp.Data.Models;
using MauiUsersApp.Data.Repositories.Interfaces;
using MauiUsersApp.Services.Interfaces;
using MauiUsersApp.ViewModels;

namespace MauiUsersApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task ChangeActiveStatusByUserIdAsync(int userId, bool isActive)
        {
            await this.userRepository.ChangeUserActiveStatusByIdAsync(userId, isActive);
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            IEnumerable<User> users = await this.userRepository.GetAllUsersAsync();

            IEnumerable<UserViewModel> result = users
                .Select(u => new UserViewModel()
                {
                    Id = u.Id,
                    FullName = u.Name,
                    Email = u.Email,
                    Password = u.Password,
                    IsActive = u.IsActive,
                })
                .ToList();

            return result;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            return await this.userRepository.LoginUser(email, password);
        }

        public Task<UserViewModel> SaveUserChangesAsync(UserViewModel user)
        {
            throw new NotImplementedException();
        }
    }
}
