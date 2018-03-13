using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Allianz.Vita.Quality.Business.Interfaces.Service
{
    public interface IIssueService : IService
    {
        Task<IIssueItem> Get(string id);
        Task<IEnumerable<IIssueItem>> GetAll();
        Task<IEnumerable<IIssueItem>> GetAllPaged(int page);
        Task<bool> IsUp();
    }
}
