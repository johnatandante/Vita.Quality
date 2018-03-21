using Allianz.Vita.Quality.Business.Interfaces.Enums;
using Allianz.Vita.Quality.Business.Interfaces.Service;

namespace Allianz.Vita.Quality.Business.Interfaces.DataModel
{
    public interface IDefect : IMailItemKey, IItem
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

        string ConfirmedNotificationAddress { get; set; }

        string Environment { get; }

        string DefectType { get; }

        SeverityLevel Severity { get; set; }

        string State { get; set; }

        string Description { get; set; }

        string[] Comments { get; }

        IAttachment[] Attachment { get; }        

	}
}
