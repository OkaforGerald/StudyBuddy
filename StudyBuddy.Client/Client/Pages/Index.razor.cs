using SharedAPI.Data;

namespace StudyBuddy.Client.Client.Pages
{
    public partial class Index
    {
        private DashboardDto dashboardDto { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            dashboardDto = await UserService.Dashboard();
        }
    }
}
