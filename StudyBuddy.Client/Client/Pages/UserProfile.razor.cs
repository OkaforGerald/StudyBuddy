using Microsoft.AspNetCore.Components;

namespace StudyBuddy.Client.Client.Pages
{
	public partial class UserProfile
	{
		[Parameter]
		public string? UserName { get; set; }
	}
}
