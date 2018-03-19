using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using System;

namespace Allianz.Vita.Quality.Business.Fake.Services
{
    public class StorageServiceFake : IStorageService
    {
        public IConfigurationService GetConfiguration()
        {
            throw new NotImplementedException();
        }

        public string Store(IAttachment att, string fileName)
        {
            throw new NotImplementedException();
        }

        public bool Store(IIssueConfiguration item)
        {
            throw new NotImplementedException();
        }
    }
}
