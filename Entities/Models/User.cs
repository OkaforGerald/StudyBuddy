using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using static System.Net.WebRequestMethods;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Bio { get; set; }

        //Change this url before live
        public string? ImageUrl { get; set; } = @"https://localhost:7122/Images/defaultavi.jpg";

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiry { get; set; }

        public ICollection<Message>? MessagesSent { get; set; }

        public ICollection<Message>? MessagesReceived { get; set; }
    }
}
