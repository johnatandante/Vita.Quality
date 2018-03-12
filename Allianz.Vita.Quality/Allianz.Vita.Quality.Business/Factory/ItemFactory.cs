﻿using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Enums;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using Allianz.Vita.Quality.Business.Models;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                ,
                Attachments = propFull ? mail.Attachments.ToArray() : new object[] { }
                ,
                Categories = propFull ? mail.Categories.ToArray() : new string[] { }
                ,
                Importance = propFull ? mail.Importance.ToString() : Importance.Normal.ToString()

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

            string agency =  data.DecodeCodCompany + " " + data.Agency.ToString().PadLeft(4, '0');
            IDefect defect = GetNewDefect(null, agency: agency, defectID: data.Id);
            
            defect.Title = data.Title;
            defect.Description = HttpUtility.UrlDecode(itemRead.Content);
            defect.IMailItemUniqueId = itemRead.UniqueId;

            return defect;

        }

        public IDefect GetNewDefect(int? id = null, string agency = null, string defectID = null, string defectType = null, string defectSystem = null, string foundIn = null, string environment = null)
        {
            IDefect defect = new DefectItem()
            {
                Id = id,
                DefectID = defectID,
                Agency = agency,
                
                SurveySystem = defectSystem ?? Config.DefaultSurveySystem,
                FoundIn = foundIn ?? Config.CurrentWebAppId,
                Environment = environment ?? Config.DefaultEnvironment,                
                DefectType = defectType ?? Config.DefaultDefectType ,
                
                AreaPath = HttpUtility.UrlDecode(Config.DefaultAreaPath),
                Iteration = HttpUtility.UrlDecode(Config.DefaultIteration),
                State = Config.DefaultDefectState,
                Severity = (SeverityLevel)Enum.Parse(typeof(SeverityLevel), Config.DefaultSeverity, true),

                Attachment = new IAttachment[] { } ,
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

            return new IssueItem() {
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
