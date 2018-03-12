using Allianz.Vita.Quality.Business.Interfaces.DataModel;

namespace Allianz.Vita.Quality.Business.Models
{
    class MailAsAttachment : IAttachment
    {
        public string Title { get; set; }

        public byte[] Content { get; set; }
    }
}
