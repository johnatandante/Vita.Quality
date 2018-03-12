using Allianz.Vita.Quality.Business.Interfaces.DataModel;

namespace Allianz.Vita.Quality.Business.Interfaces.Service
{
    public interface IIssueService : IService
    {
        IIssueItem Get(string id);
        IIssueItem[] GetAll();
        IIssueItem[] GetAllPaged(int page);

    }
}
