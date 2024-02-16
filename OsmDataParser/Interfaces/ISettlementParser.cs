using osmDataParser.model;
using OsmDataParser.Support;

namespace osmDataParser.Interfaces;

public interface ISettlementParser
{
    List<Settlement> GetSettlements(OsmData osmData, string areaName);
}