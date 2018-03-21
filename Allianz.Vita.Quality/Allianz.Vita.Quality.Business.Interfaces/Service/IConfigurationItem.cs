namespace Allianz.Vita.Quality.Business.Interfaces.Service
{
    public interface IConfigurationItem : IItem
    {
        string ServiceName { get; }
        string Url { get; }
    }
}
