namespace Allianz.Vita.Quality.Business.Interfaces
{
    public interface IMailItem
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
