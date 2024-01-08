using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StudyBuddy.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public MessagesController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet("contacts")]
        [Authorize]
        public async Task<IActionResult> GetMessagedPeople()
        {
            try
            {
                var username = HttpContext?.User?.Identity?.Name;

                var people = await serviceManager.MessageService.GetMessagedPeople(username);

                // serviceManager.PublishMessageService.EnqueueMessage(people);

                return Ok(people);
            }catch(Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }
    }
}
