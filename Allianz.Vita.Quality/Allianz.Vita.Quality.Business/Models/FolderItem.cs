using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using System.Collections.Generic;

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
