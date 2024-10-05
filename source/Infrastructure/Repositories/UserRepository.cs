using Npgsql;
using MedicalProject.Core.Entities;
using MedicalProject.Core.Ports;
using System.Threading.Tasks;

namespace MedicalProject.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var user = await connection.QuerySingleOrDefaultAsync<User>("SELECT * FROM users WHERE id = @id", new { id });

            return user;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var user = await connection.QuerySingleOrDefaultAsync<User>("SELECT * FROM users WHERE username = @username", new { username });

            return user;
        }

        public async Task AddUserAsync(User user)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            await connection.ExecuteAsync("INSERT INTO users (username, password_hash, email) VALUES (@username, @password_hash, @email)", user);
        }

        public async Task UpdateUserAsync(User user)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            await connection.ExecuteAsync("UPDATE users SET username = @username, password_hash = @password_hash, email = @email WHERE id = @id", user);
        }

        public async Task DeleteUserAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            await connection.ExecuteAsync("DELETE FROM users WHERE id = @id", new { id });
        }
    }
}