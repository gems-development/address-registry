using osmDataParser.model;
using OsmDataParser.Support;

namespace osmDataParser.Interfaces;

public interface IDistrictParser
{
    List<District> GetDistricts(OsmData osmData, string areaName);
}