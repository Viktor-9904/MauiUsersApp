using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;

using MauiUsersApp.Data.Repositories;
using MauiUsersApp.Data.Repositories.Interfaces;
using MauiUsersApp.Services;
using MauiUsersApp.Services.Interfaces;
using MauiUsersApp.ViewModels;
using MauiUsersApp.Views;

using CommunityToolkit.Maui;
using Dapper;

namespace MauiUsersApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            DatabaseInitializer.InitializeDatabaseAsync().Wait();

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "UserManagementSystem.db");
            builder.Services.AddSingleton<IUserRepository>(provider => new UserRepository(dbPath));
            builder.Services.AddSingleton<IUserService, UserService>();

            builder.Services.AddTransient<LoginPageViewModel>();
            builder.Services.AddTransient<LoginPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static class DatabaseInitializer
        {
            public static async Task InitializeDatabaseAsync()
            {
                string dbFileName = "UserManagementSystem.db";
                string dbPath = Path.Combine(FileSystem.AppDataDirectory, dbFileName);

                using var connection = new SqliteConnection($"Data Source={dbPath}");
                await connection.OpenAsync();

                string createTableSql = @"
                    DROP TABLE IF EXISTS Users;

                    CREATE TABLE IF NOT EXISTS Users (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Email TEXT NOT NULL,
                        Password TEXT,
                        IsActive INTEGER NOT NULL);
                ";

                await connection.ExecuteAsync(createTableSql);

                int count = await connection.ExecuteScalarAsync<int>("SELECT COUNT(1) FROM Users;");
                if (count == 0)
                {
                    string insertSql = @"
                        INSERT INTO Users (Name, Email, Password, IsActive) 
                        VALUES
                            ('Leanne Graham', 'Sincere@april.biz', 'password123', 1),
                            ('Ervin Howell', 'Shanna@melissa.tv', 'password123', 0),
                            ('Clementine Bauch', 'Nathan@yesenia.net', 'password123', 1),
                            ('Patricia Lebsack', 'Julianne.OConner@kory.org', 'password123', 1),
                            ('Chelsey Dietrich', 'Lucio_Hettinger@annie.ca', 'password123', 0),
                            ('Mrs. Dennis Schulist', 'Karley_Dach@jasper.info', 'password123', 1),
                            ('Kurtis Weissnat', 'Telly.Hoeger@billy.biz', 'password123', 1),
                            ('Nicholas Runolfsdottir V', 'Sherwood@rosamond.me', 'password123', 0),
                            ('Glenna Reichert', 'Chaim_McDermott@dana.io', 'password123', 1),
                            ('Clementina DuBuque', 'Rey.Padberg@karina.biz', 'password123', 1);
                    ";

                    await connection.ExecuteAsync(insertSql);
                }
            }
        }
    }
}
