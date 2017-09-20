using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Models;

namespace Allianz.Vita.Quality.Models
{
	public class MailItemToDefectViewModel
	{

		public DefectModel Defect { get; set; }

	}

	public class DefectModel : Defect { }

}