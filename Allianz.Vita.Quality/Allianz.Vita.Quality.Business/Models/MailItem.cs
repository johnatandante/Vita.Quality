using Allianz.Vita.Quality.Business.Interfaces;

namespace Allianz.Vita.Quality.Business.Models
{
	public class MailItem : IMailItem, IMailItemKey
	{
		public string UniqueId { get; set; }

		public string From { get; set; }

		public string Subject { get; set; }

		public string Content { get; set; }

		public bool Flagged { get; set; }

		public object[] Attachments { get; set; }

		public string[] Categories { get; set; }

		public string Importance { get; set; }

		public string ConversationId { get; set; }

        public string IMailItemUniqueId
        {
            get
            {
                return UniqueId;
            }

            set
            {
                UniqueId = value;
            }
        }

    }
}
