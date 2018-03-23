using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Enums;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using Allianz.Vita.Quality.Business.Services.Enums;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Allianz.Vita.Quality.Models
{

    public class DefectViewModel : IDefect, IMailItemKey {

        IDefectService Service {
            get
            {
                return ServiceFactory.Get<IDefectService>();
            }
        }

        IConfigurationService Config
        {
            get
            {
                return ServiceFactory.Get<IConfigurationService>();
            }
        }

        [JsonIgnore]
        public bool IsAutoAssigned
        {
            get
            {
                // "Dante Del Favero (Leaesed Employee)"
                return Service.GetDisplayName() == AssignedTo;
            }
        }

        [DisplayName("Prendi in carico")]
        [JsonIgnore]
        public bool AutoAssign { get; set; }        

        public string Url
        {
            get
            {
                return Service.GetTrackingUrlDetail(Id);                
            }
        }

        public int? Id { get; set; }

        public string Title { get; set; }

        public string AssignedTo { get; set; }

        [JsonIgnore]
        public string[] AreaPathAllowedValues
        {
            get
            {
                return Service.GetAllowedValues(DefectField.AreaPath);
            }
        }

        [AllowHtml]
        [DisplayName("Area Path")]
        public string AreaPath { get; set; }

        [JsonIgnore]
        public string[] IterationAllowedValues
        {
            get
            {
                return Service.GetAllowedValues(DefectField.IterationPath);
            }
        }

        [AllowHtml]
        public string Iteration { get; set; }

        [DisplayName("Survey System")]
        public string SurveySystem { get; set; }
        
        [DisplayName("Defect ID")]
        public string DefectID { get; set; }

        [JsonIgnore]
        public string[] FoundInAllowedValues
        {
            get
            {
                return Service.GetAllowedValues(DefectField.FoundIn);
            }
        }

        [DisplayName("Found In")]
        public string FoundIn { get; set; }

        public string Agency { get; set; }

        public string Environment { get; set; }

        [JsonIgnore]
        public string[] DefectTypeAllowedValues
        {
            get
            {
                return Service.GetAllowedValues(DefectField.DefectType);
            }
        }

        [DisplayName("Defect Type")]
        public string DefectType { get; set; }

        public SeverityLevel Severity { get; set; }

        [JsonIgnore]
        public string[] SeverityAllowedValues
        {
            get
            {
                return Service.GetAllowedValues(DefectField.Severity);
            }
        }

        public string State { get; set; }
        
        [UIHint("tinymce_jquery_full"), AllowHtml]
        [JsonIgnore]
        public string Description { get; set; }

        [AllowHtml]
        [JsonIgnore]
        public string[] Comments { get; set; }

        [JsonIgnore]
        public IAttachment[] Attachment { get; set; }

        public string IMailItemUniqueId { get; set; }

        [DisplayName("Email")]
        public string ConfirmedNotificationAddress { get; set; }

        public DefectViewModel() { }

		public DefectViewModel(IDefect defect) {

            Id = defect.Id;

			Title = defect.Title;

            AutoAssign = defect.AutoAssign;
            
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

            AssignedTo = defect.AssignedTo;

            ConfirmedNotificationAddress = defect.ConfirmedNotificationAddress;

        }
	}

}