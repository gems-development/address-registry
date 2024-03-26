using System.Text.Json;
using Gems.AddressRegistry.OsmDataParser.Factories;
using Gems.AddressRegistry.OsmDataParser.Interfaces;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Support;
using GeoJSON.Text.Feature;
using GeoJSON.Text.Geometry;
using OsmSharp.Tags;

namespace Gems.AddressRegistry.OsmDataParser.Tests;

public class StreetParserTests
{
    private readonly IOsmParser<Street> _streetParser = OsmParserFactory.Create<Street>();
    private readonly OsmData _osmData = new OsmData();
    
    [Fact]
    public void GetInseparableStreetByTwoWays()
    {
        //Arrange.
        var tags = new TagsCollection
        {
            {"highway", "residential"},
            {"name", "улица Ленина"}
        };

        var node1 = TestHelper.CreateNode(123, 73.3333333, 54.3333333);
        var node2 = TestHelper.CreateNode(456, 73.3333334, 54.3333334);
        var node3 = TestHelper.CreateNode(789, 73.3333335, 54.3333335);
        var node4 = TestHelper.CreateNode(121, 73.3333336, 54.3333336);
        var node5 = TestHelper.CreateNode(111, 73.3333337, 54.3333337);
    
        var way1 = TestHelper.CreateWay(1, tags,
            new [] { (long)node1.Id!, (long)node2.Id!, (long)node3.Id! });
        
        var way2 = TestHelper.CreateWay(2, tags,
            new [] { (long)node4.Id!, (long)node5.Id!, (long)node1.Id! });
        
        var expectedCoordinates = new List<(double latitude, double longitude)>
        {
            (73.3333336, 54.3333336),
            (73.3333337, 54.3333337),
            (73.3333333, 54.3333333),
            (73.3333334, 54.3333334),
            (73.3333335, 54.3333335)
        };
    
        //Act.
        _osmData.Nodes.Add(node1);
        _osmData.Nodes.Add(node2);
        _osmData.Nodes.Add(node3);
        _osmData.Nodes.Add(node4);
        _osmData.Nodes.Add(node5);
        
        _osmData.Ways.Add(way1);
        _osmData.Ways.Add(way2);
        
        var streets = _streetParser.ParseAll(_osmData, null!);
        
        var streetGeometry = JsonSerializer.Deserialize<FeatureCollection>(streets.First().GeoJson);
        var multiLine = streetGeometry!.Features.First().Geometry as MultiLineString;
        var positions = multiLine!.Coordinates.First().Coordinates;
        var nodes = positions.Select(position 
            => TestHelper.CreateNode(default, position.Latitude, position.Longitude)).ToList();
        
        //Assert.
        Assert.NotNull(streetGeometry);
        Assert.NotNull(multiLine);
        Assert.NotNull(positions);
        Assert.NotNull(nodes);
        
        Assert.Equal(expectedCoordinates.Count, nodes.Count);
        
        for (var i = 0; i < expectedCoordinates.Count; i++)
        {
            Assert.Equal(expectedCoordinates[i].latitude, nodes[i].Latitude);
            Assert.Equal(expectedCoordinates[i].longitude, nodes[i].Longitude);
        }
    }
    
