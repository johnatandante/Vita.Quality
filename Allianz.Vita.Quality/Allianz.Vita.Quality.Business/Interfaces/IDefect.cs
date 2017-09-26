using Allianz.Vita.Quality.Business.Enums;

namespace Allianz.Vita.Quality.Business.Interfaces
{
	public interface IDefect
	{

        int? Id { get; }
		string Title { get; }

		string AreaPath { get; }

		string Iteration { get; }

		string SurveySystem { get; }

		string DefectID { get; }

		string FoundIn { get; }

		string Agency { get; }

		// string AssignedTo { get; }

		string Environment { get; }

		string DefectType { get; }

		SeverityLevel Severity { get; }

		string State { get; }

		string Description { get; }

		string[] Comments { get; }

		IAttachment[] Attachment { get; }
		
	}
}
