using Allianz.Vita.Quality.Business.Interfaces.Enums;
using Allianz.Vita.Quality.Business.Factory;
using Allianz.Vita.Quality.Business.Interfaces;
using Allianz.Vita.Quality.Business.Services.Enums;
using Allianz.Vita.Quality.Business.Utilities;
using Allianz.Vita.Quality.Business.Utilities.Statement;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Allianz.Vita.Quality.Business.Services.Utilities;

namespace Allianz.Vita.Quality.Business.Services.Defect
{
    public class TfsDefectService : IDefectService
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
            DefectField.Id.FieldName(),
            DefectField.Title.FieldName(),
            DefectField.AreaPath.FieldName(),
            DefectField.IterationPath.FieldName(),
            DefectField.DefectSystem.FieldName(),
            DefectField.DefectID.FieldName(),
            DefectField.FoundIn.FieldName(),
            DefectField.Agenzia.FieldName(),
            DefectField.environment.FieldName(),
            DefectField.DefectType.FieldName(),
            DefectField.State.FieldName(),
            DefectField.Description.FieldName(),
            DefectField.Severity.FieldName(),
            DefectField.CreatedDate.FieldName(),
            DefectField.CreatedBy.FieldName(),
            DefectField.AssignedTo.FieldName(),
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

        IStorageService Storage;

        public TfsDefectService() : this(null, null, null) { }

        /// <summary>
        /// Constructor. Manually set values to match your account.
        /// </summary>
        public TfsDefectService(IItemFactory itemFactory, IMailService mail, IStorageService storage)
        {
            config = ServiceFactory.Get<IConfigurationService>();

            Factory = itemFactory ?? ServiceFactory.Get<IItemFactory>();

            Mail = mail ?? ServiceFactory.Get<IMailService>();

            Storage = storage ?? ServiceFactory.Get<IStorageService>();

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
                        result.Add(ToDefectItem(workItem));
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
                        .Where(DefectField.TeamProject, "@Project")
                        // .Where("System.AssignedTo", "@Me")
                        .Where(DefectField.WorkItemType, WorkItemType)
                        .WhereNot(DefectField.State, "Resolved")
                        .WhereNot(DefectField.State, "Closed")
                        .WhereNot(DefectField.State, "Retired")
                        .WhereNot(DefectField.State, "Completed")
                        .WhereNot(DefectField.State, "Verified")
                        .WhereNot(DefectField.DefectID, "")
                        .WhereNot(DefectField.DefectID, "TBD")
                        .WhereNot(DefectField.StateGroup, "Complete")
                        .Where(DefectField.Title, "Request", Statement.Op.Contains)
                        .Where(DefectField.AreaPath, "Vita", Statement.Op.Under)
                        .OrderBy(DefectField.CreatedDate, Statement.Order.Descending)
                        //ORDER BY[System.CreatedDate] desc
                        );

                WorkItemCollection workItems = workItemStore.Query(query.GetQuery(project: TeamProjectName, user: workItemStore.UserIdentityName));

