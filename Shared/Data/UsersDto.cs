using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedAPI.RequestFeatures;

namespace SharedAPI.Data
{
    public class UsersDto
    {
        public string? ImageUrl { get; set; }

        public string? FullName { get; set; }

        public string? UserName { get; set; }

        public string? Course { get; set; }

        public string? ProficientCourse { get; set; }

        public string? MatchStatus { get; set; }

        public decimal Rating { get; set; }

        public Metadata metadata { get; set; }
    }
}
