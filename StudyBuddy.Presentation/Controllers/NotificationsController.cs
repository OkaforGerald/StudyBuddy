using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using SharedAPI.RequestFeatures;

namespace StudyBuddy.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IServiceManager manager;

        public NotificationsController(IServiceManager manager)
        {
            this.manager = manager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetNotifications([FromQuery] RequestParameters parameters)
        {
            var username = HttpContext?.User?.Identity?.Name;

            var notifs = await manager.NotificationService.GetNotificationsForUser(username, parameters, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(notifs.metadata));

            return Ok(notifs.notifications);
        }
    }
}
