using System.ComponentModel;

namespace Allianz.Vita.Quality.Business.Enums
{
    public enum DefectLinkType
    {
        [Description("System.LinkTypes.Hierarchy-Forward")]
        Parent,

        [Description("System.LinkTypes.Hierarchy-Reverse")]
        Child,

        [Description("System.LinkTypes.Duplicate-Forward")]
        Duplicate,

        [Description("System.LinkTypes.Duplicate-Reverse")]
        DuplicateOf,

        [Description("System.LinkTypes.Related")]
        Related,

        [Description("Microsoft.VSTS.TestCase.SharedParameterReferencedBy ")]
        ReferencedBy

    }
}
