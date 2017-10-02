using Allianz.Vita.Quality.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allianz.Vita.Quality.Business.Models
{
    class MailAsAttachment : IAttachment
    {
        public string Title { get; set; }

        public byte[] Content { get; set; }
    }
}
