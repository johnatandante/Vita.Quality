using Allianz.Vita.Quality.Models;

namespace Allianz.Vita.Quality.api.Response
{
    public class IssueResponse : BaseResponse<IssueViewModel[]>
    {
        public sealed override IssueViewModel[] Result { get; set; }

    }
}