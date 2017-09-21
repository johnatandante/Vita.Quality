using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Allianz.Vita.Quality.Business.Enums;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Models;
using Microsoft.Exchange.WebServices.Data;

namespace Allianz.Vita.Quality.Business.Services
{
	public class ItemFactory : IItemFactory
	{
		static ItemFactory _Instance = null;

		public static IItemFactory Instance {
			get {
				if (_Instance == null)
					_Instance = new ItemFactory();

				return _Instance;
			}
		}

		public ItemFactory() {

		}

		#region IItemFactory Members

		public IFolderItem GetNewFolderItem() {
			return new FolderItem();
		}

		public IList<IMailItem> GetNewMailItemList() {
			return new List<IMailItem>();
		}

		public IMailItem ToMailItem(EmailMessage mail, bool propFull = false) {
			return new MailItem() {
				UniqueId = mail.Id.UniqueId
				, From = mail.From.Name
				, Subject = mail.Subject
				, Content = propFull ? mail.Body.Text : string.Empty
				//, Flagged = propFull ? mail.Flag.FlagStatus != ItemFlagStatus.NotFlagged : false
				, Attachments = propFull ? mail.Attachments.ToArray() : new object[] { }
				, Categories = propFull ? mail.Categories.ToArray() : new string[] { }
				, Importance = propFull ? mail.Importance.ToString() : Importance.Normal.ToString()
				//, ConversationId = propFull ? mail.ConversationId.UniqueId : string.Empty
			};

		}

		public IFolderItem ToFolderItem(Folder folder, FindItemsResults<Item> resultItems = null) {
			
			IFolderItem folderItem = new FolderItem() {
				DisplayName = folder.DisplayName
				, Messages = new List<IMailItem>()
			};

			if(resultItems != null)
				resultItems
					.Select(i => ToMailItem(i as EmailMessage))
					.ToList()
					.ForEach(item => folderItem.Messages.Add(item));

			return folderItem;

		}

		public IDefect GetNewDefect(IMailItem itemRead) {

			SubjectMetaData data = new SubjectMetaData(itemRead.Subject);

			IDefect defect = new Defect() {
				Title = data.Title

				, AreaPath = HttpUtility.UrlDecode("Vita\\SUV")

				, SurveySystem = "SRM"

				, DefectID = data.Id

				, FoundIn = "SUV W2003 2.2.72 PR (SUV_20170629.121900)"

				, Agency = data.Company + " " + data.Agency.ToString()

				, Environment = "Prod"

				, Iteration = HttpUtility.UrlDecode("Vita\\Dev\\SUV\\")

				, DefectType = "Altro"

				, Severity = SeverityLevel.Medium
				
				, State = "New"

				, Description = HttpUtility.UrlDecode(itemRead.Content)
				
				, Attachment = new IAttachment[] { }

				, Comments = new string[] { }
		
			};

			return defect;

		}

		#endregion
	}
}
