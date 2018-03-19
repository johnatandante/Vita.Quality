using Allianz.Vita.Quality.Business.Interfaces.Service;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Web.Mvc;

namespace Allianz.Vita.Quality.Models
{
    public class CredentialsViewModel
    {
        public bool Initialized = false;

        public CredentialsViewModel()
        {
            TFSUserName = TFSPassword = TFSDomainName = string.Empty;
            ExchangeUserName = ExchangePassword = ExchangeDomainName = string.Empty;
            JiraUserName = JiraPassword = string.Empty;
        }

        [Display(Name = "Save Defect System Account")]
        public bool UpdateTfsAccount { get; set; }

        [Display(Name = "User Name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string TFSUserName { get; set; }

        [Display(Name = "User Domain")]
        public string TFSDomainName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string TFSPassword { get; set; }

        [Display(Name = "Save Mail Account")]
        public bool UpdateExchangeAccount { get; set; }

        [Display(Name = "Account Name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string ExchangeUserName { get; set; }

        [Display(Name = "Account Domain")]
        public string ExchangeDomainName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string ExchangePassword { get; set; }
        
        [Display(Name = "Save Issue Tracking Account")]
        public bool UpdateJiraAccount { get; set; }

        [Display(Name = "Ticket Account")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string JiraUserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string JiraPassword { get; set; }

        public NetworkCredential TfsCredentials
        {
            get
            {
                return new NetworkCredential(TFSUserName, TFSPassword, TFSDomainName);
            }
        }

        public NetworkCredential MailCredentials
        {
            get
            {
                return new NetworkCredential(ExchangeUserName, ExchangePassword, ExchangeDomainName);
            }
        }

        public NetworkCredential JiraCredentials
        {
            get
            {
                return new NetworkCredential(JiraUserName, JiraPassword);
            }
        }
    }

    public class IssueCredentialsViewModel : IIssueConfiguration
    {
        public IssueCredentialsViewModel(IIssueConfiguration model)
        {
            MaxPageItems = model.MaxPageItems;
            ReopenedFieldName = model.ReopenedFieldName;
            NomeGruppoLifeFieldName = model.NomeGruppoLifeFieldName;
            DigitalAgencyFieldName = model.DigitalAgencyFieldName;
            WorklogQuery = model.WorklogQuery;
            Url = model.Url;
            ServiceName = model.ServiceName;
        }

        [DisplayName("Query Result Size")]
        public int MaxPageItems { get; set; }

        [DisplayName("Reopened Field Name")]
        public string ReopenedFieldName { get; set; }

        [DisplayName("NomeGruppoLife Field Name")]
        public string NomeGruppoLifeFieldName { get; set; }

        [DisplayName("DigitalAgency Field Name")]
        public string DigitalAgencyFieldName { get; set; }

        [AllowHtml]
        [DisplayName("Worklog Filter Query")]
        public string WorklogQuery { get; set; }

        [DisplayName("Service Name")]
        public string ServiceName { get; set; }

        [AllowHtml]
        public string Url { get; set; }

    }

    public class MailCredentialsViewModel : IMailConfiguration
    {
        public MailCredentialsViewModel(IMailConfiguration model)
        {
            IssueFolderPath = model.IssueFolderPath;
            IssueCompletedFolderPath = model.IssueCompletedFolderPath;
            DefaultSender = model.DefaultSender;
            ServiceName = model.ServiceName;
            Url = model.Url;
        }

        public string IssueFolderPath {get; set; }

        public string IssueCompletedFolderPath {get; set; }

        public string DefaultSender {get; set; }

        public string ServiceName {get; set; }

        public string Url {get; set; }
    }

    public class DefectCredentialsViewModel : IDefectConfiguration
    {
        public DefectCredentialsViewModel(IDefectConfiguration model)
        {
            Url = model.Url;
            ServiceName = model.ServiceName;
        }

        public string DefaultIteration {get; set; }

        public string DefaultAreaPath {get; set; }

        public string DefaultSurveySystem {get; set; }

        public string CurrentWebAppId {get; set; }

        public string DefaultEnvironment {get; set; }

        public string DefaultSeverity {get; set; }

        public string DefaultDefectState {get; set; }

        public string DefaultDefectType {get; set; }

        public string TrackingSystemCompany {get; set; }

        public string DefaultProjectPath {get; set; }

        public string TrackingSystemUserAreaPath {get; set; }

        public string TrackingSystemWorkingFeature {get; set; }

        public string DefaultDefectWorkItemType {get; set; }

        public string ServiceName {get; set; }

        public string Url {get; set; }
    }

}