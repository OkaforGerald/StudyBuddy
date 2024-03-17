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

        public Guid DepartmentId { get; set; }

        public Guid CourseId { get; set; }

        [Inject]
        public IUserService userService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            dashboardDto = await UserService.Dashboard();
            deppartments = await UserService.GetDepartments();
        }

        public async Task AddDetails()
        {

        }

        private async Task DepartmentClicked(ChangeEventArgs departmentEvent)
        {
            courses.Clear();
            CourseId = Guid.Empty;

            DepartmentId = new Guid(departmentEvent.Value.ToString());
            details.DepartmentId = DepartmentId;
            courses = await userService.GetCourses(DepartmentId);
            StateHasChanged();
        }

        private async Task CourseClicked(ChangeEventArgs courseEvent)
        {
            CourseId = new Guid(courseEvent.Value.ToString());
            details.CourseId = CourseId;
            StateHasChanged();
        }
    }
}
