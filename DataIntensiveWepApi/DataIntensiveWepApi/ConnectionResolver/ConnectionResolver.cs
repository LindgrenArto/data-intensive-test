namespace DataIntensiveWepApi.ConnectionResolver
{
    public class ConnectionResolver : IConnectionResolver
    {
        private readonly IConfiguration _cfg;
        public ConnectionResolver(IConfiguration cfg) => _cfg = cfg;

        public string GetConnection(DataStore s) => s switch
        {
            DataStore.One => _cfg.GetConnectionString("DbOneConnection")!,
            DataStore.Two => _cfg.GetConnectionString("DbTwoConnection")!,
            DataStore.Three => _cfg.GetConnectionString("DbThreeConnection")!,
            _ => throw new ArgumentOutOfRangeException(nameof(s))
        };
    }
}
