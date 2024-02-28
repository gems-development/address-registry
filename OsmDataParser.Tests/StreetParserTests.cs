using Gems.AddressRegistry.OsmDataParser.Interfaces;
using Gems.AddressRegistry.OsmDataParser.Model;
using Gems.AddressRegistry.OsmDataParser.Support;
using OsmSharp;
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

        var way1 = TestHelper.CreateWay(1, tags,
            new []
            {
                (long)TestHelper.CreateNode(123).Id!, 
                (long)TestHelper.CreateNode(456).Id!, 
                (long)TestHelper.CreateNode(789).Id!
            }
        );
        var way2 = TestHelper.CreateWay(2, tags,
            new []
            {
                (long)TestHelper.CreateNode(121).Id!, 
                (long)TestHelper.CreateNode(111).Id!, 
                (long)TestHelper.CreateNode(123).Id!
            }
        );
        var expectedNodeIds = new [] { 121L, 111, 123, 456, 789 };

        //Act.
        _osmData.Ways.Add(way1);
        _osmData.Ways.Add(way2);
        var streets = _streetParser.ParseAll(_osmData, null);
        var nodeIds = streets.First().Components.First().Nodes;
        
        //Assert.
        Assert.Equal(expectedNodeIds, nodeIds);
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

        var way1 = TestHelper.CreateWay(1, tags,
            new []
            {
                (long)TestHelper.CreateNode(123).Id!, 
                (long)TestHelper.CreateNode(456).Id!, 
                (long)TestHelper.CreateNode(789).Id!
            }
        );
        
        var way2 = TestHelper.CreateWay(2, tags,
            new []
            {
                (long)TestHelper.CreateNode(121).Id!, 
                (long)TestHelper.CreateNode(111).Id!, 
                (long)TestHelper.CreateNode(123).Id!
            }
        );
        
        var way3 = TestHelper.CreateWay(3, tags,
            new []
            {
                (long)TestHelper.CreateNode(988).Id!, 
                (long)TestHelper.CreateNode(353).Id!, 
                (long)TestHelper.CreateNode(121).Id!
            }
        );
        var expectedNodeIds = new [] { 988L, 353, 121, 111, 123, 456, 789 };
        
        //Act.
        _osmData.Ways.Add(way1);
        _osmData.Ways.Add(way2);
        _osmData.Ways.Add(way3);
        var streets = _streetParser.ParseAll(_osmData, null);
        var nodeIds = streets.First().Components.First().Nodes;
        
        //Assert.
        Assert.Equal(expectedNodeIds, nodeIds);
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
        
        var way1 = TestHelper.CreateWay(1, tags,
            new []
            {
                (long)TestHelper.CreateNode(123).Id!, 
                (long)TestHelper.CreateNode(456).Id!, 
                (long)TestHelper.CreateNode(789).Id!
            }
        );
        
        var way2 = TestHelper.CreateWay(2, tags,
            new []
            {
                (long)TestHelper.CreateNode(315).Id!, 
                (long)TestHelper.CreateNode(811).Id!, 
                (long)TestHelper.CreateNode(563).Id!
            }
        );
        
        var way3 = TestHelper.CreateWay(3, tags,
            new []
            {
                (long)TestHelper.CreateNode(563).Id!, 
                (long)TestHelper.CreateNode(353).Id!, 
                (long)TestHelper.CreateNode(123).Id!
            }
        );
        
        var expectedNodeIds = new [] { 315L, 811, 563, 353, 123, 456, 789 };
        
        //Act.
        _osmData.Ways.Add(way1);
        _osmData.Ways.Add(way2);
        _osmData.Ways.Add(way3);
        var streets = _streetParser.ParseAll(_osmData, null);
        var nodeIds = streets.First().Components.First().Nodes;
        
        //Assert.
        Assert.Equal(expectedNodeIds, nodeIds);
    }
}