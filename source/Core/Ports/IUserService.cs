using MedicalProject.Application.DTOs;
using System.Threading.Tasks;

namespace MedicalProject.Core.Ports
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(RegisterUserDto model);
        Task<(bool, bool)> VerifyUserAsync(VerifyUserDto model);
    }
}