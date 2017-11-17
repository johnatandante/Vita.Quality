using System.ComponentModel;

namespace Allianz.Vita.Quality.Business.Services.Enums
{
    public enum DefectField
    {
        [Description("System.Id")]
        Id,
        [Description("System.Title")]
        Title,
        [Description("System.AreaPath")]
        AreaPath,
        [Description("System.IterationPath")]
        IterationPath,
        [Description("Allianz.Alm.DefectSystem")]
        DefectSystem,
        [Description("Allianz.Alm.DefectID")]
        DefectID,
        [Description("Microsoft.VSTS.Build.FoundIn")]
        FoundIn,
        [Description("Allianz.Alm.Agenzia")]
        Agenzia,
        [Description("Allianz.Alm.environment")]
        environment,
        [Description("Allianz.Alm.DefectType")]
        DefectType,
        [Description("System.State")]
        State,
        [Description("System.Description")]
        Description,
        [Description("Microsoft.VSTS.Common.Severity")]
        Severity,
        [Description("System.CreatedDate")]
        CreatedDate,
        [Description("System.CreatedBy")]
        CreatedBy,
        [Description("System.TeamProject")]
        TeamProject,
        [Description("System.WorkItemType")]
        WorkItemType,
        [Description("Allianz.Alm.StateGroup")]
        StateGroup,
        [Description("System.AssignedTo")]
        AssignedTo,
    }
}
