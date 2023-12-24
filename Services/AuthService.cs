using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Contracts;
using Shared.Data_Transfer;

namespace Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;

        private User? User;

        public AuthService(IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<IdentityResult> RegisterUser(UserCreationDto newUser)
        {
            var user = mapper.Map<User>(newUser);

            var result = await userManager.CreateAsync(user, newUser.Password);

            if(result.Succeeded)
            {
                if(userManager.Users.Count() < 1)
                {
                    await userManager.AddToRolesAsync(user, new String[] { "User", "Admin" });
                }
                else
                {
                    await userManager.AddToRoleAsync(user, "User");
                }
            }

            return result;
        }

        public async Task<bool> AuthenticateUser(UserLoginDto loginDetails)
        {
            User = await userManager.FindByEmailAsync(loginDetails.Email);

            return User != null && await userManager.CheckPasswordAsync(User, loginDetails.Password);
        }

        public async Task<TokenDto> CreateToken(bool RefreshTokenExpiry)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var claims = await GetClaims();
            var signingCreds = GetSigningCredentials();

            var token = new JwtSecurityToken(issuer: jwtSettings["Issuer"], 
                audience: jwtSettings["Audience"], 
                claims: claims, 
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials: signingCreds);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            User.RefreshToken = GenerateRefreshToken();

            if(RefreshTokenExpiry)
            {
                User.RefreshTokenExpiry = DateTime.Now.AddDays(7);
            }

            await userManager.UpdateAsync(User);

            return new TokenDto
            {
                accessToken = accessToken,
                refreshToken = User.RefreshToken
            };
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, User.Email));
            claims.Add(new Claim(ClaimTypes.Name, User.UserName));

            var roles = await userManager.GetRolesAsync(User);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
        
        private SigningCredentials GetSigningCredentials()
        {
            return new SigningCredentials(new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("JwtSettings")["SigningKey"])), 
                SecurityAlgorithms.HmacSha256);
        }

        public ClaimsPrincipal GetClaimsPrincipalFromAccess(string accessToken)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false,

                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SigningKey"])),
                ClockSkew = TimeSpan.Zero
            };

            var handler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = handler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);
            var token = securityToken as JwtSecurityToken;

            if(principal is null || !token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new Exception("Invalid Token Details"); // TODO: Come back to this
            }


            return principal;
        }

        private string GenerateRefreshToken()
        {
            byte[] tokens = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(tokens);

                return Convert.ToBase64String(tokens);
            }
        }

        public async Task<TokenDto> RefreshToken(string accessToken, string refreshToken)
        {
            var principal = GetClaimsPrincipalFromAccess(accessToken);

            var user = await userManager.FindByNameAsync(principal?.Identity?.Name);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiry < DateTime.Now)
            {
                throw new Exception("Invalid Token Details"); // TODO: Come back to this
            }

            User = user;
            return await CreateToken(RefreshTokenExpiry: false);
        }
    }
}
