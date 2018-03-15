using System;

namespace Allianz.Vita.Client.Rest.Jira.Api2.Issue.Response
{

    /// <summary>
    /// full representation of the issue for the given issue key
    /// {
    ///    "expand": "renderedFields,names,schema,operations,editmeta,changelog,versionedRepresentations",
    ///    "id": "10002",
    ///    "self": "http://www.example.com/jira/rest/api/2/issue/10002",
    ///    "key": "EX-1",
    ///    "fields": {
    ///        "watcher": {
    ///            "self": "http://www.example.com/jira/rest/api/2/issue/EX-1/watchers",
    ///            "isWatching": false,
    ///            "watchCount": 1,
    ///            "watchers": [
    ///                {
    ///                    "self": "http://www.example.com/jira/rest/api/2/user?username=fred",
    ///                    "name": "fred",
    ///                    "displayName": "Fred F. User",
    ///                    "active": false
    ///                }
    ///            ]
    ///        },
    ///        "attachment": [
    ///            {
    ///                "self": "http://www.example.com/jira/rest/api/2.0/attachments/10000",
    ///                "filename": "picture.jpg",
    ///                "author": {
    ///                    "self": "http://www.example.com/jira/rest/api/2/user?username=fred",
    ///                    "name": "fred",
    ///                    "avatarUrls": {
    ///                        "48x48": "http://www.example.com/jira/secure/useravatar?size=large&ownerId=fred",
    ///                        "24x24": "http://www.example.com/jira/secure/useravatar?size=small&ownerId=fred",
    ///                        "16x16": "http://www.example.com/jira/secure/useravatar?size=xsmall&ownerId=fred",
    ///                        "32x32": "http://www.example.com/jira/secure/useravatar?size=medium&ownerId=fred"
    ///                    },
    ///                    "displayName": "Fred F. User",
    ///                    "active": false
    ///                },
    ///                "created": "2017-12-07T09:23:19.542+0000",
    ///                "size": 23123,
    ///                "mimeType": "image/jpeg",
    ///                "content": "http://www.example.com/jira/attachments/10000",
    ///                "thumbnail": "http://www.example.com/jira/secure/thumbnail/10000"
    ///            }
    ///        ],
    ///        "sub-tasks": [
    ///            {
    ///                "id": "10000",
    ///                "type": {
    ///                    "id": "10000",
    ///                    "name": "",
    ///                    "inward": "Parent",
    ///                    "outward": "Sub-task"
    ///                },
    ///                "outwardIssue": {
    ///                    "id": "10003",
    ///                    "key": "EX-2",
    ///                    "self": "http://www.example.com/jira/rest/api/2/issue/EX-2",
    ///                    "fields": {
    ///                        "status": {
    ///                            "iconUrl": "http://www.example.com/jira//images/icons/statuses/open.png",
    ///                            "name": "Open"
    ///                        }
    ///                    }
    ///                }
    ///            }
    ///        ],
    ///        "description": "example bug report",
    ///        "project": {
    ///            "self": "http://www.example.com/jira/rest/api/2/project/EX",
    ///            "id": "10000",
    ///            "key": "EX",
    ///            "name": "Example",
    ///            "avatarUrls": {
    ///                "48x48": "http://www.example.com/jira/secure/projectavatar?size=large&pid=10000",
    ///                "24x24": "http://www.example.com/jira/secure/projectavatar?size=small&pid=10000",
    ///                "16x16": "http://www.example.com/jira/secure/projectavatar?size=xsmall&pid=10000",
    ///                "32x32": "http://www.example.com/jira/secure/projectavatar?size=medium&pid=10000"
    ///            },
    ///            "projectCategory": {
    ///                "self": "http://www.example.com/jira/rest/api/2/projectCategory/10000",
    ///                "id": "10000",
    ///                "name": "FIRST",
    ///                "description": "First Project Category"
    ///            }
    ///        },
    ///        "comment": [
    ///            {
    ///                "self": "http://www.example.com/jira/rest/api/2/issue/10010/comment/10000",
    ///                "id": "10000",
    ///                "author": {
    ///                    "self": "http://www.example.com/jira/rest/api/2/user?username=fred",
    ///                    "name": "fred",
    ///                    "displayName": "Fred F. User",
    ///                    "active": false
    ///                },
    ///                "body": "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque eget venenatis elit. Duis eu justo eget augue iaculis fermentum. Sed semper quam laoreet nisi egestas at posuere augue semper.",
    ///                "updateAuthor": {
    ///                    "self": "http://www.example.com/jira/rest/api/2/user?username=fred",
    ///                    "name": "fred",
    ///                    "displayName": "Fred F. User",
    ///                    "active": false
    ///                },
    ///                "created": "2017-12-07T09:23:19.175+0000",
    ///                "updated": "2017-12-07T09:23:19.175+0000",
    ///                "visibility": {
    ///                    "type": "role",
    ///                    "value": "Administrators"
    ///                }
    ///            }
    ///        ],
    ///        "issuelinks": [
    ///            {
    ///                "id": "10001",
    ///                "type": {
    ///                    "id": "10000",
    ///                    "name": "Dependent",
    ///                    "inward": "depends on",
    ///                    "outward": "is depended by"
    ///                },
    ///                "outwardIssue": {
    ///                    "id": "10004L",
    ///                    "key": "PRJ-2",
    ///                    "self": "http://www.example.com/jira/rest/api/2/issue/PRJ-2",
    ///                    "fields": {
    ///                        "status": {
    ///                            "iconUrl": "http://www.example.com/jira//images/icons/statuses/open.png",
    ///                            "name": "Open"
    ///                        }
    ///                    }
    ///                }
    ///            },
    ///            {
    ///                "id": "10002",
    ///                "type": {
    ///                    "id": "10000",
    ///                    "name": "Dependent",
    ///                    "inward": "depends on",
    ///                    "outward": "is depended by"
    ///                },
    ///                "inwardIssue": {
    ///                    "id": "10004",
    ///                    "key": "PRJ-3",
    ///                    "self": "http://www.example.com/jira/rest/api/2/issue/PRJ-3",
    ///                    "fields": {
    ///                        "status": {
    ///                            "iconUrl": "http://www.example.com/jira//images/icons/statuses/open.png",
    ///                            "name": "Open"
    ///                        }
    ///                    }
    ///                }
    ///            }
    ///        ],
    ///        "worklog": [
    ///            {
    ///                "self": "http://www.example.com/jira/rest/api/2/issue/10010/worklog/10000",
    ///                "author": {
    ///                    "self": "http://www.example.com/jira/rest/api/2/user?username=fred",
    ///                    "name": "fred",
    ///                    "displayName": "Fred F. User",
    ///                    "active": false
    ///                },
    ///                "updateAuthor": {
    ///                    "self": "http://www.example.com/jira/rest/api/2/user?username=fred",
    ///                    "name": "fred",
    ///                    "displayName": "Fred F. User",
    ///                    "active": false
    ///                },
    ///                "comment": "I did some work here.",
    ///                "updated": "2017-12-07T09:23:19.553+0000",
    ///                "visibility": {
    ///                    "type": "group",
    ///                    "value": "jira-developers"
    ///                },
    ///                "started": "2017-12-07T09:23:19.553+0000",
    ///                "timeSpent": "3h 20m",
    ///                "timeSpentSeconds": 12000,
    ///                "id": "100028",
    ///                "issueId": "10002"
    ///            }
    ///        ],
    ///        "updated": 1,
    ///        "timetracking": {
    ///            "originalEstimate": "10m",
    ///            "remainingEstimate": "3m",
    ///            "timeSpent": "6m",
    ///            "originalEstimateSeconds": 600,
    ///            "remainingEstimateSeconds": 200,
    ///            "timeSpentSeconds": 400
    ///        }
    ///    },
    ///    "names": {
    ///        "watcher": "watcher",
    ///        "attachment": "attachment",
    ///        "sub-tasks": "sub-tasks",
    ///        "description": "description",
    ///        "project": "project",
    ///        "comment": "comment",
    ///        "issuelinks": "issuelinks",
    ///        "worklog": "worklog",
    ///        "updated": "updated",
    ///        "timetracking": "timetracking"
    ///    },
    ///    "schema": {}
    ///}
    /// </summary>
    /// <see cref="https://docs.atlassian.com/software/jira/docs/api/REST/7.6.1/#api/2/issue-getIssue"/>
    /// <seealso cref="http://online.azi.allianzit/jira/rest/api/2/issue/PRLIFE-16338"/>
    public class IssueResponse
    {
        public string expand;
        public string id;
        public string self;
        public string key;
        public dynamic fields;
        public Fields CustomFields;
        
