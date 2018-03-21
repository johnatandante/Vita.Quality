using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Enums;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using Allianz.Vita.Quality.Business.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web;

namespace Allianz.Vita.Quality.Business.Factory
{
    public partial class ItemFactory : IItemFactory
    {

        protected IConfigurationService Config;

        public ItemFactory() : this(null) { }

        public ItemFactory(IConfigurationService config)
        {
            Config = config ?? ServiceFactory.Get<IConfigurationService>();

            Register<IMailItem, MailItem>();
            Register<IFolderItem, FolderItem>();

            Register<IDefect, DefectItem>();
            Register<IIssueItem, IssueItem>();
            
        }

        #region IItemFactory Members

        public IList<IMailItem> GetNewMailItemList()
        {
            return new List<IMailItem>();
        }

        public IMailItem ToMailItem(string uniqueId, string from, string subject, string content, object[] attachments, string[] categories, string importance)
        {
            MailItem instance = GetNew<IMailItem>() as MailItem;

            instance.UniqueId = uniqueId;
            instance.From = from;
            instance.Subject = subject;
            instance.Content = content;
            instance.Attachments = attachments;
            instance.Categories = categories;
            instance.Importance = importance;

            return instance;
        }

        public IFolderItem ToFolderItem(string displayName)
        {
            return GetNew<IFolderItem>(displayName);
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

                SurveySystem = defectSystem ?? config.SurveySystem,
                FoundIn = foundIn ?? config.WebAppId,
                Environment = environment ?? config.Environment,
                DefectType = defectType ?? config.DefectType,

                AreaPath = HttpUtility.UrlDecode(config.AreaPath),
                Iteration = HttpUtility.UrlDecode(config.Iteration),
                State = config.DefectState,
                Severity = (SeverityLevel)Enum.Parse(typeof(SeverityLevel), config.Severity, true),
                
            };

            return defect;
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

        public TInstance GetNew<TInstance>(params object[] parameters) where TInstance : IItem
        {
            return Get<TInstance>(parameters);
        }

        #endregion
    }

}
