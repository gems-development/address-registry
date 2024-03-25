using Gems.AddressRegistry.OsmDataParser.Interfaces;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Serializers;
using Gems.AddressRegistry.OsmDataParser.Support;
using NetTopologySuite.Geometries;
using OsmSharp;

namespace Gems.AddressRegistry.OsmDataParser.Parsers;

internal sealed class StreetParser : IOsmParser<Street>
{
    private const float BufferRadius = 0.045F; // 5км
    private readonly Dictionary<long, Node> _nodeCache = new Dictionary<long, Node>();
    private readonly IOsmToGeoJsonConverter _converter = new MultiLineSerializer();
    
    public Street Parse(OsmData osmData, string streetName, string? districtName = null)
    {
        var resultStreet = new Street();
        var streets = ParseAll(osmData);
        foreach (var street in streets)
        {
            if (street.Name == streetName)
                resultStreet = street;
        }

        return resultStreet;
    }
    
    public IReadOnlyCollection<Street> ParseAll(OsmData osmData, string? areaName = null)
    {
        var ways = osmData.Ways;
        var streets = new List<Way>();
        
        foreach (var way in ways)
        {
            if (way.Tags.ContainsKey(OsmKeywords.Highway) && way.Tags.ContainsKey(OsmKeywords.Name))
                streets.Add(way);
        }

        var streetGroup = streets.GroupBy(p => p.Tags[OsmKeywords.Name]);
        var streetList = new List<Street>();

        foreach (var street in streetGroup)
        {
            var group = street.ToList();
            
            if (group.Count < 1 || group is null)
                throw new ArgumentException("Empty street");

            if (group.Count == 1)
            {
                var cleanedName = ObjectNameCleaner.Clean(street.Key);
                var resultStreet = new Street
                {
                    Name = cleanedName,
                    GeoJson = _converter.Serialize(new List<Way> { group.First() }, cleanedName, osmData)
                };
                streetList.Add(resultStreet);
            }
            
            else if (group.Count > 1)
            {
                var osmObjects = OsmParserCore.MergeByMatchingId(group);

                for (int i = 0; i < osmObjects.Count; i++)
                {
                    var streetComponents = new List<Way>();
                    var osmObject = osmObjects[i];
                    streetComponents.Add(osmObject);

                    for (int j = i + 1; j < osmObjects.Count; j++)
                    {
                        var otherOsmObject = osmObjects[j];

                        var wayNode1 = GetWayNode(osmData, osmObject);
                        var wayNode2 = GetWayNode(osmData, otherOsmObject);

                        if (wayNode1 != null && wayNode2 != null)
                        {
                            var point1 = new Point(new Coordinate((double)wayNode1.Longitude!, (double)wayNode1.Latitude!));
                            var point2 = new Point(new Coordinate((double)wayNode2.Longitude!, (double)wayNode2.Latitude!));
                            var buffer = point1.Buffer(BufferRadius, 8);

                            if (buffer.Contains(point2))
                            {
                                streetComponents.Add(otherOsmObject);
                                osmObjects.RemoveAt(j);
                                j--;
                            }
                        }
                    }

                    osmObjects.RemoveAt(i);
                    i--;

                    var cleanedName = ObjectNameCleaner.Clean(street.Key);
                    var newStreet = new Street
                    {
                        Name = cleanedName,
                        GeoJson = _converter.Serialize(streetComponents, cleanedName, osmData)
                    };
                    
                    streetList.Add(newStreet);
                    Console.WriteLine("Объект {" + newStreet.Name + "} добавлен в коллекцию улиц.");
                }
            }
        }
        return streetList;
    }

    private Node? GetWayNode(OsmData osmData, Way way)
    {
        foreach (var nodeId in way.Nodes)
        {
            if (_nodeCache.TryGetValue(nodeId, out var cachedNode))
                return cachedNode;
            
            var wayNode = osmData.Nodes.FirstOrDefault(node => node.Id == nodeId);

            if (wayNode != null)
            {
                _nodeCache[nodeId] = wayNode;
                return wayNode;
            }
        }

        return null;
    }
}