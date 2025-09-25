namespace DataIntensiveWepApi.ConnectionResolver
{
    public interface IConnectionResolver
    {
        string GetConnection(DataStore store);
    }
}
