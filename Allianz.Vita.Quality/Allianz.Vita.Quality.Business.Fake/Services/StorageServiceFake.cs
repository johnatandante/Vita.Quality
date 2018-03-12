using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using System;

namespace Allianz.Vita.Quality.Business.Fake.Services
{
    public class StorageServiceFake : IStorageService
    {
        public string Store(IAttachment att, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
