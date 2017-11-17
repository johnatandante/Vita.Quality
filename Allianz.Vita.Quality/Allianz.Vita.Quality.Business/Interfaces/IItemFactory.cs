using Microsoft.Exchange.WebServices.Data;
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
        IDefect GetNewDefect(int? Id, string agency = null, string defectID = null, string defectType = null, string defectSystem = null, string foundIn = null, string environment = null);
        IMailItem GetNewMailItem(string uniqueId = "");
        void MergeTo(IMailItem itemRead, IDefect defect);
        string GetSubject(IMailItem itemRead);
        IAttachment ToAttachment(string subject, byte[] content);

    }
}
