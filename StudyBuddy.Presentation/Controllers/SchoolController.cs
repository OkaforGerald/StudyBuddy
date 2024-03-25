using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StudyBuddy.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public SchoolController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpGet("departments")]
        [Authorize]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await serviceManager.SchoolService.GetDepartments(trackChanges: false);

            return Ok(departments);
        }

        [HttpGet("courses")]
        [Authorize]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await serviceManager.SchoolService.GetCourses(trackChanges: false);

            return Ok(courses);
        }

        [HttpGet("departments/{Id:Guid}/courses")]
        [Authorize]
        public async Task<IActionResult> GetCourses(Guid Id)
        {
            try
            {
                var courses = await serviceManager.SchoolService.GetCourseByDeptId(Id, trackChanges: false);

                return Ok(courses);

            }catch(Exception e)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = e.Message,
                });
            }
        }
    }
}
