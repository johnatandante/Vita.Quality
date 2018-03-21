using Allianz.Vita.Storage.DataModels.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// EntityFramework.SqlServer' registered in the application config file for the ADO.NET provider with invariant name 
// 'System.Data.SqlClient' could not be loaded. Make sure that the assembly-qualified name is used and that the assembly
// is available to the running application

namespace Allianz.Vita.Storage.DataContext
{
    public class ConfigurationDbContext : DbContext
    {
        static string ContextName = "ConfigurationDbContext";

        public ConfigurationDbContext() 
            : base(ContextName)
        {
        }

        public DbSet<ConfigurationDbModel> AppConfiguration { get; set; }

        public DbSet<IssueConfigurationDbModel> IssueConfiguration { get; set; }
        public DbSet<MailConfigurationDbModel> MailConfiguration { get; set; }
        public DbSet<DefectConfigurationDbModel> DefectConfiguration { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}