    [Fact]
    public void GetInseparableStreetBySeveralWays_Sequential()
    {
        //Arrange.
        var tags = new TagsCollection
        {
            {"highway", "residential"},
            {"name", "улица Ленина"}
        };
        
        var node1 = TestHelper.CreateNode(123, 73.3333333, 54.3333333);
        var node2 = TestHelper.CreateNode(456, 73.3333334, 54.3333334);
        var node3 = TestHelper.CreateNode(789, 73.3333335, 54.3333335);
        var node4 = TestHelper.CreateNode(121, 73.3333336, 54.3333336);
        var node5 = TestHelper.CreateNode(111, 73.3333337, 54.3333337);
        var node6 = TestHelper.CreateNode(988, 73.3333338, 54.3333338);
        var node7 = TestHelper.CreateNode(353, 73.3333339, 54.3333339);
    
        var way1 = TestHelper.CreateWay(1, tags,
            new [] { (long)node1.Id!, (long)node2.Id!, (long)node3.Id! });
        
        var way2 = TestHelper.CreateWay(2, tags,
            new [] { (long)node4.Id!, (long)node5.Id!, (long)node1.Id! });
        
        var way3 = TestHelper.CreateWay(3, tags,
            new [] { (long)node6.Id!, (long)node7.Id!, (long)node4.Id! });
        
        var expectedCoordinates = new List<(double latitude, double longitude)>
        {
            (73.3333338, 54.3333338),
            (73.3333339, 54.3333339),
            (73.3333336, 54.3333336),
            (73.3333337, 54.3333337),
            (73.3333333, 54.3333333),
            (73.3333334, 54.3333334),
            (73.3333335, 54.3333335)
        };
        
        //Act.
        _osmData.Nodes.Add(node1);
        _osmData.Nodes.Add(node2);
        _osmData.Nodes.Add(node3);
        _osmData.Nodes.Add(node4);
        _osmData.Nodes.Add(node5);
        _osmData.Nodes.Add(node6);
        _osmData.Nodes.Add(node7);
        
        _osmData.Ways.Add(way1);
        _osmData.Ways.Add(way2);
        _osmData.Ways.Add(way3);
        
        var streets = _streetParser.ParseAll(_osmData, null!);
        
        var streetGeometry = JsonSerializer.Deserialize<FeatureCollection>(streets.First().GeoJson);
        var multiLine = streetGeometry!.Features.First().Geometry as MultiLineString;
        var positions = multiLine!.Coordinates.First().Coordinates;
        var nodes = positions.Select(position 
            => TestHelper.CreateNode(default, position.Latitude, position.Longitude)).ToList();
        
        //Assert.
        Assert.NotNull(streetGeometry);
        Assert.NotNull(multiLine);
        Assert.NotNull(positions);
        Assert.NotNull(nodes);
        
        Assert.Equal(expectedCoordinates.Count, nodes.Count);
        
        for (var i = 0; i < expectedCoordinates.Count; i++)
        {
            Assert.Equal(expectedCoordinates[i].latitude, nodes[i].Latitude);
            Assert.Equal(expectedCoordinates[i].longitude, nodes[i].Longitude);
        }
    }
    
    [Fact]
    public void GetInseparableStreetBySeveralWays_NotSequential()
    {
        //Arrange.
        var tags = new TagsCollection
        {
            {"highway", "residential"},
            {"name", "улица Ленина"}
        };
        
        var node1 = TestHelper.CreateNode(123, 73.3333333, 54.3333333);
        var node2 = TestHelper.CreateNode(456, 73.3333334, 54.3333334);
        var node3 = TestHelper.CreateNode(789, 73.3333335, 54.3333335);
        var node4 = TestHelper.CreateNode(315, 73.3333336, 54.3333336);
        var node5 = TestHelper.CreateNode(811, 73.3333337, 54.3333337);
        var node6 = TestHelper.CreateNode(563, 73.3333338, 54.3333338);
        var node7 = TestHelper.CreateNode(353, 73.3333339, 54.3333339);
        
        var way1 = TestHelper.CreateWay(1, tags,
            new [] { (long)node1.Id!, (long)node2.Id!, (long)node3.Id! });
        
        var way2 = TestHelper.CreateWay(2, tags,
            new [] { (long)node4.Id!, (long)node5.Id!, (long)node6.Id! });
        
        var way3 = TestHelper.CreateWay(3, tags,
            new [] { (long)node6.Id!, (long)node7.Id!, (long)node1.Id! });
        
        var expectedCoordinates = new List<(double latitude, double longitude)>
        {
            (73.3333336, 54.3333336),
            (73.3333337, 54.3333337),
            (73.3333338, 54.3333338),
            (73.3333339, 54.3333339),
            (73.3333333, 54.3333333),
            (73.3333334, 54.3333334),
            (73.3333335, 54.3333335)
        };
        
        //Act.
        _osmData.Nodes.Add(node1);
        _osmData.Nodes.Add(node2);
        _osmData.Nodes.Add(node3);
        _osmData.Nodes.Add(node4);
        _osmData.Nodes.Add(node5);
        _osmData.Nodes.Add(node6);
        _osmData.Nodes.Add(node7);
        
        _osmData.Ways.Add(way1);
        _osmData.Ways.Add(way2);
        _osmData.Ways.Add(way3);
        
        var streets = _streetParser.ParseAll(_osmData, null!);
        
        var streetGeometry = JsonSerializer.Deserialize<FeatureCollection>(streets.First().GeoJson);
        var multiLine = streetGeometry!.Features.First().Geometry as MultiLineString;
        var positions = multiLine!.Coordinates.First().Coordinates;
        var nodes = positions.Select(position 
            => TestHelper.CreateNode(default, position.Latitude, position.Longitude)).ToList();
        
        //Assert.
        Assert.NotNull(streetGeometry);
        Assert.NotNull(multiLine);
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