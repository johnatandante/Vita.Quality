using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;

namespace Allianz.Vita.Quality.Business.Services.Mail
{
    public class ExchangeMailService : IMailService
    {

        //static ObjectCache cache;

        //CacheItemPolicy policy
        //{
        //    get
        //    {
        //        var p = new CacheItemPolicy();
        //        p.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(120.0);
        //        return p;
        //    }
        //}

        static ExtendedPropertyDefinition PidTagFlagStatus
        {
            get { return new ExtendedPropertyDefinition(0x1090, MapiPropertyType.Integer); }
        }

        IConfigurationService Config
        {
            get { return ServiceFactory.Get<IConfigurationService>(); }
        }

        public ExchangeVersion Version { get; private set; }

        ExchangeService service;

        IItemFactory Factory = null;

        public ExchangeMailService()
            : this(version: ExchangeVersion.Exchange2010_SP2, factory: null)
        {

        }

        public ExchangeMailService(ExchangeVersion version, IItemFactory factory)
        {
            //cache = MemoryCache.Default;

            IConfigurationService config = ServiceFactory.Get<IConfigurationService>();

            string email = config.AccountName;
            string password = config.Password;
            string defaultUrl = config.MailServiceUrl;

            Factory = factory ?? ServiceFactory.Get<IItemFactory>();

            Version = version;

            // Create the binding.
            service = new ExchangeService(version);

            // Set the credentials for the on-premises server.
            service.Credentials = new WebCredentials(email, password);

            // Set the URL.
            service.Url = new Uri(defaultUrl);

        }

        #region IMailService Members

        public List<IMailItem> OpenInbox(int? pageSize = null)
        {

            ItemView itemView = new ItemView(pageSize ?? int.MaxValue);
            
            FindItemsResults<Item> homeItems = service.FindItems(WellKnownFolderName.Inbox, itemView);
            
            return new List<IMailItem>(
                    homeItems.Select(i => Factory.ToMailItem(i as EmailMessage)
                    ));

        }

        public IFolderItem OpenFolder(string path, int? pageSize = null, string from = "")
        {

            Queue<string> folderNames = new Queue<string>(path.Split('.'));

            string folderKey = "FindFoldersOpenFolder" + path + (pageSize.HasValue ? pageSize.Value.ToString() : string.Empty) + from.ToString();
            //CacheItem objectFolderResults = cache.GetCacheItem(folderKey);
            //if (objectFolderResults == null)
            //{
            //    cache.Add(folderKey, FindSubFolder(folderNames), policy);
            //    objectFolderResults = cache.GetCacheItem(folderKey);
            //}

            //FindFoldersResults folderResults = objectFolderResults.Value as FindFoldersResults;
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
            FindItemsResults<Item> issueVitaItems = service.FindItems(folder.Id, collection, issueVitaItemView);

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
                folderResults = service.FindFolders(WellKnownFolderName.PublicFoldersRoot
                    , searchFilter, folderView);
            }
            else
            {
                folderResults = service.FindFolders(folder
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
            return EmailMessage.Bind(service, new ItemId(model.UniqueId));

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