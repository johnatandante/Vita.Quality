using System.Collections.Generic;

namespace Allianz.Vita.Quality.Business.Interfaces
{
	public interface IMailService : IService
	{

		string Version { get; }
		
		List<IMailItem> OpenInbox(int? pageSize = null);

		IFolderItem OpenFolder(string path, int? pageSize = null, string from = "");

		IMailItem Get(IMailItem model);

        IAttachment GetAsAttachment(IMailItem model);

    }
}
