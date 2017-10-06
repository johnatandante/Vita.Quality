﻿using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Utilities;
using Allianz.Vita.Quality.Business.Utilities.Statement;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Allianz.Vita.Quality.Business.Services
{
    public class DefectService : IDefectService
    {
        
        string TfsUri
        {
            get
            {
                return config.TrackingSystemUrl;

            }
        }

        string WorkItemType
        {
            get
            {
                return config.DefaultDefectWorkItemType;

            }
        }

        string TeamProjectName
        {
            get
            {
                return config.DefaultProjectPath;
            }
        }

        static string workItemQuery = "SELECT {0} FROM WorkItems WHERE {1}";

        static string myTaskQueryName = "Assigned to me";

        static string[] WorkItemOutputFields = new string[] {
            "System.Id",
            "System.Title",
            "System.AreaPath" ,
            "System.IterationPath" ,
            "Allianz.Alm.DefectSystem" ,
            "Allianz.Alm.DefectID" ,
            "Microsoft.VSTS.Build.FoundIn" ,
            "Allianz.Alm.Agenzia",
            "Allianz.Alm.environment" ,
            "Allianz.Alm.DefectType",
            "System.State" ,
            "System.Description" ,
            "Microsoft.VSTS.Common.Severity",
            "System.CreatedDate",
            "System.CreatedBy",
        };

        NetworkCredential credentials;

        IConfigurationService config;

        public NetworkCredential Credentials
        {
            get
            {
                if (credentials == null)
                {
                    credentials = new NetworkCredential(
                        config.TrackingSystemAccountName,
                        config.TrackingSystemHashedPassword,
                        config.TrackingSystemDomainName);
                }

                return credentials;
            }
        }

        IItemFactory Factory;
        IMailService Mail;

        public DefectService() : this(null, null) { }

        /// <summary>
        /// Constructor. Manually set values to match your account.
        /// </summary>
        public DefectService(IItemFactory itemFactory, IMailService mail)
        {
            config = ServiceFactory.Get<IConfigurationService>();
            
            Factory = itemFactory ?? ServiceFactory.Get<IItemFactory>();

            Mail = mail ?? ServiceFactory.Get<IMailService>();

        }

        /// <summary>
        /// Execute a WIQL query to return a list of bugs using the .NET client library
        /// </summary>
        /// <returns>List of Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem</returns>
        public List<IDefect> GetMyTasks()
        {
            List<IDefect> result = new List<IDefect>();

            // create TfsTeamProjectCollection instance using default credentials
            using (TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(new Uri(TfsUri)))
            {

                tpc.Credentials = Credentials;

                // get the WorkItemStore service
                WorkItemStore workItemStore = tpc.GetService<WorkItemStore>();
                // get the project context for the work item store
                Project workItemProject = workItemStore.Projects[TeamProjectName];

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
                    WorkItemCollection workItems = workItemStore.Query(newBugsQuery.GetQuery(project: TeamProjectName, user: workItemStore.UserIdentityName));
                    foreach (WorkItem workItem in workItems)
                    {
                        result.Add(Factory.ToDefectItem(workItem));
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

        public List<IDefect> GetAllDefects()
        {
            List<IDefect> result = new List<IDefect>();

            // create TfsTeamProjectCollection instance using default credentials
            using (TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(new Uri(TfsUri)))
            {

                tpc.Credentials = Credentials;

                // get the WorkItemStore service
                WorkItemStore workItemStore = tpc.GetService<WorkItemStore>();
                QueryDefinition query = GetNewQueryDefinition("GetAllDefects",
                    WorkItemOutputFields,
                    Statement.New()
                        .Where("System.TeamProject", "@Project")
                        // .Where("System.AssignedTo", "@Me")
                        .Where("System.WorkItemType", WorkItemType)
                        .WhereNot("System.State", "Resolved")
                        .WhereNot("System.State", "Closed")
                        .WhereNot("System.State", "Retired")
                        .WhereNot("System.State", "Completed")
                        .WhereNot("System.State", "Verified")
                        .WhereNot("Allianz.Alm.DefectID", "")
                        .WhereNot("Allianz.Alm.DefectID", "TBD")
                        .WhereNot("Allianz.Alm.StateGroup", "Complete")
                        .Where("System.Title", "Request", Statement.Op.Contains)
                        .Where("System.AreaPath", "Vita", Statement.Op.Under)
                        .OrderBy("System.CreatedDate", Statement.Order.Descending)
                        //ORDER BY[System.CreatedDate] desc
                        );

                WorkItemCollection workItems = workItemStore.Query(query.GetQuery(project: TeamProjectName, user: workItemStore.UserIdentityName));

                result.AddRange(Factory.ToDefectItemCollection(workItems));

            }

            return result;
        }

        private QueryDefinition GetNewQueryDefinition(string name, string[] outputFields, Statement statementClauses)
        {
            string outputFieldsClause = outputFields.FromClause();
            string statement = statementClauses.ToString();

            return new QueryDefinition(name, string.Format(workItemQuery, outputFieldsClause, statement));
        }

        public void Save(IDefect model)
        {

            using (TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(new Uri(TfsUri)))
            {
                tpc.Credentials = Credentials;

                WorkItemStore workItemStore = tpc.GetService<WorkItemStore>();
                Project project = workItemStore.Projects[TeamProjectName];

                // Create the work item. 
                WorkItem defect = project.WorkItemTypes["Defect"].NewWorkItem();

                // int ? Id { get; }
                defect.Title = model.Title;
                defect.AreaPath = model.AreaPath;
                defect.IterationPath = model.Iteration;

                defect.State = model.State;
                defect.Description = model.Description;

                defect.Fields["Allianz.Alm.DefectSystem"].Value = model.SurveySystem;
                defect.Fields["Allianz.Alm.DefectID"].Value = model.DefectID;
                defect.Fields["Microsoft.VSTS.Build.FoundIn"].Value = model.FoundIn;
                defect.Fields["Allianz.Alm.Agenzia"].Value = model.Agency;
                defect.Fields["Allianz.Alm.environment"].Value = model.Environment;
                defect.Fields["Allianz.Alm.DefectType"].Value = model.DefectType;
                defect.Fields["Microsoft.VSTS.Common.Severity"].Value =
                    defect.Fields["Microsoft.VSTS.Common.Severity"].AllowedValues[(short)model.Severity];

                // Comments
                defect.History = "Created on " + DateTime.Now.Date.ToShortDateString() + " by " + workItemStore.UserIdentityName;

                if (!string.IsNullOrEmpty(model.IMailItemUniqueId))
                {
                    IAttachment att = Mail.GetAsAttachment(Factory.GetNewMailItem(model.IMailItemUniqueId));
                    defect.Attachments.Add(Factory.ToAttachment(att,
                        comment: "Uploaded by " + workItemStore.UserIdentityName + " with Allianz.Vita.Quality Tool",
                        fileName: model.Title.Replace('/', '-') + ".eml"));
                }

                // Links is read only

                // Save the new
                if (!defect.IsValid())
                    throw new ApplicationException("Errore in inserimento Defect " + defect.Title);

                defect.Save();

            }

        }

        public IDefect Get(string id)
        {

            IDefect result = null;

            using (TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(new Uri(TfsUri)))
            {
                tpc.Credentials = Credentials;

                WorkItemStore workItemStore = tpc.GetService<WorkItemStore>();

                QueryDefinition query = GetNewQueryDefinition("GetById",
                    WorkItemOutputFields,
                    Statement.New()
                        .Where("System.Id", id)
                        );

                WorkItemCollection workItems = workItemStore.Query(query.QueryText);
                result = Factory.ToDefectItemCollection(workItems).SingleOrDefault();

            }

            return result;

        }
    }
}