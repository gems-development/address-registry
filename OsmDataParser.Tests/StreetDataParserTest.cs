using OsmSharp;
using OsmSharp.Tags;

namespace osmDataParser.Tests;

public class StreetDataParserTest
{
    private StreetDataParser _streetDataParser = new StreetDataParser();
    private OsmData _osmData = new OsmData();
    
    [Fact]
    public void GetInseparableStreetByTwoWays()
    {
        //Arrange.
        var node1 = new Node { Id = 123 };
        var node2 = new Node { Id = 456 };
        var node3 = new Node { Id = 789 };
        
        var node4 = new Node { Id = 121 };
        var node5 = new Node { Id = 111 };
        var node6 = new Node { Id = 123 };

        long[] nodeIds1 = { (long)node1.Id, (long)node2.Id, (long)node3.Id };
        long[] nodeIds2 = { (long)node4.Id, (long)node5.Id, (long)node6.Id };

        var tagCollect = new TagsCollection();
        tagCollect.AddOrReplace("highway", "residential");
        tagCollect.AddOrReplace("name", "улица Ленина");
        
        var way1 = new Way
        {
            Id = 1,
            Nodes = nodeIds1,
            Tags = tagCollect
        };
        
        var way2 = new Way
        {
            Id = 2,
            Nodes = nodeIds2,
            Tags = tagCollect
        };
        
        var expectedNodeIds = new [] { 121L, 111, 123, 456, 789 };

        //Act.
        _osmData.Ways.Add(way1);
        _osmData.Ways.Add(way2);
        var streets = _streetDataParser.GetStreets(_osmData);
        var nodeIds = streets.First().Components.First().Nodes;
        
        //Assert.
        Assert.Equal(expectedNodeIds, nodeIds);
    }
    
    [Fact]
    public void GetInseparableStreetBySeveralWays_Sequential()
    {
        //Arrange.
        var node1 = new Node { Id = 123 };
        var node2 = new Node { Id = 456 };
        var node3 = new Node { Id = 789 };
        
        var node4 = new Node { Id = 121 };
        var node5 = new Node { Id = 111 };
        var node6 = new Node { Id = 123 };
        
        var node7 = new Node { Id = 988 };
        var node8 = new Node { Id = 353 };
        var node9 = new Node { Id = 121 };
        
        long[] nodeIds1 = { (long)node1.Id, (long)node2.Id, (long)node3.Id };
        long[] nodeIds2 = { (long)node4.Id, (long)node5.Id, (long)node6.Id };
        long[] nodeIds3 = { (long)node7.Id, (long)node8.Id, (long)node9.Id };
        
        var tagCollect = new TagsCollection();
        tagCollect.AddOrReplace("highway", "residential");
        tagCollect.AddOrReplace("name", "улица Ленина");
        
        var way1 = new Way
        {
            Id = 1,
            Nodes = nodeIds1,
            Tags = tagCollect
        };
        
        var way2 = new Way
        {
            Id = 2,
            Nodes = nodeIds2,
            Tags = tagCollect
        };
        
        var way3 = new Way
        {
            Id = 3,
            Nodes = nodeIds3,
            Tags = tagCollect
        };
        
        var expectedNodeIds = new [] { 988L, 353, 121, 111, 123, 456, 789 };
        
        //Act.
        _osmData.Ways.Add(way1);
        _osmData.Ways.Add(way2);
        _osmData.Ways.Add(way3);
        var streets = _streetDataParser.GetStreets(_osmData);
        var nodeIds = streets.First().Components.First().Nodes;
        
        //Assert.
        Assert.Equal(expectedNodeIds, nodeIds);
    }
    
    [Fact]
    public void GetInseparableStreetBySeveralWays_NotSequential()
    {
        //Arrange.
        var node1 = new Node { Id = 123 };
        var node2 = new Node { Id = 456 };
        var node3 = new Node { Id = 789 };
        
        var node4 = new Node { Id = 315 };
        var node5 = new Node { Id = 811 };
        var node6 = new Node { Id = 563 };
        
        var node7 = new Node { Id = 563 };
        var node8 = new Node { Id = 353 };
        var node9 = new Node { Id = 123 };
        
        long[] nodeIds1 = { (long)node1.Id, (long)node2.Id, (long)node3.Id };
        long[] nodeIds2 = { (long)node4.Id, (long)node5.Id, (long)node6.Id };
        long[] nodeIds3 = { (long)node7.Id, (long)node8.Id, (long)node9.Id };
        
        var tagCollect = new TagsCollection();
        tagCollect.AddOrReplace("highway", "residential");
        tagCollect.AddOrReplace("name", "улица Ленина");
        
        var way1 = new Way
        {
            Id = 1,
            Nodes = nodeIds1,
            Tags = tagCollect
        };
        
        var way2 = new Way
        {
            Id = 2,
            Nodes = nodeIds2,
            Tags = tagCollect
        };
        
        var way3 = new Way
        {
            Id = 3,
            Nodes = nodeIds3,
            Tags = tagCollect
        };
        
        var expectedNodeIds = new [] { 315L, 811, 563, 353, 123, 456, 789 };
        
        //Act.
        _osmData.Ways.Add(way1);
        _osmData.Ways.Add(way2);
        _osmData.Ways.Add(way3);
        var streets = _streetDataParser.GetStreets(_osmData);
        var nodeIds = streets.First().Components.First().Nodes;
        
        //Assert.
        Assert.Equal(expectedNodeIds, nodeIds);
    }
}