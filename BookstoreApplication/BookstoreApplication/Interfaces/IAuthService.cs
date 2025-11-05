using System.Security.Claims;
using BookstoreApplication.DTO.Login.Request;
using BookstoreApplication.DTO.Register.Request;
using BookstoreApplication.Models;

namespace BookstoreApplication.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegistrationDto data);
        Task<string> Login(LoginRequestDto data);
        Task<ProfileRequestDTO> GetProfile(ClaimsPrincipal userPrincipal);
    }
}
