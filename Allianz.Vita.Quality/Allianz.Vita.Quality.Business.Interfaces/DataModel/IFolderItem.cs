using System.Collections.Generic;

namespace Allianz.Vita.Quality.Business.Interfaces.DataModel
{
    public interface IFolderItem
	{
		string DisplayName { get; }
		IList<IMailItem> Messages { get; }
	}
}
