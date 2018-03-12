using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using System;

namespace Allianz.Vita.Quality.Business.Fake.Services
{
    public class IssueServiceFake : IIssueService
    {
        public IIssueItem Get(string id)
        {
            throw new NotImplementedException();
        }

        public IIssueItem[] GetAll()
        {
            throw new NotImplementedException();
        }

        public IIssueItem[] GetAllPaged(int page)
        {
            throw new NotImplementedException();
        }
    }
}
