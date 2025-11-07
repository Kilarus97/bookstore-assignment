using BookstoreApplication.DTO.Login.Request;
using BookstoreApplication.DTO.Register.Request;
using BookstoreApplication.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly GoogleAuthService _googleAuthService;
        private readonly JwtService _jwtService;

        public AuthController(IAuthService authService, GoogleAuthService googleAuthService, JwtService jwtService)
        {
            _authService = authService;
            _googleAuthService = googleAuthService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _authService.RegisterAsync(data);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var token = await _authService.Login(data);
            return Ok(token);
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            return Ok(await _authService.GetProfile(User));
        }

        [HttpPost("google")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            try
            {
                var payload = await _googleAuthService.VerifyTokenAsync(request.IdToken);

                // Kreiraj svog korisnika ili pronađi u bazi
                var user = new
                {
                    Email = payload.Email,
                    Name = payload.Name,
                    Picture = payload.Picture,
                    GoogleId = payload.Subject
                };

                // Izdaj sopstveni JWT
                var token = _jwtService.GenerateToken(user.Email, "User"); // možeš dodati role

                return Ok(new { user, token });
            }
            catch
            {
                return Unauthorized("Invalid Google token");
            }
        }
    }
}
