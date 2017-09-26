using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Allianz.Vita.Quality.Business.Services
{
    public class MailService : IMailService
	{

		public string Version { get; private set; }
		ExchangeService service;

		// Config
		string email = "AZGROUP\\le00035";
		string password = "Filipa52";
		string defaultUrl = "https://cas01.servizi.allianzit/EWS/Exchange.asmx";
		// "https://cas01.servizi.allianzit/ews/Services.wsdl";

		IItemFactory Factory = null;

        public MailService()
            : this(version: ExchangeVersion.Exchange2010_SP2, factory: null)
        {

        }


        public MailService(ExchangeVersion version, IItemFactory factory) {

			Factory = factory ?? ServiceFactory.Get<IItemFactory>();

			Version = version.ToString();

			// Create the binding.
			service = new ExchangeService(version);
			// Set the credentials for the on-premises server.
			service.Credentials = new WebCredentials(email, password);

			// Set the URL.
			service.Url = new Uri(defaultUrl);

		}

		#region IMailService Members

		public List<IMailItem> OpenInbox(int? pageSize = null) {

			ItemView itemView = new ItemView(pageSize ?? int.MaxValue);

			FindItemsResults<Item> homeItems = service.FindItems(WellKnownFolderName.Inbox, itemView);
			
			return new List<IMailItem>(
					homeItems.Select(i => Factory.ToMailItem(i as EmailMessage)
					));

		}
		
		public IFolderItem OpenFolder(string path, int? pageSize = null) {
			
			Queue<string> folderNames = new Queue<string>(path.Split('.'));

			FindFoldersResults folderResults = FindSubFolder(folderNames);

			if (!folderResults.Any())
				throw new ApplicationException("Errore nel percorso " + path);
			
			if(folderResults.Folders.Count > 1)
				throw new ApplicationException("Trovati più risultati nel percorso " + path);

			Folder folder = folderResults.Folders.Single();
			
			ItemView issueVitaItemView = new ItemView(pageSize ?? int.MaxValue);
			issueVitaItemView.PropertySet = new PropertySet(BasePropertySet.FirstClassProperties);
			// issueVitaItemView.PropertySet.RequestedBodyType = BodyType.HTML;

			SearchFilter searchFilter = new SearchFilter.ContainsSubstring(EmailMessageSchema.Subject, "Request");
			FindItemsResults<Item> issueVitaItems = service.FindItems(folder.Id, searchFilter, issueVitaItemView);
			
			return Factory.ToFolderItem(folder, issueVitaItems);

		}

		private FindFoldersResults FindSubFolder(Queue<string> folderNames, FolderId folder = null) {
			string folderName = folderNames.Dequeue();

			FolderView folderView = new FolderView(int.MaxValue);
			SearchFilter searchFilter = new SearchFilter.ContainsSubstring(FolderSchema.DisplayName, folderName);

			FindFoldersResults folderResults;

			if (folder == null) {
				folderResults = service.FindFolders(WellKnownFolderName.PublicFoldersRoot
					, searchFilter, folderView);
			} else {
				folderResults = service.FindFolders(folder
					, searchFilter, folderView);
			}

			if (!folderResults.Any())
				throw new ApplicationException("Non trovata la cartella " + folderName);
			
			if (folderNames.Any()) {
				return FindSubFolder(folderNames, folderResults.Folders.First().Id);
			} else {
				return folderResults;
			}

		}

		public IMailItem Get(IMailItem model) {

			PropertySet prop = new PropertySet(	BasePropertySet.FirstClassProperties,
												ItemSchema.Attachments,
												ItemSchema.Body,
												ItemSchema.Categories,
												ItemSchema.Flag, 
												ItemSchema.Importance,
												ItemSchema.ConversationId,
												ItemSchema.MimeContent);

			EmailMessage email = EmailMessage.Bind(service, new ItemId(model.UniqueId));
			return Factory.ToMailItem(email, propFull: true);

		}

		#endregion
	}
}