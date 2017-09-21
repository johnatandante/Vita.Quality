using System.Collections.Generic;
using Allianz.Vita.Quality.Business.Interfaces;

namespace Allianz.Vita.Quality.Models
{
	public class DefectViewModel
	{
		public string ConnectionMessage { get; set; }
		public IList<IMailItem> InboxMessages { get; set; }
		public IFolderItem PublicFolder { get; set; }

		public DefectViewModel() {
			InboxMessages = new List<IMailItem>();

		}

	}
	
}