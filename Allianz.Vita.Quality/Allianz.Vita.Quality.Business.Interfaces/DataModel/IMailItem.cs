using Allianz.Vita.Quality.Business.Interfaces.Service;

namespace Allianz.Vita.Quality.Business.Interfaces.DataModel
{
    public interface IMailItem : IItem
	{
		string UniqueId { get; }
		string From { get; }
		string Subject { get; }
		string Content { get; }

		bool Flagged { get; }
		object[] Attachments  { get; }
		string[] Categories  { get; }
		string Importance  { get; }
		string ConversationId { get; }

	}
}