        public class Priority : DataModel.ResponseField
        {

            public string iconUrl;

            public string id;

        }

        public class Issuelink
        {
            public string id;
            public dynamic type;
            public dynamic outwardIssue;
        }

        public class AvatarUrls
        {
            //48x48	"http://online.azi.allianzit/jira/secure/projectavatar?pid=12700&avatarId=10011"
            //24x24	"http://online.azi.allianzit/jira/secure/projectavatar?size=small&pid=12700&avatarId=10011"
            //16x16	"http://online.azi.allianzit/jira/secure/projectavatar?size=xsmall&pid=12700&avatarId=10011"
            //32x32	"http://online.azi.allianzit/jira/secure/projectavatar?size=medium&pid=12700&avatarId=10011"
        }

        public class JiraUser
        {
            public string self;
            public string key;
            public string emailAddress;
            public string displayname;
            public bool active;
            public string timeZone;
            public object avatarUrls = new object();
        }

        public class StatusCategory : DataModel.ResponseField
        {
            public int id;
            public string key;
            public string colorName;
        }

        public class Status : DataModel.ResponseField
        {
            public string description;
            public string iconUrl;
            public string id;
            public StatusCategory statusCategory = new StatusCategory();
        }

        public class Component : DataModel.ResponseField
        {
            public string id;
        }

