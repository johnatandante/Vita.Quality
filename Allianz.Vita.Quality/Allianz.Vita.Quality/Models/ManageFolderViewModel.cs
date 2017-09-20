using System.Collections.Generic;
using Allianz.Vita.Quality.Business.Interfaces;

namespace Allianz.Vita.Quality.Models
{
	public class ManageFolderViewModel
	{
		public string ConnectionMessage { get; set; }
		public IList<IMailItem> InboxMessages { get; set; }
		public IFolderItem PublicFolder { get; set; }

		public ManageFolderViewModel() {
			InboxMessages = new List<IMailItem>();

		}

	}
	
}