using Allianz.Vita.Quality.Business.Interfaces.Enums;

namespace Allianz.Vita.Quality.Business.Interfaces
{
    public interface IDefect : IMailItemKey
    {

        int? Id { get; }
        
        string Title { get; set; }

        bool AutoAssign { get; }

        string AreaPath { get; set; }

        string Iteration { get; set; }

        string SurveySystem { get; }

        string DefectID { get; }

        string FoundIn { get; }

        string Agency { get; }

        string AssignedTo { get; set; }

        string Environment { get; }

        string DefectType { get; }

        SeverityLevel Severity { get; set; }

        string State { get; set; }

        string Description { get; set; }

        string[] Comments { get; }

        IAttachment[] Attachment { get; }
        
	}
}
