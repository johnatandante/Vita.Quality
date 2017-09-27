using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Allianz.Vita.Quality.Business.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Allianz.Vita.Quality.Business.Services
{
    public class DefectService : IDefectService
    {

        readonly string _uri;

        static string defaultTfsUri = "http://bretfsas2s01.azgroup.itad.corpnet/tfs";
        //static string defaultDefectWorkItemType = "Defect";
        static string teamProjectName = "Vita";

        static string myTaskQueryName = "Assigned to me";

        static string[] WorkItemOutputFields = new string[] {
            "[System.Title]",
            "[System.AreaPath]" ,
            "[System.Iteration]" ,
            "[System.SurveySystem]" ,
            "[System.DefectID]" ,
            "[System.FoundIn]" ,
            "[System.Agency]",
            "[System.Environment]" ,
            "[System.DefectType]",
            "[System.State]" ,
            "[System.Description]" ,
            "[System.Severity]"
        };

        IItemFactory _ItemFactory;

        public DefectService() : this(null) { }

        /// <summary>
        /// Constructor. Manually set values to match your account.
        /// </summary>
        public DefectService(IItemFactory itemFactory)
        {
            _uri = defaultTfsUri;
            
            _ItemFactory = itemFactory ?? ServiceFactory.Get<IItemFactory>();

        }
        
        /// <summary>
        /// Execute a WIQL query to return a list of bugs using the .NET client library
        /// </summary>
        /// <returns>List of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem</returns>
        public List<IDefect> GetMyTasks()
        {
            List<IDefect> result = new List<IDefect>();

            // create TfsTeamProjectCollection instance using default credentials
            using (TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(new Uri(_uri)))
            {
                WorkItemCollection workItems;

                // get the WorkItemStore service
                WorkItemStore workItemStore = tpc.GetService<WorkItemStore>();
                // get the project context for the work item store
                Project workItemProject = workItemStore.Projects[teamProjectName];
                
                // search for the 'My Queries' folder
                QueryFolder myQueriesFolder = workItemProject
                    .QueryHierarchy
                    .FirstOrDefault(qh => qh is QueryFolder && qh.IsPersonal)
                    as QueryFolder;

                if (myQueriesFolder != null)
                {
                    // search for the 'SOAP Sample' query
                    QueryDefinition newBugsQuery = myQueriesFolder
                        .FirstOrDefault(qi => qi is QueryDefinition && qi.Name.Equals(myTaskQueryName))
                        as QueryDefinition;
                    
                    if (newBugsQuery == null)
                        return result;

                    // run the 'SOAP Sample' query                    
                    workItems = workItemStore.Query(newBugsQuery.GetQuery(project: teamProjectName, user: workItemStore.UserIdentityName));
                    foreach (WorkItem workItem in workItems)
                    {
                        result.Add(_ItemFactory.ToDefectItem(workItem));
                    }

                }

                return result;

            }
            
        }

        [Obsolete("Non utilizzato, anche se valido")]
        private static QueryDefinition CreateNewQuery(Project workItemProject, QueryFolder myQueriesFolder, string queryName)
        {            
            QueryDefinition newBugsQuery = new QueryDefinition(queryName,
                @"SELECT [System.Id],[System.WorkItemType],
                                [System.Title],[System.AssignedTo],[System.State],[System.Tags] 
                              FROM WorkItems WHERE[System.WorkItemType] = '{0}' 
                                    AND[System.State] = 'New'");
            myQueriesFolder.Add(newBugsQuery);
            workItemProject.QueryHierarchy.Save();

            return newBugsQuery;
        }
    }
}
