using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Enums;
using System;
using System.Collections.Generic;

namespace Allianz.Vita.Quality.Business.Interfaces.Service
{
    public interface IDefectService : IService
    {
        List<IDefect> GetMyTasks();
        List<IDefect> GetAllDefects();
        string Save(IDefect model);
        IDefect Get(string id);
        string[] GetAllowedValues(Enum field);
        string[] GetAllowedValues(string field);
        string GetTrackingUrlDetail(int? id);
        void Autoassign(string id);
        void MoveStateOn(IDefect defect);
        IDefect LookFor(IMailItem mailItem);
        IDefect LookFor(string title);
        string NotifyReopened(IDefect defect);
        string GetDisplayName();

    }
}
