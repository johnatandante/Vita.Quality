using Allianz.Vita.Quality.Business.Interfaces.Service;
using System.Collections.Generic;

namespace Allianz.Vita.Quality.Business.Interfaces.DataModel
{
    public interface IFolderItem : IItem
	{
		string DisplayName { get; }
		IList<IMailItem> Messages { get; }
	}
}
