using osmDataParser.model;
using OsmDataParser.Support;

namespace osmDataParser.Interfaces;

public interface IStreetParser
{
    List<Street> GetStreets(OsmData osmData);
}