using Allianz.Vita.Quality.Business.Interfaces;
using System.IO;

namespace Allianz.Vita.Quality.Business.Services
{
    public class StorageService : IStorageService
    {
        public string Store(IAttachment att, string fileName)
        {
            string fullpath = Path.Combine(Path.GetTempPath(), fileName);
            File.WriteAllBytes(fullpath, att.Content);

            return fullpath;
        }
    }
}
