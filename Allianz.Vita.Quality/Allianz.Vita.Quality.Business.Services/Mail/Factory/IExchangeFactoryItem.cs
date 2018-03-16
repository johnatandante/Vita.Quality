using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Microsoft.Exchange.WebServices.Data;

namespace Allianz.Vita.Quality.Business.Services.Mail.Factory
{
    public interface IExchangeFactoryItem : IItemFactory
    {
        IMailItem ToMailItem(EmailMessage mail, bool propFull = false);
        IFolderItem ToFolderItem(Folder folder, FindItemsResults<Item> resultItems = null);

    }
}
