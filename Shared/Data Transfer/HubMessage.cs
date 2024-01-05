using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Data_Transfer
{
    public class HubMessage
    {
        public string? messageContent { get; set; }

        public string? RecipientUsername { get; set; }

        public string? SnederUsername { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
