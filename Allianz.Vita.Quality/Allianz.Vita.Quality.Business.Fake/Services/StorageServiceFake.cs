using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Allianz.Vita.Quality.Business.Fake.Services
{
    public class StorageServiceFake : IStorageService
    {
        public void EnsurePath(string basePath)
        {
            throw new NotImplementedException();
        }

        public IConfigurationService GetConfiguration()
        {
            throw new NotImplementedException();
        }

        public object GetDataToExport()
        {
            throw new NotImplementedException();
        }

        public byte[] GetDownloadableTextData(object data)
        {
            throw new NotImplementedException();
        }

        public void ImportSettings(string fileName, string basePath)
        {
            throw new NotImplementedException();
        }

        public Task ImportSettings(Stream inputStream)
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

        public bool Store(IMailConfiguration item)
        {
            throw new NotImplementedException();
        }

        public bool Store(IDefectConfiguration item)
        {
            throw new NotImplementedException();
        }
    }
}
