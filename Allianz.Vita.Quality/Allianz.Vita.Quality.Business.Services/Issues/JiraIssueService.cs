using Allianz.Vita.Client.Rest.Jira;
using Allianz.Vita.Client.Rest.Jira.DataModel;
using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Service;
using Allianz.Vita.Quality.Business.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace Allianz.Vita.Quality.Business.Services.Issues
{
    public class JiraIssueService : IIssueService
    {

        static string WorklogQuery = "project in (PRLIFE, NVINDI, HOSTTS, GRLWEB, LAISVC, ATTVIT, GOALPV, LAISBD, CUBDWH, SISCOM, UNIFON) AND created >= startOfYear(-2) AND status != CHIUSA";
        
        public NetworkCredential Credentials
        {
            get
            {
                if (!Auth.IsAuthenticatedOn(this.GetType()))
                {
                    throw new AuthenticationException("Identity not found for TFS");
                }

                return Auth.GetCredentialsFor(this);
            }
        }

        string JiraUri
        {
            get
            {
                return config.IssueSystemUrl;

            }
        }

        int  MaxPageItems
        {
            get
            {
                return config.MaxPageItems;
            }
        }

        IConfigurationService config;

        IIdentityService Auth;

        IItemFactory Factory;

        Jira Service
        {
            get
            {
                return new Jira( new Uri(JiraUri), Credentials);
            }
        }

        public JiraIssueService() : this(itemFactory:null, auth: null) { }

        /// <summary>
        /// Constructor. Manually set values to match your account.
        /// </summary>
        public JiraIssueService(IItemFactory itemFactory,  IIdentityService auth)
        {
            config = ServiceFactory.Get<IConfigurationService>();

            Factory = itemFactory ?? ServiceFactory.Get<IItemFactory>();
            
            Auth = auth ?? ServiceFactory.Get<IIdentityService>();
            
        }
        
        public async Task<IIssueItem> Get(string id)
        {
            using (Jira service = Service)
            {
                await service.Login();
                return ToIssueItem(await service.GetIssueAsync(id));
            }
        }

        private IIssueItem ToIssueItem(Issue item)
        {
            
            DateTime? reopenedOn = item.CustomFields.ToDateTimeNullable("customfield_17407");
            string nomeGruppoLife = item.CustomFields.ToValueString("customfield_11901");
            bool? digitalAgency = item.CustomFields.ToBooleanNullable("customfield_12300");

            return Factory.GetNewIssueItem(item.Key, 
                item.IssueType == null ? string.Empty : item.IssueType.Name,
                item.Assignee == null ? string.Empty : item.Assignee.ToString(),
                item.Priority == null ? string.Empty : item.Priority.Name, 
                item.Project.Name,
                item.Summary, item.Status.Name, item.CreatedDate.Value, item.ResolutionDate,
                reopenedOn, nomeGruppoLife, digitalAgency);
        }

        public async Task<IEnumerable<IIssueItem>> GetAll()
        {
            List<IIssueItem> results = new List<IIssueItem>();

            using (Jira service = Service)
            {
                await service.Login();

                string jqlquery = WorklogQuery;

                Issue[] result = (await service.GetIssuesFromJqlAsync(jqlquery, maxResults: MaxPageItems)).ToArray();

                foreach(Issue item in result)
                {
                    results.Add(ToIssueItem(item));
                }

            }
            
            return results.ToArray();

        }
        public async Task<IEnumerable<IIssueItem>> GetAllPaged(int page)
        {
            List<IIssueItem> results = new List<IIssueItem>();
            int itemIndex = page > 1 ? page * MaxPageItems : 0;

            using (Jira service = Service)
            {

                await service.Login();

                string jqlquery = WorklogQuery;

                Issue[] result = (await service.GetIssuesFromJqlAsync(jqlquery, startAt: itemIndex, maxResults: MaxPageItems)).ToArray();

                foreach (Issue item in result)
                {
                    results.Add(ToIssueItem(item));
                }

            }

            return results.ToArray();
        }

        public async Task<bool> IsUp()
        {
            using (Jira service = Service)
            {
                return await service.IsUp();
            }
         }
    }
}
