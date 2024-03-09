using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using SharedAPI.Data;
using SharedAPI.RequestFeatures;
using StudyBuddy.Client.Client.Features;

namespace StudyBuddy.Client.Client.Pages
{
    public partial class Search
    {
        public string SearchTerm { get; set; }

        public RequestParameters parameters { get; set; } = new();

        List<DepartmentDto> deppartments { get; set; } = new();

        List<CourseDto> courses { get; set; } = new();

        public PagingResponse<UsersDto> users = new PagingResponse<UsersDto>();

        public Guid DepartmentId { get; set; }

        public Guid CourseId { get; set; }

        [Parameter]
        public EventCallback<string> OnSearchChanged { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Interceptor.RegisterEvent();
            deppartments = await userService.GetDepartments();
        }

        private async Task DepartmentClicked(ChangeEventArgs departmentEvent)
        {
            courses.Clear();
            CourseId = Guid.Empty;

            DepartmentId = new Guid(departmentEvent.Value.ToString());
            parameters.DepartmmentId = DepartmentId;
            users = await userService.GetUsers(parameters);
            courses = await userService.GetCourses(DepartmentId);
            StateHasChanged();
        }

        private async Task CourseClicked(ChangeEventArgs courseEvent)
        {
            CourseId = new Guid(courseEvent.Value.ToString());
            parameters.CourseId = CourseId;
            users = await userService.GetUsers(parameters);
            StateHasChanged();
        }

        private async Task SearchChanged(string searchTerm)
        {
            parameters.PageNumber = 1;
            parameters.SearchTerm = searchTerm;
            users = await userService.GetUsers(parameters);
        }

        private async Task PageChanged(int PageNumber)
        {
            parameters.PageNumber = PageNumber;
            users = await userService.GetUsers(parameters);
        }

        public void Dispose()
        {
            Interceptor.DisposeEvent();
        }
    }
}
