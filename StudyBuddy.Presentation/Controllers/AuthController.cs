using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Shared.Data_Transfer;

namespace StudyBuddy.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public AuthController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreationDto newUser)
        {
            if (newUser is null)
            {
                return BadRequest("User Creation Object Can't be Null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await serviceManager.AuthService.RegisterUser(newUser);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(new RegistrationResponseDto { Errors = errors});
            }

            return Ok(new
            {
                StatusCode = 200,
                Message = "Registration successful"
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto user)
        {
            try
            {
                if (user is null)
                {
                    return BadRequest("User Login Object Can't be Null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await serviceManager.AuthService.AuthenticateUser(user);
                if (!result)
                {
                    return Unauthorized(
                    new AuthenticationResponseDto
                    {
                        ErrorMessage = "Invalid Username/Password"
                    }
                );
                }

                var token = await serviceManager.AuthService.CreateToken(RefreshTokenExpiry: true);

                return Ok(new AuthenticationResponseDto
                {
                    IsAuthSuccessful = true,
                    accessToken = token.accessToken,
                    refreshToken = token.refreshToken
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = e.Message
                });
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenDto tokenDto)
        {
            try
            {
                if (tokenDto is null)
                {
                    return BadRequest("User Login Object Can't be Null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var token = await serviceManager.AuthService.RefreshToken(tokenDto.accessToken, tokenDto.refreshToken);

                return Ok(new AuthenticationResponseDto
                {
                    IsAuthSuccessful = true,
                    accessToken = token.accessToken,
                    refreshToken = token.refreshToken
                });
            }
            catch(Exception e)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = e.Message
                });
            }
        }
    }
}
