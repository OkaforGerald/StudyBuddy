using System.Formats.Asn1;
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

        public int currentRating { get; set; }

        private UserDetailsDto? details = new UserDetailsDto { ProficientCourses = new List<ProficientCoursesDto>()};

        protected override async Task OnInitializedAsync()
        {
            Interceptor.RegisterEvent();
            details = await UserService.GetUserDetails(UserName);
        }

        private async Task Match(string username)
        {
            await MatchService.CreateMatch(username);
            details.MatchStatus = "Pending";
        }

        private async Task AckMatch(string username)
        {
            await MatchService.AckMatch(username);
            details.MatchStatus = "Friends";
        }

        private void SetRating(int rating)
        {
            currentRating = rating;
        }

        public async ValueTask DisposeAsync()
        {
            Interceptor.DisposeEvent();
        }
    }
}
