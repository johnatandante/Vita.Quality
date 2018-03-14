using Allianz.Vita.Client.Rest.Jira;
using Allianz.Vita.Client.Rest.Jira.DataModel;
using Allianz.Vita.Client.Rest.Jira.DataModel.Auth;
using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Interfaces.DataModel;
using Allianz.Vita.Quality.Business.Interfaces.Service;
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

        static string WorklogQuery = "project in (PRLIFE, NVINDI, HOSTTS, GRLWEB, LAISVC, ATTVIT, GOALPV, LAISBD, CUBDWH, SISCOM, UNIFON) AND issuetype in (\"ISSUE(ANOMALIA)\", \"REQUEST(RICHIESTA)\") AND created >= startOfYear(-2) AND status != CHIUSA ORDER BY created ASC";

        // Issue Fields
        // Id
        // Annullamento dopo verifica(.value)
        // Annullamento dopo verifica(.id)
        // Area
        // SubArea
        // Assegnatario(.name)
        // Assegnatario(.key)
        // Assegnatario(.emailAddress)
        // Assegnatario(.displayName)
        // Assegnatario(.active)
        // Assegnatario lavorazione(.name)
        // Assegnatario lavorazione(.key)
        // Assegnatario lavorazione(.emailAddress)
        // Assegnatario lavorazione(.displayName)
        // Assegnatario lavorazione(.active)
        // Assegnatario verifica(.name)
        // Assegnatario verifica(.key)
        // Assegnatario verifica(.emailAddress)
        // Assegnatario verifica(.displayName)
        // Assegnatario verifica(.active)
        // Pre Assegnatario
        // Pre Assegnatario(.name)
        // Pre Assegnatario(.key)
        // Pre Assegnatario(.emailAddress)
        // Pre Assegnatario(.displayName)
        // Pre Assegnatario(.active)
        // Assegnatario lavorazione
        // Assegnatario verifica
        // Assegnatario
        // Creato
        // Data Risoluzione IT
        // Date Open
        // Date Reopen
        // Esamina(.value)
        // Esamina(.id)
        // Esamina
        // Esamina Richiesta(.value)
        // Esamina Richiesta(.id)
        // Esamina Richiesta
        // KPI Inizio Lavorazione (calculated Consumed)
        // KPI Inizio Lavorazione (calculated RemainingTime)
        // KPI Inizio Lavorazione (calculated DueDate)
        // KPI Inizio Lavorazione (calculated StartDate)
        // KPI Inizio Lavorazione (calculated EndDate)
        // KPI Inizio Lavorazione (calculated Closed)
        // KPI Inizio Lavorazione (calculated Delayed)
        // KPI Inizio Test (calculated Consumed)
        // KPI Inizio Test (calculated RemainingTime)
        // KPI Inizio Test (calculated DueDate)
        // KPI Inizio Test (calculated StartDate)
        // KPI Inizio Test (calculated EndDate)
        // KPI Inizio Test (calculated Closed)
        // KPI Inizio Test (calculated Delayed)
        // KPI Lavorazione (calculated Consumed)
        // KPI Lavorazione (calculated RemainingTime)
        // KPI Lavorazione (calculated DueDate)
        // KPI Lavorazione (calculated StartDate)
        // KPI Lavorazione (calculated EndDate)
        // KPI Lavorazione (calculated Closed)
        // KPI Lavorazione (calculated Delayed)
        // KPI Presa In Carico (post FP) (calculated Consumed)
        // KPI Presa In Carico (post FP) (calculated RemainingTime)
        // KPI Presa In Carico (post FP) (calculated DueDate)
        // KPI Presa In Carico (post FP) (calculated StartDate)
        // KPI Presa In Carico (post FP) (calculated EndDate)
        // KPI Presa In Carico (post FP) (calculated Closed)
        // KPI Presa In Carico (post FP) (calculated Delayed)
        // KPI Presa In Carico (calculated Consumed)
        // KPI Presa In Carico (calculated RemainingTime)
        // KPI Presa In Carico (calculated DueDate)
        // KPI Presa In Carico (calculated StartDate)
        // KPI Presa In Carico (calculated EndDate)
        // KPI Presa In Carico (calculated Closed)
        // KPI Presa In Carico (calculated Delayed)
        // KPI Test (calculated Consumed)
        // KPI Test (calculated RemainingTime)
        // KPI Test (calculated DueDate)
        // KPI Test (calculated StartDate)
        // KPI Test (calculated EndDate)
        // KPI Test (calculated Closed)
        // KPI Test (calculated Delayed)
        // KPI Valutazione (calculated Consumed)
        // KPI Valutazione (calculated RemainingTime)
        // KPI Valutazione (calculated DueDate)
        // KPI Valutazione (calculated StartDate)
        // KPI Valutazione (calculated EndDate)
        // KPI Valutazione (calculated Closed)
        // KPI Valutazione (calculated Delayed)
        // Nome Gruppo Life
        // Priorità(.name)
        // Priorità(.id)
        // Progetto(.id)
        // Progetto(.key)
        // Progetto(.name)
        // Progetto(.Category.id)
        // Progetto(.Category.description)
        // Progetto(.Category.name)
        // Riepilogo
        // Riepilogo()
        // Segnalazione Digital Agency(.value)
        // Segnalazione Digital Agency(.id)
        // Stato(.description)
        // Stato(.name)
        // Stato(.id)
        // Stato(.Category.id)
        // Stato(.Category.key)
        // Stato(.Category.colorName)
        // Stato(.Category.name)
        // Team Accenture
        // Team Allianz
        // Tipo segnalazione(.id)
        // Tipo segnalazione(.description)
        // Tipo segnalazione(.name)
        // Tipo segnalazione(.subtask)
        // Tipo segnalazione(.avatarId)
        // Tipologia issue(.value)
        // Tipologia issue(.id)
        // Tipologia issue
        // Type
        
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
            
            DateTime? reopenedOn = null;
            string nomeGruppoLife = null;
            bool? digitalAgency = null;

            return Factory.GetNewIssueItem(item.Key, item.IssueType.Name,
                item.Assignee.ToString(),
                item.Priority.Name, item.Project.Name,
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
