using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public enum MatchStatus
    {
        Pending,
        Friends,
        PendingAck,
        Nil

    }
    public class Match
    {
        public Guid Id { get; set; }

        [ForeignKey(nameof(User))]
        public string? MatcherId { get; set; }
        public User? Matcher { get; set; }

        [ForeignKey(nameof(User))]
        public string? MatchedId { get; set; }
        public User? Matched { get; set; }

        public MatchStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
