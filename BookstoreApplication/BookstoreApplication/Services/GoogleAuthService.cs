using Google.Apis.Auth;
using System.Threading.Tasks;

public class GoogleAuthService
{
    private readonly string _clientId;

    public GoogleAuthService(string clientId)
    {
        _clientId = clientId;
    }

    public async Task<GoogleJsonWebSignature.Payload> VerifyTokenAsync(string idToken)
    {
        var settings = new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new[] { _clientId }
        };

        var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
        return payload; // sadrži Email, Name, Picture, Subject (Google user ID)
    }
}
