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
        protected override sealed void Seed(ConfigurationDbContext context)
        {
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
                    StartDate=DateTime.Now
                },
            };
            issueConfiguration.ForEach(s => context.IssueConfiguration.Add(s));

            var defectConfiguration = new List<DefectConfigurationDbModel>
            {
                new DefectConfigurationDbModel{
                    CurrentWebAppId = string.Empty,
                    DefaultAreaPath = string.Empty,
                    DefaultDefectState = string.Empty,
                    DefaultDefectType = string.Empty,
                    DefaultDefectWorkItemType = string.Empty,
                    DefaultEnvironment = "Prod",
                    DefaultIteration = string.Empty,
                    DefaultProjectPath = string.Empty,
                    DefaultSeverity = string.Empty,
                    DefaultSurveySystem = string.Empty,
                    TrackingSystemCompany = string.Empty,
                    TrackingSystemUserAreaPath = string.Empty,
                    TrackingSystemWorkingFeature = string.Empty,
                    ServiceName = "Defect",
                    Url = "https://outlook.live.com/EWS/Exchange.asmx",
                    StartDate = DateTime.Now
                },
            };
            defectConfiguration.ForEach(s => context.DefectConfiguration.Add(s));

            context.SaveChanges();
            var mailConfiguration = new List<MailConfigurationDbModel>
            {
                new MailConfigurationDbModel {
                    DefaultSender = "",
                    IssueCompletedFolderPath = "",
                    IssueFolderPath = "",
                    ServiceName = "Mail",
                    Url = "https://outlook.live.com/EWS/Exchange.asmx",
                    StartDate = DateTime.Now
                },
            };
            mailConfiguration.ForEach(s => context.MailConfiguration.Add(s));

            context.AppConfiguration.Add(new ConfigurationDbModel() {
                Defect = defectConfiguration.First(),
                Issue = issueConfiguration.First(),
                Mail = mailConfiguration.First()
            });

            context.SaveChanges();
        }
    }
}
