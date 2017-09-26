using Allianz.Vita.Quality.Business.Enums;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Models;
using Allianz.Vita.Quality.Business.Utilities;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Allianz.Vita.Quality.Business.Factory
{
    public class ItemFactory : IItemFactory
	{

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

				, FoundIn = "SUV 2.2.78 PR (SUV_20170925.014104)"

                , Agency = data.DecodeCodCompany + " " + data.Agency.ToString()

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

        public IDefect ToDefectItem(WorkItem workItem)
        {

            Defect defect = new Defect()
            {
                Id = workItem.Id,

                Title = workItem.TryToGetField("System.Title"),

                AreaPath = workItem.TryToGetField("System.AreaPath"),

                Iteration = workItem.TryToGetField("System.Iteration"),

                SurveySystem = workItem.TryToGetField("System.SurveySystem"),

                DefectID = workItem.TryToGetField("System.DefectID"),

                FoundIn = workItem.TryToGetField("System.FoundIn"),

                Agency = workItem.TryToGetField("System.Agency"),

                Environment = workItem.TryToGetField("System.Environment"),

                DefectType = workItem.TryToGetField("System.DefectType"),

                State = workItem.TryToGetField("System.State"),

                Description = workItem.TryToGetField("System.Description"),

                Severity = workItem.TryToGetEnumField<SeverityLevel>("System.Severity"),

                Comments = new string[] { },

                Attachment = new IAttachment[] { },

            };

            return defect;
            
        }

        #endregion
    }

}
