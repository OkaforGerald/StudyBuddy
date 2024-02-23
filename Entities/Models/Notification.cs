using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public enum NotificationType
    {
        MatchRequest = 1,
        ProfileView = 2,
        AckMatch = 3,
        DecMatch = 4
    }

    public class Notification
    {
        public Guid Id { get; set; }

        [ForeignKey(nameof(ProfileViewer))]
        public string? ProfileViewerId { get; set; }
        public User? ProfileViewer { get; set; }

        [ForeignKey(nameof(Owner))]
        public string? OwnerId { get; set; }
        public User? Owner { get; set; }

        [ForeignKey(nameof(Matcher))]
        public string? MatcherId { get; set; }
        public User? Matcher { get; set; }

        [ForeignKey(nameof(Matched))]
        public string? MatchedId { get; set; }
        public User? Matched { get; set; }

        public NotificationType NotifType { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
