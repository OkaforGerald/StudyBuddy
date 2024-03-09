using Entities.Models;
using Microsoft.AspNetCore.Components;
using SharedAPI.Data;
using StudyBuddy.Client.Client.HttpRepository;

namespace StudyBuddy.Client.Client.Pages
{
    public partial class Index
    {
        private DashboardDto dashboardDto { get; set; } = new();
        List<DepartmentDto> deppartments { get; set; } = new();
        List<CourseDto> courses { get; set; } = new();
        private AddDetailsDto details { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            dashboardDto = await UserService.Dashboard();
            deppartments = await UserService.GetDepartments();
        }

        public async Task AddDetails()
        {

        }
    }
}
