using Microsoft.Data.SqlClient;
using MauiUsersApp.ViewModels;

using Dapper;
using MauiUsersApp.Data.Repositories.Interfaces;
using MauiUsersApp.Data.Models;
using Microsoft.Data.Sqlite;

namespace MauiUsersApp.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string dbPath;

        public UserRepository(string dbPath)
        {
            this.dbPath = dbPath;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            using var connection = new SqliteConnection($"Data Source={dbPath}");
            await connection.OpenAsync();

            const string query = "SELECT * FROM Users;";
            return await connection.QueryAsync<User>(query);
        }

        public Task AddUserAsync(UserViewModel user)
        {
            throw new NotImplementedException();
        }

        public Task ChangeUserActiveStatusByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task EditUserAsync(UserViewModel user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LoginUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
