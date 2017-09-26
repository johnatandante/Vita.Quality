using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allianz.Vita.Quality.Business.Interfaces
{
    public interface IDefectService : IService
    {
        List<IDefect> GetAllDefects();

    }
}
