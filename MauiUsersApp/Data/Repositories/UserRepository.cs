using Dapper;
using MauiUsersApp.Data.Models;
using MauiUsersApp.Data.Repositories.Interfaces;
using MauiUsersApp.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.Maui.ApplicationModel.Communication;

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

        public async Task ChangeUserActiveStatusByIdAsync(int userId, bool isActive)
        {
            using var connection = new SqliteConnection($"Data Source={dbPath}");
            await connection.OpenAsync();

            const string query = @"
                UPDATE Users 
                SET IsActive = @IsActive 
                WHERE Id = @Id
            ";

            await connection.ExecuteAsync(query, new { Id = userId, IsActive = isActive });
        }

        public Task EditUserAsync(UserViewModel user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> LoginUser(string email, string password)
        {
            using var connection = new SqliteConnection($"Data Source={dbPath}");
            await connection.OpenAsync();

            const string query = @"
                SELECT COUNT(1)
                FROM Users
                WHERE Email = @Email AND Password = @Password;
            ";

            int count = await connection
                .ExecuteScalarAsync<int>(query, new { Email = email, Password = password });

            return count > 0;
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
