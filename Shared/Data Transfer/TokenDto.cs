using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data_Transfer
{
    public record TokenDto
    {
        [Required]
        public string? accessToken { get; init; }

        [Required]
        public string? refreshToken { get; init; }
    }
}
