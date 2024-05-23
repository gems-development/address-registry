using System.Collections.Concurrent;
using NetTopologySuite.Features;
using NetTopologySuite.IO;
using Gems.AddressRegistry.OsmDataParser.Model;

namespace Gems.AddressRegistry.OsmDataParser.DataGroupingServices;

public static class ObjectIntersector
{
	private static readonly ConcurrentDictionary<string, FeatureCollection> _cachedGeometries;
	private static readonly GeoJsonReader _geoJsonReader;

	static ObjectIntersector()
	{
		_cachedGeometries = new ConcurrentDictionary<string, FeatureCollection>();
		_geoJsonReader = new GeoJsonReader();
	}

	public static bool Intersects(RealObject realObject1, RealObject realObject2)
	{
		if (!_cachedGeometries.TryGetValue(realObject1.GeoJson, out var firstGeometry))
		{
			firstGeometry = _geoJsonReader.Read<FeatureCollection>(realObject1.GeoJson);

			_cachedGeometries.AddOrUpdate(realObject1.GeoJson, firstGeometry, (_, collection) => collection);
		}

		if (!_cachedGeometries.TryGetValue(realObject1.GeoJson, out var secondGeometry))
		{
			secondGeometry = _geoJsonReader.Read<FeatureCollection>(realObject2.GeoJson);

			_cachedGeometries.AddOrUpdate(realObject2.GeoJson, secondGeometry, (_, collection) => collection);
		}

		var firstFeature = firstGeometry?.FirstOrDefault();
		var secondFeature = secondGeometry?.FirstOrDefault();

		if (firstFeature is null || secondFeature is null)
			return false;

		return firstFeature.Geometry.Contains(secondFeature.Geometry);
	}
}
