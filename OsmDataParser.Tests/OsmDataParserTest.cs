using OsmSharp;
using OsmSharp.Tags;

namespace osmDataParser.Tests;

public class OsmDataParserTest
{
    private readonly OsmDataParser _osmDataParser = new OsmDataParser();
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
        var streets = _osmDataParser.GetStreets(_osmData);
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
        var streets = _osmDataParser.GetStreets(_osmData);
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
        var streets = _osmDataParser.GetStreets(_osmData);
        var nodeIds = streets.First().Components.First().Nodes;
        
        //Assert.
        Assert.Equal(expectedNodeIds, nodeIds);
    }

    [Fact]
    public void GetLocalities_Success()
    {
        //Arrange.
        var tags = new TagsCollection
        {
            {"boundary", "administrative"},
            {"admin_level", "6"},
            {"name", "Омский район"}
        };
        
        var way1 = TestHelper.CreateWay(1, null!,
            new []
            {
                (long)TestHelper.CreateNode(123).Id!, 
                (long)TestHelper.CreateNode(456).Id!, 
                (long)TestHelper.CreateNode(789).Id!
            }
        );
        
        var way2 = TestHelper.CreateWay(2, null!,
            new []
            {
                (long)TestHelper.CreateNode(315).Id!, 
                (long)TestHelper.CreateNode(811).Id!, 
                (long)TestHelper.CreateNode(563).Id!
            }
        );
        
        var way3 = TestHelper.CreateWay(3, null!,
            new []
            {
                (long)TestHelper.CreateNode(563).Id!, 
                (long)TestHelper.CreateNode(353).Id!, 
                (long)TestHelper.CreateNode(123).Id!
            }
        );
        
        var way4 = TestHelper.CreateWay(4, null!,
            new []
            {
                (long)TestHelper.CreateNode(315).Id!, 
                (long)TestHelper.CreateNode(888).Id!, 
                (long)TestHelper.CreateNode(789).Id!
            }
        );
        
        var adminCenter = TestHelper.CreateNode(5);
        
        var relation = TestHelper.CreateRelation(999, tags, 
            new []
            {
                new RelationMember { Id = (long)way1.Id! },
                new RelationMember { Id = (long)way2.Id! },
                new RelationMember { Id = (long)way3.Id! },
                new RelationMember { Id = (long)way4.Id! },
                new RelationMember { Id = (long)adminCenter.Id! }
            }
        );
        
        var expectedNodeIds = new [] { 789L, 888, 315, 811, 563, 353, 123, 456, 789 };
        
        //Act.
        _osmData.Ways.Add(way1);
        _osmData.Ways.Add(way2);
        _osmData.Ways.Add(way3);
        _osmData.Ways.Add(way4);
        _osmData.Relations.Add(relation);
        var localities = _osmDataParser.GetLocalities(_osmData);
        var nodeIds = localities.First().Components.First().Nodes;
        
        //Assert.
        Assert.Equal(expectedNodeIds, nodeIds);
    }
}