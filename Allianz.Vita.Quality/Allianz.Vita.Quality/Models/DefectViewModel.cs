using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Interfaces.Enums;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Allianz.Vita.Quality.Models
{
    
	public class DefectViewModel : IDefect {

        public string Url
        {
            get
            {
                IConfigurationService config = ServiceFactory.Get<IConfigurationService>();
                return string.Join("/", 
                    config.TrackingSystemUrl,
                    config.TrackingSystemCompany,
                    config.DefaultProjectPath, 
                    "_workItems?id=" + Id.Value.ToString());
            }
        }

        public int? Id { get; set; }

        public string Title { get; set; }

        [AllowHtml]
        public string AreaPath { get; set; }

        [AllowHtml]
        public string Iteration { get; set; }

        public string SurveySystem { get; set; }

        public string DefectID { get; set; }

        public string FoundIn { get; set; }

        public string Agency { get; set; }

        public string Environment { get; set; }

        public string DefectType { get; set; }

        public SeverityLevel Severity { get; set; }

        public string State { get; set; }

        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Description { get; set; }

        [AllowHtml]
        public string[] Comments { get; set; }

        public IAttachment[] Attachment { get; set; }

        public string IMailItemUniqueId { get; set; }

        public DefectViewModel() { }

		public DefectViewModel(IDefect defect) {

            Id = defect.Id;

			Title = defect.Title;

            AreaPath = defect.AreaPath;

            Iteration = defect.Iteration;
			
			SurveySystem = defect.SurveySystem;
			
			DefectID = defect.DefectID;
			
			FoundIn = defect.FoundIn;
			
			Agency= defect.Agency;
			
			Environment = defect.Environment;
			
			DefectType = defect.DefectType;
			
			Severity = defect.Severity;
			
			State = defect.State;

            Description = defect.Description;
			
			Comments = defect.Comments;

			Attachment = defect.Attachment;

            IMailItemUniqueId = defect.IMailItemUniqueId;

		}
	}

}