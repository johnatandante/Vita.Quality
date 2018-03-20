using Allianz.Vita.Storage.DataContext;
using Allianz.Vita.Storage.DataModels.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Allianz.Vita.Storage.Initializer
{

    public class ConfigurationInitializer : DropCreateDatabaseIfModelChanges<ConfigurationDbContext>
    {
        public override sealed void InitializeDatabase(ConfigurationDbContext context)
        {
            base.InitializeDatabase(context);

        }        

        protected override sealed void Seed(ConfigurationDbContext context)
        {
            var conf = new ConfigurationDbModel() {
                StartDate = DateTime.Now
            };

            var issueConfiguration = new List<IssueConfigurationDbModel>
            {
                new IssueConfigurationDbModel{
                    DigitalAgencyFieldName = "",
                    MaxPageItems =15,
                    NomeGruppoLifeFieldName = "",
                    ReopenedFieldName = "",
                    ServiceName = "Issue",
                    Url = "",
                    WorklogQuery = "created >= startOfYear()",
                    StartDate=DateTime.Now,
                    Configuration = conf                   
                },
            };
            issueConfiguration.ForEach(s => context.IssueConfiguration.Add(s));

            var defectConfiguration = new List<DefectConfigurationDbModel>
            {
                new DefectConfigurationDbModel{
                    WebAppId = string.Empty,
                    AreaPath = string.Empty,
                    DefectState = string.Empty,
                    DefectType = string.Empty,
                    WorkItemType = string.Empty,
                    Environment = "Prod",
                    Iteration = string.Empty,
                    ProjectPath = string.Empty,
                    Severity = string.Empty,
                    SurveySystem = string.Empty,
                    Company = string.Empty,
                    UserAreaPath = string.Empty,
                    WorkingFeature = string.Empty,
                    ServiceName = "Defect",
                    Url = "",
                    StartDate = DateTime.Now,
                    Configuration = conf
                },
            };
            defectConfiguration.ForEach(s => context.DefectConfiguration.Add(s));

            var mailConfiguration = new List<MailConfigurationDbModel>
            {
                new MailConfigurationDbModel {
                    DefaultSender = "",
                    CompletedFolderPath = "",
                    IssueFolderPath = "",
                    ServiceName = "Mail",
                    Url = "https://outlook.live.com/EWS/Exchange.asmx",
                    StartDate = DateTime.Now,
                    Configuration = conf
                },
            };
            mailConfiguration.ForEach(s => context.MailConfiguration.Add(s));

            context.AppConfiguration.Add(conf);

            context.SaveChanges();
        }
    }
}
