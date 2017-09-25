using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allianz.Vita.Quality.Business.Interfaces;

namespace Allianz.Vita.Quality.Business.Models
{
	public class FolderItem : IFolderItem
	{
		public string DisplayName { get; set; }
		public IList<IMailItem> Messages { get; set; }
		
        public FolderItem()
        {
            Messages = new List<IMailItem>();
        }

	}
}
