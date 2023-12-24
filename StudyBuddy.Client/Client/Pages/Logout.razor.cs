using StudyBuddy.Client.Client.HttpRepository;
using Microsoft.AspNetCore.Components;

namespace StudyBuddy.Client.Client.Pages
{
    public partial class Logout
    {
        [Inject] public IAuthenticationService AuthenticationService { get; set; } = null!;

        [Inject] public NavigationManager NavigationManager { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            await AuthenticationService.Logout();
            NavigationManager.NavigateTo("/");
        }
    }
}
