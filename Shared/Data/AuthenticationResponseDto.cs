using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedAPI.Data
{
    public record AuthenticationResponseDto
    {
        public bool IsAuthSuccessful { get; init; }
        public string ErrorMessage { get; init; }
        public string accessToken { get; init; }
        public string refreshToken { get; init; }
    }
}
