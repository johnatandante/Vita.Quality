using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using System;
using System.Collections.Generic;

namespace Allianz.Vita.Quality.Business.Fake.Services
{
    public class MailServiceFake : IMailService
    {
        public void Complete(IMailItem model)
        {
            throw new NotImplementedException();
        }

        public void Flag(IMailItem itemRead)
        {
            throw new NotImplementedException();
        }

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

        public List<IMailItem> OpenInbox(int? pageSize = null, bool? read = null)
        {
            throw new NotImplementedException();
        }
    }
}
