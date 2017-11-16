using System.Collections.Generic;

namespace Allianz.Vita.Quality.Business.Interfaces
{
	public interface IMailService : IService
	{
        		
		List<IMailItem> OpenInbox(int? pageSize = null, bool? read = null);

		IFolderItem OpenFolder(string path, int? pageSize = null, string from = "");

		IMailItem Get(IMailItem model);

        IAttachment GetAsAttachment(IMailItem model);

        void Flag(IMailItem model);

        void Complete(IMailItem model);

    }
}
