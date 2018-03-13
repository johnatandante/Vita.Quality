using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allianz.Vita.Client.Rest.Jira.Api2.Issue.Request
{
    public class IssueRequest
    {
        public string fields;
        public string expand;
        public string properties;

        public static class Fields {
            public static string All = "*all";
        }

        public sealed override string ToString()
        {
            return string.Join("&"
                , "fields=" + fields
                , "expand=" + expand 
                //, "properties=" + properties 
                );
        }

    }
}
