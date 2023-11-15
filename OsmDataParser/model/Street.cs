using OsmSharp;

namespace OsmDataParser.model;

public class Street
{
    public string Name { get; set; }
    public List<Way> Components = new List<Way>();
}