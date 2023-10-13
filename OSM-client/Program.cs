
namespace OSM_client
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var osmMiner = new OsmDataMiner();
            var filePath = "../../../overpass_data.xml";
            osmMiner.ParseOsmData(filePath);
        }
    }
}