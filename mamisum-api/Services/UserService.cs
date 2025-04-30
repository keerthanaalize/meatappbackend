using System.Security.Cryptography;
using System.Text;
using mamisum_api.Models;
using mamisum_api.Models.Users;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace mamisum_api.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;
        private readonly ILogger<UserService> _logger;

        public UserService(IOptions<MongoDbSettings> mongoDbSettings, ILogger<UserService> logger)
        {
            var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _users = database.GetCollection<User>("Users");

            _logger = logger;
        }

        public async Task<bool> RegisterUser(RegisterRequest request)
        {
            _logger.LogInformation($"Attempting to register user with email: {request.Email}");

            var existingUser = await _users.Find(u => u.Email.ToLower() == request.Email.ToLower()).FirstOrDefaultAsync();

            if (existingUser != null)
            {
                if (existingUser.Role == request.Role)
                {
                    _logger.LogWarning($"User with email {request.Email} already registered as {request.Role}.");
                    return false; 
                }
                else
                {
                    _logger.LogWarning($"User with email {request.Email} already exists with role: {existingUser.Role}. Cannot register as {request.Role}.");
                    return false; 
                }
            }

            var newUser = new User
            {
                Email = request.Email,
                Name = request.Name,
                PasswordHash = HashPassword(request.Password),
                Role = request.Role
            };

            try
            {
                await _users.InsertOneAsync(newUser);
                _logger.LogInformation($"User with email {request.Email} registered successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting user: {ex.Message}");
                return false;
            }
        }


        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(enteredPassword);
                var hash = sha256.ComputeHash(bytes);
                var computedHash = Convert.ToBase64String(hash);

                return computedHash == storedPasswordHash;
            }
        }

        public async Task<bool> UpdatePassword(string email, string currentPassword, string newPassword)
        {
            var user = await GetUserByEmail(email);

            if (user == null)
            {
                _logger.LogWarning($"No user found with email: {email}");
                return false;
            }

            if (!VerifyPassword(currentPassword, user.PasswordHash))
            {
                _logger.LogWarning($"Password verification failed for email: {email}");
                return false;
            }

            var newPasswordHash = HashPassword(newPassword);
            _logger.LogInformation($"New Password Hash: {newPasswordHash}");

            var update = Builders<User>.Update.Set(u => u.PasswordHash, newPasswordHash);
            var result = await _users.UpdateOneAsync(u => u.Email.ToLower() == email.ToLower(), update);

            if (result.ModifiedCount > 0)
            {
                _logger.LogInformation($"Password updated successfully for email: {email}");
                return true;
            }
            else
            {
                _logger.LogWarning($"Failed to update password for email: {email}");
                return false;
            }
        }

        public async Task<bool> DeleteUserById(string id)
        {
            _logger.LogInformation($"Attempting to delete user with id: {id}");

            if (!ObjectId.TryParse(id, out var objectId))
            {
                _logger.LogWarning($"Invalid ObjectId format: {id}");
                return false;
            }

            var result = await _users.DeleteOneAsync(u => u.Id == objectId.ToString());

            if (result.DeletedCount > 0)
            {
                _logger.LogInformation($"User with id: {id} deleted successfully.");
                return true;
            }

            _logger.LogWarning($"No user found with id: {id}");
            return false;
        }
    }
}