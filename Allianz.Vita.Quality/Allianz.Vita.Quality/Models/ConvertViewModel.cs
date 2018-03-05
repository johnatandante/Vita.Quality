using Allianz.Vita.Quality.Business.Interfaces;
using System.Collections.Generic;

namespace Allianz.Vita.Quality.Models
{
    public class ConvertViewModel
	{

        public string PublicFolderDisplayName { get; set; }
        public IList<IMailItem> PublicFolderMessages { get; set; }

    }
	
}