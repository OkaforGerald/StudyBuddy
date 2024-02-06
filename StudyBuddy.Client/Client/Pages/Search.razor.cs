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
 
        public PagingResponse<UsersDto> users = new PagingResponse<UsersDto>();

        [Parameter]
        public EventCallback<string> OnSearchChanged { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Interceptor.RegisterEvent();
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

        public async ValueTask DisposeAsync()
        {
            Interceptor.DisposeEvent();
        }
    }
}
