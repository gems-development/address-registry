
namespace OSM_client
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            const string url = "https://overpass-api.de/api/interpreter";
            const string path = "osm_data.xml";
            var overpassClient = new OverpassApiClient(url);

            // Запрос к Overpass API
            const string query = "[out:xml];" +
                                 "\nnode(54.979788, 73.414227, 54.983705, 73.423204);" +
                                 "\nout body;" +
                                 "\nway(54.979788, 73.414227, 54.983705, 73.423204);" +
                                 "\nout body;" +
                                 "\nrelation(54.979788, 73.414227, 54.983705, 73.423204);" +
                                 "\nout body;";
        
            try
            {
                var xmlData = await overpassClient.ExecuteOverpassQueryAsync(query);
                await File.WriteAllTextAsync(path, xmlData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
            var osmMiner = new OsmDataProcessor();
            osmMiner.ProcessOsmData(path);
        }
    }
}