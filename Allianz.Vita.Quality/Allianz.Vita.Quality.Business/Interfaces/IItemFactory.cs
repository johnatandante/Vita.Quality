using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace Allianz.Vita.Quality.Business.Interfaces
{
	public interface IItemFactory : IService
	{
		IFolderItem GetNewFolderItem();

		IList<IMailItem> GetNewMailItemList();

		IMailItem ToMailItem(EmailMessage mail, bool propFull = false );

		IFolderItem ToFolderItem(Folder folder, FindItemsResults<Item> resultItems = null);

		IDefect GetNewDefect(IMailItem itemRead);

        IDefect ToDefectItem(WorkItem w);

        IEnumerable<IDefect> ToDefectItemCollection(WorkItemCollection workItems);
    }
}
