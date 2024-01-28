using Entities.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using SharedAPI.Data;

namespace StudyBuddy.Client.Client.Pages
{
	public partial class UserProfile
	{
		[Parameter]
		public string? UserName { get; set; }

        private UserDetailsDto? details = new UserDetailsDto { ProficientCourses = new List<ProficientCoursesDto>()};

        protected override async Task OnInitializedAsync()
        {
            Interceptor.RegisterEvent();
            details = await UserService.GetUserDetails(UserName);
        }

        public async ValueTask DisposeAsync()
        {
            Interceptor.DisposeEvent();
        }
    }
}
