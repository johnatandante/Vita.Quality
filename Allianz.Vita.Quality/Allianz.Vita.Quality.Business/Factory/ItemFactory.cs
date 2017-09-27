using Allianz.Vita.Quality.Business.Enums;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Models;
using Allianz.Vita.Quality.Business.Utilities;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
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

        #endregion
    }

}