        public class Subtask { }

        public class Issuetype : DataModel.ResponseField
        {
            public string id;
            public string description;
            public string iconUrl;
            public bool subtask;
            public int avatarId;
        }

        public class ProjectCategory : DataModel.ResponseField
        {
            public string id;
            public string description;
        }

        public class Project : DataModel.ResponseField
        {
            public string id;
            public string key;

            //public AvatarUrls AvatarUrls = new AvatarUrls();	
            public object AvatarUrls = new object();

            public ProjectCategory projectCategory = new ProjectCategory();

        }

        public class Aggregateprogress : Progress { }

        public class Progress
        {
            public int progress;
            public int total;
        }

        public class Watcher
        {
            public string self;
            public int watchCount;
            public bool isWatching;

            public JiraUser[] watchers;

        }

        public class Attachment
        {
            public string self;
            public string id;
            public string filename;
            public JiraUser author;
            public DateTime created;
            public ulong size;
            public string mimeType;
            public string content;
        }

        public class Timetracking { }

        public class Comment
        {
            public object[] comments = new object[] { };
            public int maxResults;
            public int totals;
            public int startAt;
        }

        public class Resolution : DataModel.ResponseField
        {
            public string id;
            public string description;
        }

        public class Fields
        {
            public Resolution resolution = null;
            public DateTime? lastViewed = null;
            public Priority priority = new Priority();
            public string aggregatetimeoriginalestimate = null;
            public string timeestimate = null;
            public Issuelink[] issuelinks = new Issuelink[] { };
            public JiraUser assignee = new JiraUser();
            public Status status = new Status();
            public Component[] components = new Component[] { };
            public string aggregatetimeestimate = null;
            public JiraUser creator = new JiraUser();
            public Subtask[] subtasks = new Subtask[] { };
            public JiraUser reporter = new JiraUser();
            public Aggregateprogress aggregateprogress = new Aggregateprogress();
            public Progress progress = new Progress();
            public Issuetype issuetype = new Issuetype();
            public string timespent = null;
            public Project project = new Project();
            public string aggregatetimespent = null;
            public DateTime? resolutiondate = null;
            public int workratio;
            public Watcher watcher = new Watcher();
            public DateTime created;
            public DateTime updated;
            public string timeoriginalestimate = null;
            public string description;
            public Timetracking timetracking = new Timetracking();
            public Attachment[] attachment = new Attachment[] { };
            public string summary;
            public string duedate = null;
            public Comment comment = new Comment();

        }

    }
}
