namespace Allianz.Vita.Client.Rest.Jira.Api2.Issue.Request
{
    public class IssueRequest : DataModel.Intefaces.RequestItem
    {
        public string fields;
        public string expand;
        public string properties;
        
        public static class Fields {
            public static string All = "*all";
        }

        public override sealed string ToString()
        {
            return string.Join(",", "fields=" + fields
                                   , "expand=" + expand);
        }
                
    }
}
