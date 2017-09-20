using Allianz.Vita.Quality.Business.Enums;
using Allianz.Vita.Quality.Business.Interfaces;

namespace Allianz.Vita.Quality.Business.Models
{
	public class Defect : IDefect
	{

		#region IDefect Members

		public string Title { get; set;}

		public string AreaPath { get; set; }
		
		public string Iteration { get; set; }

		public string SurveySystem { get; set; }

		public string DefectID { get; set; }

		public string FoundIn { get; set; }

		public string Agency { get; set; }

		public string Environment { get; set; }

		public string DefectType { get; set; }

		public SeverityLevel Severity { get; set; }

		public string State { get; set; }

		public string Description { get; set; }

		public string[] Comments { get; set; }

		public IAttachment[] Attachment { get; set; }

		#endregion
	}
}
