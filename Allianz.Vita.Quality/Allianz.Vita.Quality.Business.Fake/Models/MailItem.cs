using Allianz.Vita.Quality.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allianz.Vita.Quality.Business.Fake.Models
{
    class MailItem : IMailItem
    {
        public string UniqueId { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public bool Flagged { get; set; }

        public object[] Attachments { get; set; }

        public string[] Categories { get; set; }

        public string Importance { get; set; }

        public string ConversationId { get; set; }
    }
}
