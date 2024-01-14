using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SharedAPI.Data;

namespace Services.Contracts
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUser(UserCreationDto newUser);

        Task<bool> AuthenticateUser(UserLoginDto loginDetails);

        Task<TokenDto> CreateToken(bool RefreshTokenExpiry);

        ClaimsPrincipal GetClaimsPrincipalFromAccess(string accessToken);

        Task<TokenDto> RefreshToken(string accessToken, string refreshToken);
    }
}
