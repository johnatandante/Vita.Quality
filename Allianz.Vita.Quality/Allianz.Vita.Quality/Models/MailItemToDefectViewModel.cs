using System.Web.Mvc;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Models;

namespace Allianz.Vita.Quality.Models
{
	public class MailItemToDefectViewModel
	{

		public IDefect Defect { get; set; }

	}

	public class DefectModel : Defect {

		[AllowHtml]
		public string HtmlDescription { get { return Description; } set { Description = value;  } }

		[AllowHtml]
		public string HtmlAreaPath { get { return AreaPath; } set { AreaPath = value; } }

		[AllowHtml]
		public string HtmlIteration { get { return Iteration; } set { Iteration = value; } }

		public DefectModel() { }

		public DefectModel(IDefect defect) {
			
			Title = defect.Title;

			HtmlAreaPath = defect.AreaPath;

			HtmlIteration = defect.Iteration;
			
			SurveySystem = defect.SurveySystem;
			
			DefectID = defect.DefectID;
			
			FoundIn = defect.FoundIn;
			
			Agency= defect.Agency;
			
			Environment = defect.Environment;
			
			DefectType = defect.DefectType;
			
			Severity = defect.Severity;
			
			State = defect.State;

			HtmlDescription = defect.Description;
			
			Comments = defect.Comments;

			Attachment = defect.Attachment;
		}
	}

}