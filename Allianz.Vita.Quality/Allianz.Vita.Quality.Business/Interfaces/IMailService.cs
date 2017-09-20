using System.Collections.Generic;

namespace Allianz.Vita.Quality.Business.Interfaces
{
	public interface IMailService
	{

		string Version { get; }
		
		List<IMailItem> OpenInbox(int? pageSize = null);

		IFolderItem OpenFolder(string path, int? pageSize = null);

		IMailItem Get(IMailItem model);

	}
}
