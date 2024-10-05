using MedicalProject.Core.Ports;
using MedicalProject.Application.DTOs;
using System.Threading.Tasks;

namespace MedicalProject.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> RegisterUserAsync(RegisterUserDto model)
        {
            // Реализация регистрации пользователя
            // TODO: Добавить код для регистрации пользователя
            return await Task.FromResult(true);
        }

        public async Task<(bool, bool)> VerifyUserAsync(VerifyUserDto model)
        {
            // Реализация верификации пользователя
            // TODO: Добавить код для верификации пользователя
            return await Task.FromResult((true, false));
        }
    }
}