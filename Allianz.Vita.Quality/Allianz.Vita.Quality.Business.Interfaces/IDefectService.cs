using System.Collections.Generic;

namespace Allianz.Vita.Quality.Business.Interfaces
{
    public interface IDefectService : IService
    {
        List<IDefect> GetMyTasks();
        List<IDefect> GetAllDefects();
        void Save(IDefect model);
    }
}
