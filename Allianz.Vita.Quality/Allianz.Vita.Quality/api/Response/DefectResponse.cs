using Allianz.Vita.Quality.Models;

namespace Allianz.Vita.Quality.api.Response
{
    public class DefectResponse : BaseResponse<DefectViewModel[]>
    {
        public sealed override DefectViewModel[] Result { get; set; }

    }
}