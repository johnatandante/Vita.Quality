using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Enums;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using Allianz.Vita.Quality.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Allianz.Vita.Quality.Business.Factory
{
    public class ItemFactory : IItemFactory
    {

        protected IConfigurationService Config;

        public ItemFactory() : this(null) { }

        public ItemFactory(IConfigurationService config)
        {
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

        public IMailItem ToMailItem(string uniqueId, string from, string subject, string content, object[] attachments, string[] categories, string importance)
        {
            return new MailItem()
            {
                UniqueId = uniqueId,
                From = from,
                Subject = subject,
                Content = content,
                Attachments = attachments,
                Categories = categories,
                Importance = importance
            };
        }

        public IFolderItem ToFolderItem(string displayName)
        {
            return new FolderItem()
            {
                DisplayName = displayName,
                Messages = new List<IMailItem>()
            };
        }

        public IDefect GetNewDefect(IMailItem itemRead)
        {

            SubjectMetaData data = new SubjectMetaData(itemRead.Subject);

            string agency = data.DecodeCodCompany + " " + data.Agency.ToString().PadLeft(4, '0');
            IDefect defect = GetNewDefect(null, agency: agency, defectID: data.Id);

            defect.Title = data.Title;
            defect.Description = HttpUtility.UrlDecode(itemRead.Content);
            defect.IMailItemUniqueId = itemRead.UniqueId;

            return defect;

        }

        public IDefect GetNewDefect(int? id = null, string agency = null, string defectID = null, string defectType = null, string defectSystem = null, string foundIn = null, string environment = null)
        {
            IDefectConfiguration config = this.Config.Defect;

            IDefect defect = new DefectItem()
            {
                Id = id,
                DefectID = defectID,
                Agency = agency,

                SurveySystem = defectSystem ?? config.DefaultSurveySystem,
                FoundIn = foundIn ?? config.CurrentWebAppId,
                Environment = environment ?? config.DefaultEnvironment,
                DefectType = defectType ?? config.DefaultDefectType,

                AreaPath = HttpUtility.UrlDecode(config.DefaultAreaPath),
                Iteration = HttpUtility.UrlDecode(config.DefaultIteration),
                State = config.DefaultDefectState,
                Severity = (SeverityLevel)Enum.Parse(typeof(SeverityLevel), config.DefaultSeverity, true),

                Attachment = new IAttachment[] { },
                Comments = new string[] { }

            };

            return defect;
        }

        public IMailItem GetNewMailItem(string uniqueId = "")
        {
            return new MailItem() { UniqueId = uniqueId };
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

        public IUserCredentials GetNewUserCredential(NetworkCredential identity)
        {
            return new UserCredential(identity.UserName, UserCredential.AuthenticationMode.Classic.ToString());
        }

        public IIssueItem GetNewIssue()
        {
            return new IssueItem() { };
        }

        public IIssueItem GetNewIssueItem(string id, string type, string assignee, string priority, string project, string summary, string status, DateTime created, DateTime? resolvedOn, DateTime? reopenedOn, string nomeGruppoLife, bool? digitalAgency)
        {

            return new IssueItem()
            {
                Id = id,
                Assignee = assignee,
                Created = created,
                ResolvedOn = resolvedOn,
                ReopenedOn = reopenedOn,
                NomeGruppoLife = nomeGruppoLife,
                Priority = priority,
                Project = project,
                Summary = summary,
                DigitalAgency = digitalAgency,
                Status = status,
                IssueType = type
            };

        }


        #endregion
    }

}
