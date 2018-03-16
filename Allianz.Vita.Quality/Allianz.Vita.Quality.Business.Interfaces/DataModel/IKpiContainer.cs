namespace Allianz.Vita.Quality.Business.Interfaces.DataModel
{
    public class IKpiContainer
    {
        IKpiItem PresaInCarico { get; }
        IKpiItem InizioLavorazione { get; }
        IKpiItem Lavorazione { get; }
        IKpiItem InizioTest { get; }
        IKpiItem Test { get; }
    }
}
