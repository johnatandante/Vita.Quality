using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using Owin;
using System;

namespace Allianz.Vita.Quality
{
    public partial class Startup
    {
        public void InitOrDie(IAppBuilder app)
        {

            IStorageService storage = ServiceFactory.Get<IStorageService>();
            IItemFactory factory = ServiceFactory.Get<IItemFactory>();
            
            ItemFactory.Register<IDefectConfiguration, DefectConfiguration>();
            ItemFactory.Register<IIssueConfiguration, IssueConfiguration>();
            ItemFactory.Register<IMailConfiguration, MailConfiguration>();
            
            IConfigurationService conf = storage.GetConfiguration();

            IMailConfiguration mailConf = factory.GetNew<IMailConfiguration>();
            storage.Store(mailConf);

            IDefectConfiguration defetctConf = factory.GetNew<IDefectConfiguration>();
            storage.Store(defetctConf);

            IIssueConfiguration issueConf = factory.GetNew<IIssueConfiguration>();
            storage.Store(issueConf);

            IConfigurationService conf2 = storage.GetConfiguration();

        }

        class DefectConfiguration : IDefectConfiguration
        {
            public DefectConfiguration()
            {
                ServiceName = "Defect " + DateTime.Now.ToLongTimeString();
            }

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
        }

        class IssueConfiguration : IIssueConfiguration
        {

            public IssueConfiguration()
            {
                ServiceName = "Issue " + DateTime.Now.ToLongTimeString();
            }

            public int MaxPageItems { get; set; }

            public string ReopenedFieldName { get; set; }

            public string NomeGruppoLifeFieldName { get; set; }

            public string DigitalAgencyFieldName { get; set; }

            public string WorklogQuery { get; set; }

            public string ServiceName { get; set; }

            public string Url { get; set; }
        }

        class MailConfiguration : IMailConfiguration
        {
            public MailConfiguration()
            {
                ServiceName = "Mail " + DateTime.Now.ToLongTimeString();
            }

            public string IssueFolderPath { get; set; }

            public string CompletedFolderPath { get; set; }

            public string DefaultSender { get; set; }

            public string ServiceName { get; set; }

            public string Url { get; set; }
        }

    }
}