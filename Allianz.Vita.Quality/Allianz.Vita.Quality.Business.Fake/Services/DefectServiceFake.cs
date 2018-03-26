using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using System;
using System.Collections.Generic;

namespace Allianz.Vita.Quality.Business.Fake.Services
{
    public class DefectServiceFake : IDefectService
    {
        public void Autoassign(string id)
        {
            throw new NotImplementedException();
        }

        public IDefect Get(string id)
        {
            throw new NotImplementedException();
        }

        public List<IDefect> GetAllDefects()
        {
            throw new NotImplementedException();
        }

        public string[] GetAllowedValues(Enum field)
        {
            throw new NotImplementedException();
        }

        public string[] GetAllowedValues(string field)
        {
            throw new NotImplementedException();
        }

        public string GetDisplayName()
        {
            throw new NotImplementedException();
        }

        public List<IDefect> GetMyTasks()
        {
            throw new NotImplementedException();
        }

        public string GetTrackingUrlDetail(int? id)
        {
            throw new NotImplementedException();
        }

        public IDefect LookFor(IMailItem mailItem)
        {
            throw new NotImplementedException();
        }

        public void MoveStateOn(IDefect defect)
        {
            throw new NotImplementedException();
        }

        public string Save(IDefect model)
        {
            throw new NotImplementedException();
        }

        public string NotifyReopened(IDefect defect)
        {
            throw new NotImplementedException();
        }

        public IDefect LookFor(string title)
        {
            throw new NotImplementedException();
        }
    }
}
