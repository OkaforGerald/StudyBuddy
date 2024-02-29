using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedAPI.Data
{
    public record DashboardDto
    {
        public int ProfileViews { get; set; }

        public int PendingMatches { get; set; }

        public int NumMatches { get; set; }

        public DateTime TimeRequested { get; set; }

        public string? username { get; set; }
    }
}
