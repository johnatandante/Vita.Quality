using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Interfaces.Enums;
using Allianz.Vita.Quality.Business.Models;
using Allianz.Vita.Quality.Business.Utilities;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allianz.Vita.Quality.Business.Factory
{
    public class ItemFactory : IItemFactory
    {

        IStorageService Storage;

        IConfigurationService Config;

        public ItemFactory() : this(null, null) { }

        public ItemFactory(IStorageService storage, IConfigurationService config)
        {
            Storage = storage ?? ServiceFactory.Get<IStorageService>();

            Config = config ?? ServiceFactory.Get<IConfigurationService>();
        }

        #region IItemFactory Members

        public IFolderItem GetNewFolderItem()
        {
            return new FolderItem();
        }

        public IList<IMailItem> GetNewMailItemList()
        {
            return new List<IMailItem>();
        }

        public IMailItem ToMailItem(EmailMessage mail, bool propFull = false)
        {
            return new MailItem()
            {
                UniqueId = mail.Id.UniqueId
                ,
                From = mail.From.Name
                ,
                Subject = mail.Subject
                ,
                Content = propFull ? mail.Body.Text : string.Empty
                //, Flagged = propFull ? mail.Flag.FlagStatus != ItemFlagStatus.NotFlagged : false
                ,
                Attachments = propFull ? mail.Attachments.ToArray() : new object[] { }
                ,
                Categories = propFull ? mail.Categories.ToArray() : new string[] { }
                ,
                Importance = propFull ? mail.Importance.ToString() : Importance.Normal.ToString()
                //, ConversationId = propFull ? mail.ConversationId.UniqueId : string.Empty
            };

        }

        public IFolderItem ToFolderItem(Folder folder, FindItemsResults<Item> resultItems = null)
        {

            IFolderItem folderItem = new FolderItem()
            {
                DisplayName = folder.DisplayName
                ,
                Messages = new List<IMailItem>()
            };

            if (resultItems != null)
                resultItems
                    .Select(i => ToMailItem(i as EmailMessage))
                    .ToList()
                    .ForEach(item => folderItem.Messages.Add(item));

            return folderItem;

        }

        public IDefect GetNewDefect(IMailItem itemRead)
        {

            SubjectMetaData data = new SubjectMetaData(itemRead.Subject);

            IDefect defect = new Defect()
            {
                Title = data.Title
                ,
                AreaPath = HttpUtility.UrlDecode(Config.DefaultAreaPath)
                ,
                SurveySystem = Config.DefaultSurveySystem
                ,
                DefectID = data.Id
                ,
                FoundIn = Config.CurrentWebAppId
                ,
                Agency = data.DecodeCodCompany + " " + data.Agency.ToString().PadLeft(4, '0')
                ,
                Environment = Config.DefaultEnvironment
                ,
                Iteration = HttpUtility.UrlDecode(Config.DefaultIteration)
                ,
                DefectType = Config.DefaultDefectType
                ,
                Severity = (SeverityLevel)Enum.Parse(typeof(SeverityLevel), Config.DefaultSeverity, true)
                ,
                State = Config.DefaultDefectState
                ,
                Description = HttpUtility.UrlDecode(itemRead.Content)
                ,
                Attachment = new IAttachment[] { }
                ,
                Comments = new string[] { }
                ,
                IMailItemUniqueId = itemRead.UniqueId

            };

            return defect;

        }

        public IDefect ToDefectItem(WorkItem workItem)
        {

            Defect defect = new Defect()
            {
                Id = workItem.Id,

                Title = workItem.TryToGetField("System.Title"),

                AreaPath = workItem.TryToGetField("System.AreaPath"),

                Iteration = workItem.TryToGetField("System.IterationPath"),

                SurveySystem = workItem.TryToGetField("Allianz.Alm.DefectSystem"),

                DefectID = workItem.TryToGetField("Allianz.Alm.DefectID"),

                FoundIn = workItem.TryToGetField("Microsoft.VSTS.Build.FoundIn"),

                Agency = workItem.TryToGetField("Allianz.Alm.Agenzia"),

                Environment = workItem.TryToGetField("Allianz.Alm.environment"),

                DefectType = workItem.TryToGetField("Allianz.Alm.DefectType"),

                State = workItem.TryToGetField("System.State"),

                Description = workItem.TryToGetField("System.Description"),

                Severity = workItem.TryToGetEnumField<SeverityLevel>("Microsoft.VSTS.Common.Severity"),

                Comments = new string[] { },

                Attachment = new IAttachment[] { },

            };

            return defect;

        }

        public IEnumerable<IDefect> ToDefectItemCollection(WorkItemCollection workItems)
        {
            List<IDefect> result = new List<IDefect>();
            foreach (WorkItem workItem in workItems)
            {
                result.Add(ToDefectItem(workItem));
            }

            return result;

        }

        public IMailItem GetNewMailItem(string uniqueId = "")
        {
            return new MailItem() { UniqueId = uniqueId };

        }

        public Microsoft.TeamFoundation.WorkItemTracking.Client.Attachment ToAttachment(IAttachment att, string comment = "", string fileName = "")
        {
            if (string.IsNullOrEmpty(fileName))
                fileName = "Mail.eml";

            return new Microsoft.TeamFoundation.WorkItemTracking.Client.Attachment(Storage.Store(att, fileName), comment);

        }

        public IAttachment ToAttachment(string subject, byte[] buffer)
        {
            return new MailAsAttachment() { Title = subject, Content = buffer };
        }

        public void MergeTo(IMailItem itemRead, IDefect defect)
        {
            
            defect.Description = HttpUtility.UrlDecode(itemRead.Content);
            defect.IMailItemUniqueId = itemRead.UniqueId;

        }

        public string GetSubject(IMailItem itemRead)
        {
            return new SubjectMetaData(itemRead.Subject).Title;
        }

        #endregion
    }

}
