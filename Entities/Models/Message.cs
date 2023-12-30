using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Message
    {
        public Guid Id {  get; set; }

        public string? messageContent { get; set; }

        [ForeignKey(nameof(Sender))]
        public string SenderId { get; set; }
        public User? Sender { get; set; }

        [ForeignKey(nameof(Recipient))]
        public string RecipientId { get; set; }
        public User? Recipient { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
