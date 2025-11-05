using BookstoreApplication.DTO.Login.Request;
using BookstoreApplication.DTO.Register.Request;

namespace BookstoreApplication.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegistrationDto data);
        Task Login(LoginRequestDto data);
    }
}
