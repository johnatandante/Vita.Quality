using Microsoft.Exchange.WebServices.Data;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.Collections.Generic;

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

        IMailItem GetNewMailItem(string uniqueId = "");

        Microsoft.TeamFoundation.WorkItemTracking.Client.Attachment ToAttachment(IAttachment att, string comment = "", string fileName = "");

        IAttachment ToAttachment(string subject, byte[] content);
    }
}
