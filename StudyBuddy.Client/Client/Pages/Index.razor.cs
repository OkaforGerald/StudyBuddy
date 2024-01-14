using Microsoft.AspNetCore.Components;
using SharedAPI.Data;
using StudyBuddy.Client.Client.HttpRepository;

namespace StudyBuddy.Client.Client.Pages
{
    public partial class Index
    {
        private UserLoginDto _userForAuthentication = new UserLoginDto();

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public bool ShowAuthenticationErrors { get; set; }
        public string Errors { get; set; }

        public async Task ExecuteLogin()
        {
            ShowAuthenticationErrors = false;

            var result = await AuthenticationService.Login(_userForAuthentication);
            if (!result.IsAuthSuccessful)
            {
                Errors = result.ErrorMessage;
                ShowAuthenticationErrors = true;
            }
            else
            {
                NavigationManager.NavigateTo("/home");
            }
        }
    }
}
