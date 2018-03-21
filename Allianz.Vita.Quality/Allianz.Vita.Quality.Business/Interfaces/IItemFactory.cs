using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using System.Collections.Generic;
using System.Net;
using System;

namespace Allianz.Vita.Quality.Business.Interfaces
{
    public interface IItemFactory : IService
	{
		IList<IMailItem> GetNewMailItemList();
		IDefect GetNewDefect(IMailItem itemRead);
        IDefect GetNewDefect(int? Id, string agency = null, string defectID = null, string defectType = null, string defectSystem = null, string foundIn = null, string environment = null);        
        T GetNew<T>(params object[] parameters) where T : IItem;
        void MergeTo(IMailItem itemRead, IDefect defect);
        string GetSubject(IMailItem itemRead);
        IAttachment ToAttachment(string subject, byte[] content);
        IUserCredentials GetNewUserCredential(NetworkCredential identity);
        IIssueItem GetNewIssueItem(string id, string type, string assignee, string priority, string project, string summary, string status, DateTime created, DateTime? resolvedOn, DateTime? reopenedOn, string nomeGruppoLife, bool? digitalAgency);
        IMailItem ToMailItem(string uniqueId, string from, string subject, string content, object[] attachments, string[] categories, string importance);
        IFolderItem ToFolderItem(string displayName);
    }
}
