using Allianz.Vita.Quality.Business.Interfaces.Service;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Allianz.Vita.Storage.DataModels.Configuration
{
    [Table("DefectConfiguration")]
    public class DefectConfigurationDbModel : IDefectConfiguration
    {
        public DefectConfigurationDbModel() { }

        public DefectConfigurationDbModel(IDefectConfiguration item)
        {
            Iteration = item.Iteration;
            AreaPath = item.AreaPath;
            SurveySystem = item.SurveySystem;
            WebAppId = item.WebAppId;
            Environment = item.Environment;
            Severity = item.Severity;
            DefectState = item.DefectState;
            DefectType = item.DefectType;
            Company = item.Company;
            ProjectPath = item.ProjectPath;
            UserAreaPath = item.UserAreaPath;
            WorkingFeature = item.WorkingFeature;
            WorkItemType = item.WorkItemType;
            ServiceName = item.ServiceName;
            Url = item.Url;
            StartDate = DateTime.Now;
        }

        [ForeignKey("Configuration")]
        public int Id { get; set; }

        public string Iteration { get; set; }

        public string AreaPath { get; set; }

        public string SurveySystem { get; set; }

        public string WebAppId { get; set; }

        public string Environment { get; set; }

        public string Severity { get; set; }

        public string DefectState { get; set; }

        public string DefectType { get; set; }

        public string Company { get; set; }

        public string ProjectPath { get; set; }

        public string UserAreaPath { get; set; }

        public string WorkingFeature { get; set; }

        public string WorkItemType { get; set; }

        public string ServiceName { get; set; }

        public string Url { get; set; }

        public DateTime StartDate { get; set; }
        
        public int? ConfigurationId { get; set; }

        public virtual ConfigurationDbModel Configuration { get; set; }

    }
}