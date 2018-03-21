using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Allianz.Vita.Quality.Business.Fake.Services
{
    public class IssueServiceFake : IIssueService
    {
        public Task<IIssueItem> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IIssueItem>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IIssueItem>> GetAllPaged(int page)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUp()
        {
            throw new NotImplementedException();
        }
    }
}
