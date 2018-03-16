using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Microsoft.Exchange.WebServices.Data;
using System.Linq;

namespace Allianz.Vita.Quality.Business.Services.Mail.Factory
{
    public class ExchangeFactoryItem : ItemFactory, IExchangeFactoryItem
    {

        public ExchangeFactoryItem() { }
        
        public IMailItem ToMailItem(EmailMessage mail, bool propFull = false)
        {
            return ToMailItem(mail.Id.UniqueId, mail.From.Name, mail.Subject
                , propFull ? mail.Body.Text : string.Empty
                , propFull ? mail.Attachments.ToArray() : new object[] { }
                , propFull ? mail.Categories.ToArray() : new string[] { }
                , propFull ? mail.Importance.ToString() : Importance.Normal.ToString()
                );
        }

        public IFolderItem ToFolderItem(Folder folder, FindItemsResults<Item> resultItems = null)
        {
            IFolderItem folderItem = ToFolderItem(folder.DisplayName);

            if (resultItems != null)
                resultItems
                    .Select(i => ToMailItem(i as EmailMessage))
                    .ToList()
                    .ForEach(item => folderItem.Messages.Add(item));

            return folderItem;
        }

    }
}
