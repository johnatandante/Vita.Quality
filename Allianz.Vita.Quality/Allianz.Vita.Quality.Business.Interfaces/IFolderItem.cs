using System.Collections.Generic;

namespace Allianz.Vita.Quality.Business.Interfaces
{
    public interface IFolderItem
	{
		string DisplayName { get; }
		IList<IMailItem> Messages { get; }
	}
}
