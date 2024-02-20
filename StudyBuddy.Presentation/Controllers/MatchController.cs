using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StudyBuddy.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly IServiceManager manager;

        public MatchController(IServiceManager manager)
        {
            this.manager = manager;
        }

        [HttpPost("{username}")]
        public async Task<IActionResult> CreateMatch(string username)
        {
            if(string.IsNullOrEmpty(username))
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Error = "Username Can't be null"
                });
            }

            var requester = HttpContext.User?.Identity?.Name;
            try
            {
                await manager.MatchService.CreateMatch(username, requester);
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return NoContent();
        }

        [HttpPut("{username}")]
        public async Task<IActionResult> AcknowledgeMatch(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Error = "Username Can't be null"
                    });
                }

                var acknowledger = HttpContext.User?.Identity?.Name;

                await manager.MatchService.AcknowledgeMatch(acknowledger, username, trackChanges: true);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return NoContent();
        }
    }
}
