using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Allianz.Vita.Quality.Business.Services
{
    public class DefectService : IDefectService
    {

        readonly string _uri;
        readonly string _personalAccessToken;
        //readonly string _project;
        readonly ICredentials userCredentials;

        static string userName = "le00035";
        static string password = "Filipa52";

        const string defaultDefectWorkItemType = "Defect";
        
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

        public DefectService() 
            : this(null) {
        }

            /// <summary>
            /// Constructor. Manually set values to match your account.
            /// </summary>
        public DefectService(IItemFactory itemFactory)
        {
            _uri = "http://bretfsas2s01.azgroup.itad.corpnet/tfs";
            _personalAccessToken = "personal access token";
            //_project = "project name";

            userCredentials = new NetworkCredential(userName, password);

            _ItemFactory = itemFactory ?? ServiceFactory.Get<IItemFactory>();

        }

        /// <summary>
        /// Execute a WIQL query to return a list of bugs using the .NET client library
        /// </summary>
        /// <returns>List of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem</returns>
        public List<IDefect> GetAllDefects()
        {
            List<IDefect> result = new List<IDefect>();
            List<WorkItem> workItems = new List<WorkItem>();

            Uri uri = new Uri(_uri);
            string personalAccessToken = _personalAccessToken;
            // string project = _project;

            VssBasicCredential credentials = new VssBasicCredential(userCredentials);
            //create a wiql object and build our query
            Wiql wiql = new Wiql()
            {
                Query = "Select [Title], [State]" +
                        "From WorkItems " +
                        "Where [Work Item Type] = '" + defaultDefectWorkItemType + "' " +
                        // "And [System.TeamProject] = '" + project + "' " +
                        "And [System.State] <> 'Closed' " +
                        "Order By [System.Title] Asc, [Changed Date] Desc"
            };
            //create instance of work item tracking http client
            using (WorkItemTrackingHttpClient workItemTrackingHttpClient =
                new WorkItemTrackingHttpClient(uri, credentials))
            {

                //execute the query to get the list of work items in the results
                WorkItemQueryResult queryResult =
                    workItemTrackingHttpClient.QueryByWiqlAsync(wiql).Result;
                
                int[] arr = queryResult.WorkItems.Select(w => w.Id).ToArray();
                
                //get work items for the ids found in query
                workItems.AddRange(workItemTrackingHttpClient
                                        .GetWorkItemsAsync(arr, WorkItemOutputFields, queryResult.AsOf)
                                        .Result);
            }

            workItems.ForEach(w => result.Add(_ItemFactory.ToDefectItem(w)));

            return result;

        }

    }
}
