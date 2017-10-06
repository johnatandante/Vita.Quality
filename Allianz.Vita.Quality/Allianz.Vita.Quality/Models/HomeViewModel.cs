using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allianz.Vita.Quality.Models
{
    public class HomeViewModel
    {
        public string ConnectionMessages { get; set; }

        public string[] InboxMessages { get; set; }

        public string[] PublicFolderMessages { get; set; }

        public string PublicFolderDisplayName { get; set; }

    }
}