using System.Text.Json;
using Gems.AddressRegistry.OsmDataParser.Factories;
using Gems.AddressRegistry.OsmDataParser.Interfaces;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Support;
using GeoJSON.Text.Feature;
using GeoJSON.Text.Geometry;
using Microsoft.Extensions.Logging;
using Moq;
using OsmSharp;
using OsmSharp.Tags;

namespace Gems.AddressRegistry.OsmDataParser.Tests;

public class DistrictParserTests
{
    private readonly IOsmParser<District> _districtParser = OsmParserFactory.Create<District>();
    private readonly OsmData _osmData = new OsmData();
    private readonly ILogger _logger = new Mock<ILogger>().Object;

    [Fact]
    public void GetDistricts_Success()
    {
        //Arrange.
        var tags = new TagsCollection
        {
            {"boundary", "administrative"},
            {"admin_level", "6"},
            {"name", "Омский район"}
        };

        var node1 = TestHelper.CreateNode(123, 73.1, 54.1);
        var node2 = TestHelper.CreateNode(456, 73.2, 54.2);
        var node3 = TestHelper.CreateNode(789, 73.3, 54.3);
        var node4 = TestHelper.CreateNode(315, 73.5, 54.5);
        var node5 = TestHelper.CreateNode(811, 73.6, 54.6);
        var node6 = TestHelper.CreateNode(563, 73.7, 54.7);
        var node7 = TestHelper.CreateNode(353, 73.8, 54.8);
        var node8 = TestHelper.CreateNode(888, 73.4, 54.4);
        
        var way1 = TestHelper.CreateWay(1, null!, 
            new [] { (long)node1.Id!, (long)node2.Id!, (long)node3.Id! });
        
        var way2 = TestHelper.CreateWay(2, null!,
            new [] { (long)node4.Id!, (long)node5.Id!, (long)node6.Id! });
        
        var way3 = TestHelper.CreateWay(3, null!,
            new [] { (long)node6.Id!, (long)node7.Id!, (long)node1.Id! });
        
        var way4 = TestHelper.CreateWay(4, null!,
            new [] { (long)node4.Id!, (long)node8.Id!, (long)node3.Id! });
        
        var districtRelation = TestHelper.CreateRelation(999, tags, 
            new []
            {
                new RelationMember { Id = (long)way1.Id! },
                new RelationMember { Id = (long)way2.Id! },
                new RelationMember { Id = (long)way3.Id! },
                new RelationMember { Id = (long)way4.Id! },
            }
        );
        
        var areaTags = new TagsCollection
        {
            {"boundary", "administrative"},
            {"admin_level", "4"},
            {"name", "Омская область"}
        };

        var areaRelation = TestHelper.CreateRelation(1, areaTags,
            new[]
            {
                new RelationMember { Id = (long)districtRelation.Id! }
            }
        );
        
        var expectedCoordinates = new List<(double latitude, double longitude)>
        {
            (73.3, 54.3),
            (73.4, 54.4),
            (73.5, 54.5),
            (73.6, 54.6),
            (73.7, 54.7),
            (73.8, 54.8),
            (73.1, 54.1),
            (73.2, 54.2),
            (73.3, 54.3)
        };
        
        //Act.
        _osmData.Nodes.Add(node1);
        _osmData.Nodes.Add(node2);
        _osmData.Nodes.Add(node3);
        _osmData.Nodes.Add(node4);
        _osmData.Nodes.Add(node5);
        _osmData.Nodes.Add(node6);
        _osmData.Nodes.Add(node7);
        _osmData.Nodes.Add(node8);
        
        _osmData.Ways.Add(way1);
        _osmData.Ways.Add(way2);
        _osmData.Ways.Add(way3);
        _osmData.Ways.Add(way4);
        
        _osmData.Relations.Add(areaRelation);
        _osmData.Relations.Add(districtRelation);
        
        var districts = _districtParser.ParseAll(_osmData, _logger, "Омская область");
        
        var districtGeometry = JsonSerializer.Deserialize<FeatureCollection>(districts.First().GeoJson);
        var multiPolygon = districtGeometry!.Features.First().Geometry as MultiPolygon;
        var positions = multiPolygon!.Coordinates.First().Coordinates.First().Coordinates;
        var nodes = positions.Select(position 
            => TestHelper.CreateNode(default, position.Latitude, position.Longitude)).ToList();
        
        //Assert.
        Assert.NotNull(districtGeometry);
        Assert.NotNull(multiPolygon);
        Assert.NotNull(positions);
        Assert.NotNull(nodes);
        
        Assert.Equal(expectedCoordinates.Count, nodes.Count);
        
        for (var i = 0; i < expectedCoordinates.Count; i++)
        {
            Assert.Equal(expectedCoordinates[i].latitude, nodes[i].Latitude);
            Assert.Equal(expectedCoordinates[i].longitude, nodes[i].Longitude);
        }
    }
}