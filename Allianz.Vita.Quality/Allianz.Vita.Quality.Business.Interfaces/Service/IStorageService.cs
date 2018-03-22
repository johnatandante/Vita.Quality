using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Allianz.Vita.Quality.Business.Interfaces.Service
{
    public interface IStorageService : IService
    {
        string Store(IAttachment att, string fileName);

        IConfigurationService GetConfiguration();

        bool Store(IIssueConfiguration item);
        bool Store(IMailConfiguration item);
        bool Store(IDefectConfiguration item);
        byte[] GetDownloadableTextData(object data);
        object GetDataToExport();
        void ImportSettings(string fileName, string basePath);
        Task ImportSettings(Stream inputStream);
        void EnsurePath(string basePath);
        object GetErrorDataToExport(Exception e);
    }
}
