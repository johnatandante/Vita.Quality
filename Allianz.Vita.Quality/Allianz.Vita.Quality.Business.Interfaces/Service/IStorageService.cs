using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allianz.Vita.Quality.Business.Interfaces.Service
{
    public interface IStorageService : IService
    {
        string Store(IAttachment att, string fileName);
    }
}
