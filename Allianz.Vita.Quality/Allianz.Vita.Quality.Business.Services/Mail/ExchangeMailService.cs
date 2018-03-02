using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Authentication;

namespace Allianz.Vita.Quality.Business.Services.Mail
{
    public class ExchangeMailService : IMailService
    {
        
        static ExtendedPropertyDefinition PidTagFlagStatus
        {
            get { return new ExtendedPropertyDefinition(0x1090, MapiPropertyType.Integer); }
        }

        IConfigurationService Config
        {
            get { return ServiceFactory.Get<IConfigurationService>(); }
        }

        public ExchangeVersion Version { get; private set; }
        
        static ExchangeService _Service;

        ExchangeService Service
        {
            get
            {
                // Create the binding.
                if (_Service == null)
                    _Service = new ExchangeService(Version);

                // Set the credentials for the on-premises server.
                _Service.Credentials = new WebCredentials(Credentials.UserName, Credentials.Password);
                _Service.Url = new Uri(Config.MailServiceUrl);

                return _Service;
            }
        }

        IItemFactory Factory = null;
        IIdentityService Auth = null;

        NetworkCredential Credentials
        {
            get
            {
                if (!Auth.IsAuthenticatedOn(this.GetType()))
                {
                    throw new AuthenticationException("Identity not found for Exchange");
                }

                return Auth.GetCredentialsFor(this);
            }
        }

        public ExchangeMailService()
            : this(version: ExchangeVersion.Exchange2010_SP2, factory: null, auth: null)
        {

        }

        public ExchangeMailService(ExchangeVersion version, IItemFactory factory, IIdentityService auth)
        {
            
            Factory = factory ?? ServiceFactory.Get<IItemFactory>();
            Auth = auth ?? ServiceFactory.Get<IIdentityService>();
            
            Version = version;
            
        }

        #region IMailService Members

        public List<IMailItem> OpenInbox(int? pageSize = null, bool? read = null)
        {

            ItemView itemView = new ItemView(pageSize ?? int.MaxValue);

            SearchFilter.SearchFilterCollection collection =
                new SearchFilter.SearchFilterCollection(LogicalOperator.And);

            if (read.HasValue)
            {
                collection.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, read.Value));
            }
            
            FindItemsResults<Item> homeItems = Service.FindItems(WellKnownFolderName.Inbox, collection, itemView);
            
