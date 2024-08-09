using NetTopologySuite.Features;
using NetTopologySuite.IO;
using Gems.AddressRegistry.OsmDataParser.Model;

namespace Gems.AddressRegistry.OsmDataParser.DataGroupingServices;

public static class ObjectContainer
{
	private static readonly GeoJsonReader _geoJsonReader;

	static ObjectContainer() => _geoJsonReader = new GeoJsonReader();

	public static bool Contains(RealObject realObject1, RealObject realObject2)
	{
		var firstGeometry = _geoJsonReader.Read<FeatureCollection>(realObject1.GeoJson);
		var secondGeometry = _geoJsonReader.Read<FeatureCollection>(realObject2.GeoJson);
		var firstFeature = firstGeometry?.FirstOrDefault();
		var secondFeature = secondGeometry?.FirstOrDefault();

		if (firstFeature is null || secondFeature is null)
			return false;

		return firstFeature.Geometry.Contains(secondFeature.Geometry);
	}
}
