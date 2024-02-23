using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedAPI.Data
{
    public class NotificationDto
    {
        public string? ProfileViewer { get; set; }

        public string? Owner { get; set; }

        public string? Matcher { get; set; }

        public string? Matched { get; set; }

        public string? NotifType { get; set; }

        public bool isRead { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