                result.AddRange(ToDefectItemCollection(workItems));

            }

            return result;
        }

        private IEnumerable<IDefect> ToDefectItemCollection(WorkItemCollection workItems)
        {
            List<IDefect> result = new List<IDefect>();
            foreach (WorkItem workItem in workItems)
            {
                result.Add(ToDefectItem(workItem));
            }

            return result;

        }

        private IDefect ToDefectItem(WorkItem workItem)
        {

            IDefect defect = Factory.GetNewDefect(workItem.Id,
                workItem.TryToGetField(DefectField.Agenzia.FieldName()),
                workItem.TryToGetField(DefectField.DefectID.FieldName()),
                workItem.TryToGetField(DefectField.DefectType.FieldName()),
                workItem.TryToGetField(DefectField.DefectSystem.FieldName()),
                workItem.TryToGetField(DefectField.FoundIn.FieldName()),
                workItem.TryToGetField(DefectField.environment.FieldName()));

            defect.Title =  workItem.TryToGetField(DefectField.Title.FieldName());
            defect.AreaPath = workItem.TryToGetField(DefectField.AreaPath.FieldName());
            defect.Iteration = workItem.TryToGetField(DefectField.IterationPath.FieldName());
            defect.State = workItem.TryToGetField(DefectField.State.FieldName());
            defect.Description = workItem.TryToGetField(DefectField.Description.FieldName());
            defect.Severity = workItem.TryToGetEnumField<SeverityLevel>(DefectField.Severity.FieldName() );
            defect.AssignedTo = workItem.TryToGetField(DefectField.AssignedTo.FieldName());

            return defect;

        }


        private QueryDefinition GetNewQueryDefinition(string name, string[] outputFields, Statement statementClauses)
        {
            string outputFieldsClause = outputFields.FromClause();
            string statement = statementClauses.ToString();

            return new QueryDefinition(name, string.Format(workItemQuery, outputFieldsClause, statement));
        }

        public string Save(IDefect model)
        {
            WorkItem defect;

            using (TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(new Uri(TfsUri)))
            {
                tpc.Credentials = Credentials;

                WorkItemStore workItemStore = tpc.GetService<WorkItemStore>();
                Project project = workItemStore.Projects[TeamProjectName];

                // Create the work item. 
                defect = project
                    .WorkItemTypes[ServiceFactory.Get<IConfigurationService>().DefaultDefectWorkItemType]
                    .NewWorkItem();

                // int ? Id { get; }
                defect.Title = model.Title;

                if (model.AutoAssign)
                {
                    Autoassign(workItemStore, defect);
                } else
                {
                    defect.AreaPath = model.AreaPath;
                    defect.IterationPath = model.Iteration;
                }

                defect.State = model.State;
                defect.Description = model.Description;

                defect.Fields[DefectField.DefectSystem.FieldName()].Value = model.SurveySystem;
                defect.Fields[DefectField.DefectID.FieldName()].Value = model.DefectID;
                defect.Fields[DefectField.FoundIn.FieldName()].Value = model.FoundIn;
                defect.Fields[DefectField.Agenzia.FieldName()].Value = model.Agency;
                defect.Fields[DefectField.environment.FieldName()].Value = model.Environment;
                defect.Fields[DefectField.DefectType.FieldName()].Value = model.DefectType;
                defect.Fields[DefectField.Severity.FieldName()].Value =
                    defect.Fields[DefectField.Severity.FieldName()].AllowedValues[(short)model.Severity];

                // Comments
                defect.History = "Created on " + DateTime.Now.Date.ToShortDateString() + " by " + workItemStore.UserIdentityName;

                if (!string.IsNullOrEmpty(model.IMailItemUniqueId))
                {
                    IAttachment att = Mail.GetAsAttachment(Factory.GetNewMailItem(model.IMailItemUniqueId));
                    defect.Attachments.Add(ToAttachment(att,
                        comment: "Uploaded by " + workItemStore.UserIdentityName + " with Allianz.Vita.Quality Tool",
                        fileName: model.Title.Replace('/', '-') + ".eml"));
                }

                // Links is read only

                // Save the new
                if (!defect.IsValid())
                    throw new ApplicationException("Errore in inserimento Defect " + defect.Title);

                Mail.Complete(Factory.GetNewMailItem(model.IMailItemUniqueId));

                defect.Save();
                
            }

            return defect != null ? defect.Id.ToString() : string.Empty;
        }

        public IDefect Get(string id)
        {

            IDefect result = null;

            using (TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(new Uri(TfsUri)))
            {
                tpc.Credentials = Credentials;

                WorkItemStore workItemStore = tpc.GetService<WorkItemStore>();

                WorkItemCollection workItems = GetWorkItemById(workItemStore, id);

                result = ToDefectItemCollection(workItems).SingleOrDefault();
            }

            return result;

        }

        private WorkItemCollection GetWorkItemById(WorkItemStore workItemStore, string id)
        {
            QueryDefinition query = GetNewQueryDefinition("GetById",
                WorkItemOutputFields,
                Statement.New()
                    .Where(DefectField.Id, id)
                    );

            return workItemStore.Query(query.QueryText);
        }

        static Dictionary<Enum, string[]> _FieldValueCache = new Dictionary<Enum, string[]>();

        public string[] GetAllowedValues(Enum field)
        {
            if (!_FieldValueCache.ContainsKey(field))
                _FieldValueCache.Add(field, GetAllowedValues(field.FieldName()));

            return _FieldValueCache[field];

        }

        string[] GetAllowedValues(string key)
        {
            List<string> allowedValues = new List<string>();

            using (TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(new Uri(TfsUri)))
            {
                tpc.Credentials = Credentials;

                WorkItemStore workItemStore = tpc.GetService<WorkItemStore>();

                if (key == "System.AreaPath")
                {
                    allowedValues.AddRange(Explore(workItemStore.Projects[TeamProjectName].AreaRootNodes));

                }
                else if (key == "System.IterationPath")
                {
                    allowedValues.AddRange(Explore(workItemStore.Projects[TeamProjectName].IterationRootNodes));

                }
                else if (workItemStore.FieldDefinitions.Contains(key))
                {

                    foreach (var field in workItemStore.FieldDefinitions[key].AllowedValues)
                        allowedValues.Add(field.ToString());
                }

            }

            return allowedValues.ToArray();

        }

        private IEnumerable<string> Explore(NodeCollection nodeCollection)
        {
            List<string> nodeList = new List<string>();
            foreach (Node node in nodeCollection)
            {
                nodeList.Add(node.Path);
                if (node.HasChildNodes)
                    nodeList.AddRange(Explore(node.ChildNodes));
            }

            return nodeList;
        }

        public string GetTrackingUrlDetail(int? id)
        {
            IConfigurationService config = ServiceFactory.Get<IConfigurationService>();

            return string.Join("/",
                config.TrackingSystemUrl,
                config.TrackingSystemCompany,
                config.DefaultProjectPath,
                id.HasValue ? "_workItems?id=" + id.Value.ToString() : string.Empty);
        }

        public void Autoassign(string id)
        {
            using (TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(new Uri(TfsUri)))
            {
                tpc.Credentials = Credentials;

                WorkItemStore workItemStore = tpc.GetService<WorkItemStore>();

                WorkItemCollection collection = GetWorkItemById(workItemStore, id);
                if (collection.Count > 0)
                {
                    WorkItem workItem = collection[0];
                    workItem.Open();

                    Autoassign(workItemStore, workItem);
                    
                    if (!workItem.IsValid())
                        throw new ApplicationException("Errore salvataggio " + workItem.Title);
                    
                    workItem.Save();
                }

            }
        }

        private void Autoassign(WorkItemStore workItemStore, WorkItem workItem)
        {
            workItem.Fields[DefectField.AreaPath.FieldName()].Value = config.TrackingSystemUserAreaPath;
            workItem.Fields[DefectField.AssignedTo.FieldName()].Value = workItemStore.UserIdentityName;

            string path = GetCurrentIterationPath(workItemStore);
            workItem.Fields[DefectField.IterationPath.FieldName()].Value = path;

            // Docs Link to w.i. as parent of...
            // https://docs.microsoft.com/en-us/vsts/work/customize/reference/link-type-element-reference                    
            WorkItemLinkTypeEnd linkType = workItemStore.WorkItemLinkTypes.LinkTypeEnds[DefectLinkType.Child.FieldName()];            
            workItem.Links.Add(new RelatedLink(linkType, int.Parse(config.TrackingSystemWorkingFeature)));

        }

        public void MoveStateOn(IDefect defect)
        {
            using (TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(new Uri(TfsUri)))
            {
                tpc.Credentials = Credentials;

                WorkItemStore workItemStore = tpc.GetService<WorkItemStore>();
                WorkItemCollection collection = GetWorkItemById(workItemStore, defect.DefectID);
                WorkItem workItem = collection[0];
                workItem.Open();

                workItem.State = GetNextState(workItem.Fields[DefectField.State.FieldName()]);

                if (!workItem.IsValid())
                    throw new ApplicationException("Errore salvataggio " + workItem.Title);

                workItem.Save();
            }

        }

        private string GetNextState(Field field)
        {
            string value = string.Empty;
            switch(field.Value)
            {
                case "New":
                case "Reopened":
                    value = "In Progress";
                    break;
                case "In Progress":
                    value = "Resolved";
                    break;

            }
            
            return value;
        }

        private string GetCurrentIterationPath(WorkItemStore workItemStore)
        {

            Node node = FindNodeFromPath(workItemStore.Projects[TeamProjectName].IterationRootNodes
                , string.Join("\\", config.DefaultIteration.Split('\\').Skip(1)) );

            if (node != null)
                return Explore(node.ChildNodes).FirstOrDefault();

            throw new ApplicationException("Cannot find current path node: " + config.DefaultIteration);
        }

        private Node FindNodeFromPath(NodeCollection collection, string path)
        {
            Node defaultIterationNode = null;

            string[] splitted = path.Split('\\');
            string currentPath = splitted.FirstOrDefault();
            string remaining = currentPath == null || splitted.Length < 2 ? 
                string.Empty : string.Join("\\", path.Split('\\').Skip(1));
            
            foreach(Node node in collection)
            {
                if (string.IsNullOrEmpty(remaining) && node.Name == currentPath)
                {
                    return node;
                }
                else if (node.Name == currentPath)
                {
                    return FindNodeFromPath(node.ChildNodes, remaining);
                }
                else
                {
                    continue;
                }                
            }

            return defaultIterationNode;
        }

        public IDefect LookFor(IMailItem mailItem)
        {
            IDefect result = null;

            IMailItem mail = Mail.Get(mailItem);
            string subject = Factory.GetSubject(mail);

            using (TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(new Uri(TfsUri)))
            {
                tpc.Credentials = Credentials;

                WorkItemStore workItemStore = tpc.GetService<WorkItemStore>();

                WorkItemCollection workItems = GetWorkItemByTitle(workItemStore, subject);
                                result = ToDefectItemCollection(workItems).FirstOrDefault();
            }

            return result;

        }

        private WorkItemCollection GetWorkItemByTitle(WorkItemStore workItemStore, string title)
        {
            QueryDefinition query = GetNewQueryDefinition("GetByTitle",
                WorkItemOutputFields,
                Statement.New()
                    .Where(DefectField.Title, title)
                    );

            return workItemStore.Query(query.QueryText);
        }

        public string SaveNotify(IDefect model)
        {
            WorkItem workItem;

            using (TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(new Uri(TfsUri)))
            {
                tpc.Credentials = Credentials;

                WorkItemStore workItemStore = tpc.GetService<WorkItemStore>();
                Project project = workItemStore.Projects[TeamProjectName];

                // Create the work item.                 
                workItem = GetWorkItemById(workItemStore, model.Id.Value.ToString())[0] as WorkItem;
                workItem.Open();


                // Comments & state
                if (workItem.State == "Resolved")
                {
                    workItem.State = "Verified";
                    workItem.History = string.Join(Environment.NewLine
                    , "Reopened on " + DateTime.Now.Date.ToShortDateString() + " by " + workItemStore.UserIdentityName
                    , ""
                    , "<em>" + model.Description + "<em>");

                    // Save the new
                    if (!workItem.IsValid())
                        throw new ApplicationException("Errore in verifica Defect " + workItem.Title);

                    workItem.Save();

                    // Reopen
                    workItem.Open();

                    // needs to be reopened
                    workItem.State = "Reopened";

                }
                else
                {

                    if (workItem.State == "Verified")
                    {
                        workItem.State = "Reopened";
                    }
                    
                    workItem.History = string.Join(Environment.NewLine
                    , "Notified on " + DateTime.Now.Date.ToShortDateString() + " by " + workItemStore.UserIdentityName
                    , ""
                    , "<em>" + model.Description + "<em>");

                }
                
                //workItem.Fields[DefectField.Severity.FieldName()].Value = model.Severity;
                    //workItem.Fields[DefectField.Severity.FieldName()].AllowedValues[(short)model.Severity];             

                if (!string.IsNullOrEmpty(model.IMailItemUniqueId))
                {
                    IAttachment att = Mail.GetAsAttachment(Factory.GetNewMailItem(model.IMailItemUniqueId));
                    workItem.Attachments.Add(ToAttachment(att,
                        comment: "Uploaded by " + workItemStore.UserIdentityName + " with Allianz.Vita.Quality Tool",
                        fileName: model.Title.Replace('/', '-') + " - Comunicazione("+ (workItem.Attachments.Count + 1) + ").eml"));
                }

                // Links is read only
                
                // Save the new
                if (!workItem.IsValid())
                    throw new ApplicationException("Errore in aggiornamento Defect " + workItem.Title);

                Mail.Complete(Factory.GetNewMailItem(model.IMailItemUniqueId));

                workItem.Save();
                
            }

            return workItem != null ? workItem.Id.ToString() : string.Empty;

        }

        public string GetDisplayName()
        {
            using (TfsTeamProjectCollection tpc = new TfsTeamProjectCollection(new Uri(TfsUri)))
            {

                tpc.Credentials = Credentials;

                Microsoft.TeamFoundation.Framework.Client.TeamFoundationIdentity identity = null;
                tpc.GetAuthenticatedIdentity(out identity);
                
                return identity.DisplayName;
            }
        }

        private Attachment ToAttachment(IAttachment att, string comment = "", string fileName = "")
        {            
            if (string.IsNullOrEmpty(fileName))
                fileName = "Mail.eml";
            
            return new Attachment(Storage.Store(att, fileName), comment);

        }

    }
}
