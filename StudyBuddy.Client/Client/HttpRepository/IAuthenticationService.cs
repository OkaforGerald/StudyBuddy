using SharedAPI.Data;

namespace StudyBuddy.Client.Client.HttpRepository
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponseDto> RegisterUser(UserCreationDto userForRegistration);

        Task<AuthenticationResponseDto> Login(UserLoginDto userForAuthentication);

        Task<string> RefreshToken();

        Task Logout();
    }
}
