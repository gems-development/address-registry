using Gems.AddressRegistry.Entities;
using Gems.AddressRegistry.Entities.DataSources;
using Gems.AddressRegistry.OsmDataGroupingService;
using Gems.AddressRegistry.OsmDataParser.Model;
using City = Gems.AddressRegistry.OsmDataParser.Model.City;

namespace Gems.DataMergeServices.Tests;

public class DataMergeServiceTest
{
    [Fact]
    public async void MergeDataTest_Success()
    {
        // Arrange
        var area = new Area {Id = 10L, Name = "Омская", GeoJson = "areaGeoJson"};
        var district = new District { Id = 20L, Name = "Омск", Area = area, GeoJson = "areaGeoJson" };
        var city = new City { Id = 30L, Name = "Омск", District = district, GeoJson = "areaGeoJson" };
        var street = new Street{ Id = 40L, Name = "Мира", City = city, GeoJson = "areaGeoJson"};
        
        var house1 = new House
        {
            Id = 50L,
            Number = "5",
            Street = street,
            Address = null!,
            GeoJson = "areaGeoJson"
        };

        var addressesOsm = new List<House>
        {
            house1
        };
        
        NormalizedAddressBuilder.BuildAddress(addressesOsm);

        var addressesFias = new List<Address>();

        var building1 = new Building();
        building1.Number = "5";

        var roadNetworkElement = new RoadNetworkElement();
        roadNetworkElement.Name = "Мира";

        var municipalArea = new MunicipalArea();
        municipalArea.Name = "Омск";

        var cityFias = new Gems.AddressRegistry.Entities.City();
        cityFias.Name = "Омск";
        
        var region = new Region();
        region.Name = "Омская";

        var addressFias1 = new Address();
        addressFias1.Region = region;
        addressFias1.MunicipalArea = municipalArea;
        addressFias1.City = cityFias;
        addressFias1.RoadNetworkElement = roadNetworkElement;
        addressFias1.Building = building1;
        
        addressesFias.Add(addressFias1);
        
        // Act
        DataMergeServices.Services.DataMergeService.MergeAddresses(addressesOsm,addressesFias);
        
        // Assert
        Assert.Equal(addressesFias.First().Building!.GeoJson, addressesOsm.First().GeoJson);
        Assert.Equal(addressesFias.First().RoadNetworkElement!.GeoJson, addressesOsm.First().Street!.GeoJson);
        Assert.Equal(addressesFias.First().City!.GeoJson, addressesOsm.First().Street!.City!.GeoJson);
        Assert.Equal(addressesFias.First().MunicipalArea!.GeoJson, addressesOsm.First().Street!.City!.District.GeoJson);
        Assert.Equal(addressesFias.First().Region!.GeoJson, addressesOsm.First().Street!.City!.District.Area.GeoJson);
    }
}