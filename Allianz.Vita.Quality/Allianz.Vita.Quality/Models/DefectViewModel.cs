using System.Collections.Generic;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Services;

namespace Allianz.Vita.Quality.Models
{
	public class DefectViewModel
	{
		public string ConnectionMessage { get; set; }
		public IList<IMailItem> InboxMessages { get; set; }
		public IFolderItem PublicFolder { get; set; }

		public DefectViewModel() {
            InboxMessages = ItemFactory.Instance.GetNewMailItemList();
            PublicFolder = ItemFactory.Instance.GetNewFolderItem();

        }

    }
	
}