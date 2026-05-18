using BLL.DTOs;
using DAL.EF.Table;
using DAL.Repos;
using System.Security.Cryptography;
using System.Text;

namespace BLL.Services
{
    public class AuthService
    {
        private readonly UserRepo _userRepo;

        public AuthService(UserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public string Register(UserDTO dto)
        {
            if (_userRepo.EmailExists(dto.Email))
            {
                return "This email is already registered.";
            }

            var newUser = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = HashPassword(dto.Password),
                Role = dto.Role is "Staff" or "Admin" ? dto.Role : "User",
                InterestedOn = dto.InterestedOn
            };

            var isCreated = _userRepo.Create(newUser);

            return isCreated ? "Success" : "Failed to register user. Please try again.";
        }

        public UserDTO Authenticate(string email, string password)
        {
            var hashedPassword = HashPassword(password);
            var user = _userRepo.Authenticate(email, hashedPassword);
            if (user != null)
            {
                return new UserDTO
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role
                };
            }

            return null;
        }

        public bool ResetPassword(string email, string newPassword)
        {
            var user = _userRepo.GetByEmail(email);
            if (user != null)
            {
                user.Password = HashPassword(newPassword);
                return _userRepo.Update(user);
            }
            return false;
        }

        public bool EmailExists(string email)
        {
            return _userRepo.EmailExists(email);
        }

        public User GetUserByEmail(string email)
        {
            return _userRepo.GetByEmail(email);
        }

        private static string HashPassword(string password)
        {
            using var md5 = MD5.Create();
            var inputBytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = md5.ComputeHash(inputBytes);
            return Convert.ToHexString(hashBytes).ToLowerInvariant();
        }
    }
}