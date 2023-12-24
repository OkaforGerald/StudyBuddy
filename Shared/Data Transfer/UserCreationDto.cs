using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data_Transfer
{
    public record UserCreationDto
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        [MaxLength(11, ErrorMessage ="Phone Number Must Not Exceed 11 Numbers")]
        [MinLength(11, ErrorMessage = "Phone Number Must Not be Less than 11 Numbers")]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Password { get; set; }

        [Compare("Password")]
        public string? ComparePassword { get; set; }
    }
}