            return new List<IMailItem>(
                    homeItems.Select(i => Factory.ToMailItem(i as EmailMessage)
                    ));

        }

        public IFolderItem OpenFolder(string path, int? pageSize = null, string from = "")
        {

            Queue<string> folderNames = new Queue<string>(path.Split('.'));

            string folderKey = "FindFoldersOpenFolder" + path + (pageSize.HasValue ? pageSize.Value.ToString() : string.Empty) + from.ToString();
            
            FindFoldersResults folderResults = FindSubFolder(folderNames);

            if (!folderResults.Any())
                throw new ApplicationException("Errore nel percorso " + path);

            if (folderResults.Folders.Count > 1)
                throw new ApplicationException("Trovati più risultati nel percorso " + path);

            Folder folder = folderResults.Folders.Single();

            ItemView issueVitaItemView = new ItemView(pageSize ?? int.MaxValue);
            issueVitaItemView.PropertySet = new PropertySet(BasePropertySet.FirstClassProperties);
            // issueVitaItemView.PropertySet.RequestedBodyType = BodyType.HTML;

            SearchFilter.SearchFilterCollection collection =
                new SearchFilter.SearchFilterCollection(LogicalOperator.And);
                        
            collection.Add(new SearchFilter.ContainsSubstring(EmailMessageSchema.Subject, "Request"));
            collection.Add(new SearchFilter.ContainsSubstring(EmailMessageSchema.From, "srm@allianz.it", ContainmentMode.Prefixed, ComparisonMode.IgnoreCase));

            collection.Add(new SearchFilter.Not(new SearchFilter.Exists(PidTagFlagStatus)));
            //collection.Add(new SearchFilter.IsNotEqualTo(PidTagFlagStatus, (short)MailFlag.Flagged));
            //collection.Add(new SearchFilter.IsNotEqualTo(PidTagFlagStatus, (short)MailFlag.Complete));

            //string itemsKey = "FindItemsOpenFolder" + path + (pageSize.HasValue ? pageSize.Value.ToString() : string.Empty) + from.ToString() + folder.Id;

            //CacheItem objectIssueVitaItems = cache.GetCacheItem(itemsKey);
            //if (objectIssueVitaItems == null)
            //{
            //    cache.Add(itemsKey, service.FindItems(folder.Id, collection, issueVitaItemView), policy);
            //    objectIssueVitaItems = cache.GetCacheItem(itemsKey);
            //}

            //FindItemsResults<Item> issueVitaItems = objectIssueVitaItems.Value as FindItemsResults<Item>;
            FindItemsResults<Item> issueVitaItems = Service.FindItems(folder.Id, collection, issueVitaItemView);

            return Factory.ToFolderItem(folder, issueVitaItems);

        }

        private FindFoldersResults FindSubFolder(Queue<string> folderNames, FolderId folder = null)
        {
            string folderName = folderNames.Dequeue();

            FolderView folderView = new FolderView(int.MaxValue);
            SearchFilter searchFilter = new SearchFilter.ContainsSubstring(FolderSchema.DisplayName, folderName);

            FindFoldersResults folderResults;

            if (folder == null)
            {
                folderResults = Service.FindFolders(WellKnownFolderName.PublicFoldersRoot
                    , searchFilter, folderView);
            }
            else
            {
                folderResults = Service.FindFolders(folder
                    , searchFilter, folderView);
            }

            if (!folderResults.Any())
                throw new ApplicationException("Non trovata la cartella " + folderName);

            if (folderNames.Any())
            {
                return FindSubFolder(folderNames, folderResults.Folders.First().Id);
            }
            else
            {
                return folderResults;
            }

        }

        EmailMessage GetEmailMessage(IMailItem model, params PropertyDefinitionBase[] additionalProperties)
        {
            if (additionalProperties == null)
                additionalProperties = new PropertyDefinitionBase[] { ItemSchema.Attachments,
                                                ItemSchema.Body,
                                                ItemSchema.Categories,
                                                ItemSchema.Flag,
                                                ItemSchema.Importance,
                                                ItemSchema.ConversationId};

            PropertySet prop = new PropertySet(BasePropertySet.FirstClassProperties, additionalProperties);
            return EmailMessage.Bind(Service, new ItemId(model.UniqueId));

        }

        public IMailItem Get(IMailItem model)
        {
            EmailMessage email = GetEmailMessage(model);
            return Factory.ToMailItem(email, propFull: true);

        }

        public IAttachment GetAsAttachment(IMailItem model)
        {
            EmailMessage message = GetEmailMessage(model);
            string title = message.Subject;
            message.Load(new PropertySet(ItemSchema.MimeContent));

            using (MemoryStream ms = new MemoryStream())
            {
                MimeContent mc = message.MimeContent;
                ms.Write(mc.Content, 0, mc.Content.Length);

                return Factory.ToAttachment(title, ms.ToArray());
            }

        }

        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/office/cc842307.aspx
        /// </summary>
        public void Flag(IMailItem model)
        {

            EmailMessage message = GetEmailMessage(model, ItemSchema.Flag);

            if (Version < ExchangeVersion.Exchange2013)
            {
                
                message.SetExtendedProperty(PidTagFlagStatus, (short)MailFlag.Flagged);

            }
            else
            {

                message.Flag.FlagStatus = ItemFlagStatus.Flagged;
                
            }

            message.Update(ConflictResolutionMode.AutoResolve);

        }

        public void Complete(IMailItem model)
        {
            EmailMessage message = GetEmailMessage(model, ItemSchema.Flag, EmailMessageSchema.ParentFolderId);

            if (Version >= ExchangeVersion.Exchange2013)
            {
                message.Flag.FlagStatus = ItemFlagStatus.Complete;
            }
            else
            {
                message.SetExtendedProperty(PidTagFlagStatus, (short)MailFlag.Complete );
            }

            message.Update(ConflictResolutionMode.AutoResolve);

            // Archive
            //Queue<string> folderNames = new Queue<string>(Config.IssueCompletedFolderPath.Split('.'));
            //FindFoldersResults folderResults = FindSubFolder(folderNames);

            //if (!folderResults.Any())
            //    return;

            //message.Move(folderResults.First().Id);

        }
        

        #endregion
    }
}