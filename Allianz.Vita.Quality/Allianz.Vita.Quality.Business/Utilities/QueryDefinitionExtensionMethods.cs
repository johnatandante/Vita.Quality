using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allianz.Vita.Quality.Business.Utilities
{
    static class QueryDefinitionExtensionMethods
    {

        public static string GetQuery(this QueryDefinition definition, string project = "", string user = "")
        {
            return definition.QueryText
                .Replace("@project", string.IsNullOrEmpty(project) ? "''" : project.Quoted())
                .Replace("@Project", string.IsNullOrEmpty(project) ? "''" : project.Quoted())
                .Replace("@me", string.IsNullOrEmpty(user) ? "''" : user.Quoted())
                .Replace("@Me", string.IsNullOrEmpty(user) ? "''" :  user.Quoted())
                ;
            
        }

    }
}
