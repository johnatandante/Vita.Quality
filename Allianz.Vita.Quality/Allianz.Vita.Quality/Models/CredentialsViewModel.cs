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

    public class IssueCredentialsViewModel : BaseCredentialsViewModel, IIssueConfiguration
    {
        public IssueCredentialsViewModel() { }

        public IssueCredentialsViewModel(IIssueConfiguration model) : base(model.ServiceName, model.Url)
        {
            MaxPageItems = model.MaxPageItems;
            ReopenedFieldName = model.ReopenedFieldName;
            NomeGruppoLifeFieldName = model.NomeGruppoLifeFieldName;
            DigitalAgencyFieldName = model.DigitalAgencyFieldName;
            WorklogQuery = model.WorklogQuery;
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

    }

    public class MailCredentialsViewModel : BaseCredentialsViewModel, IMailConfiguration
    {
        public MailCredentialsViewModel() { }

        public MailCredentialsViewModel(IMailConfiguration model) : base(model.ServiceName, model.Url)
        {
            IssueFolderPath = model.IssueFolderPath;
            CompletedFolderPath = model.CompletedFolderPath;
            DefaultSender = model.DefaultSender;
        }

        [DisplayName("Issue Folder")]
        public string IssueFolderPath {get; set; }
        
        [DisplayName("Archive Folder")]
        public string CompletedFolderPath {get; set; }
        
        [DisplayName("Sender")]
        public string DefaultSender {get; set; }

    }

    public class DefectCredentialsViewModel : BaseCredentialsViewModel, IDefectConfiguration
    {
        public DefectCredentialsViewModel() { }

        public DefectCredentialsViewModel(IDefectConfiguration model) : base(model.ServiceName, model.Url)
        {
            Url = model.Url;
            ServiceName = model.ServiceName;
        }

        [DisplayName("Iteration")]
        [AllowHtml]
        public string Iteration {get; set; }

        [DisplayName("Area Path")]
        [AllowHtml]
        public string AreaPath {get; set; }

        [DisplayName("Survey System")]
        public string SurveySystem {get; set; }

        [DisplayName("Web App Id")]
        [AllowHtml]
        public string WebAppId {get; set; }

        [DisplayName("Environment")]
        public string Environment {get; set; }

        [DisplayName("Severity")]
        public string Severity {get; set; }

        [DisplayName("Defect State")]
        public string DefectState {get; set; }

        [DisplayName("Defect Type")]
        public string DefectType {get; set; }

        [DisplayName("Company")]
        public string Company {get; set; }

        [DisplayName("Project Path")]
        [AllowHtml]
        public string ProjectPath {get; set; }

        [DisplayName("User Area Path")]
        [AllowHtml]
        public string UserAreaPath {get; set; }

        [DisplayName("Mail Feature")]
        public string WorkingFeature {get; set; }

        [DisplayName("Defect Work Item Type")]
        public string WorkItemType {get; set; }
        
    }

    public class BaseCredentialsViewModel : IConfigurationItem
    {
        public BaseCredentialsViewModel() { }

        public BaseCredentialsViewModel(IConfigurationItem model) : this(model.ServiceName, model.Url) { }

        public BaseCredentialsViewModel(string serviceName, string url)
        {
            ServiceName = serviceName;
            Url = url;
        }

        [DisplayName("Service Name")]
        public string ServiceName { get; set; }

        [AllowHtml]
        public string Url { get; set; }

    }

}