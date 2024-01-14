using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using StudyBuddy.Client.Client.AuthProviders;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using SharedAPI.Data;

namespace StudyBuddy.Client.Client.HttpRepository
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthenticationService(HttpClient client, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        public async Task<RegistrationResponseDto> RegisterUser(UserCreationDto userForRegistration)
        {
            var content = JsonSerializer.Serialize(userForRegistration);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var registrationResult = await _client.PostAsync("auth/register", bodyContent);
            var registrationContent = await registrationResult.Content.ReadAsStringAsync();

            if (!registrationResult.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<RegistrationResponseDto>(registrationContent, _options);
                return result!;
            }

            return new RegistrationResponseDto { IsSuccessfulRegistration = true };
        }

        public async Task<AuthenticationResponseDto> Login(UserLoginDto userForAuthentication)
        {
            var content = JsonSerializer.Serialize(userForAuthentication);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var authResult = await _client.PostAsync("auth/login", bodyContent);
            var authContent = await authResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AuthenticationResponseDto>(authContent, _options);

            if (!authResult.IsSuccessStatusCode)
                return result!;

            await _localStorage.SetItemAsync("authToken", result!.accessToken);
            await _localStorage.SetItemAsync("refreshToken", result!.refreshToken);
            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.accessToken);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.accessToken);

            return new AuthenticationResponseDto { IsAuthSuccessful = true };
        }

        public async Task<string> RefreshToken()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");

            var tokenDto = JsonSerializer.Serialize(new TokenDto { accessToken = token, refreshToken = refreshToken});
            var bodyContent = new StringContent(tokenDto, Encoding.UTF8, "application/json");

            var refreshResult = await _client.PostAsync("auth/refresh-token", bodyContent);
            var refreshContent = await refreshResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AuthenticationResponseDto>(refreshContent, _options);

            if (!refreshResult.IsSuccessStatusCode)
                throw new ApplicationException("Something went wrong during the refresh token action");

            await _localStorage.SetItemAsync("authToken", result.accessToken);
            await _localStorage.SetItemAsync("refreshToken", result.refreshToken);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.accessToken);

            return result.accessToken;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("refreshToken");
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _client.DefaultRequestHeaders.Authorization = null;
        }
    }
}
