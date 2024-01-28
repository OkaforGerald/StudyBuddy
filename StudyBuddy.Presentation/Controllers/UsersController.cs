﻿using System;
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
    public class UsersController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public UsersController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;    
        }

        [HttpGet("{username}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(string username)
        {
            try
            {
                if(username is null)
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Error = "Username Can't be null"
                    });
                }

                var requester = HttpContext.User?.Identity?.Name;

                var response = await serviceManager.UserService.GetUserDetails(username, trackChanges: false);

                if(requester != null && requester.Equals(response.Username, StringComparison.CurrentCultureIgnoreCase))
                {
                    response.RequestedByOwner = true;
                }

                return Ok(response);
            }catch(DirectoryNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    ErrorMessage = ex.Message,
                });
            }
        }
    }
}