using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allianz.Vita.Client.Rest.Jira.DataModel
{
    public abstract class Field
    {
        
        public string Name { get; }
        public string Url { get; }

        public Field(ResponseField field)
        {            
            Name = field.name;
            Url = field.self;
        }

    }
}
