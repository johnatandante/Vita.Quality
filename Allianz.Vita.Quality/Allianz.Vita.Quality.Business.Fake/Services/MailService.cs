using Allianz.Vita.Quality.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allianz.Vita.Quality.Business.Fake.Services
{
    public class MailService : IMailService
    {
        public string Version => throw new NotImplementedException();

        public IMailItem Get(IMailItem model)
        {
            throw new NotImplementedException();
        }

        public IAttachment GetAsAttachment(IMailItem model)
        {
            throw new NotImplementedException();
        }

        public IFolderItem OpenFolder(string path, int? pageSize = null, string from = "")
        {
            throw new NotImplementedException();
        }

        public List<IMailItem> OpenInbox(int? pageSize = null)
        {
            throw new NotImplementedException();
        }
    }
}
