﻿using Gems.AddressRegistry.Entities;
using Gems.AddressRegistry.OsmDataParser.Model;
using Microsoft.Extensions.Logging;
using Moq;
using City = Gems.AddressRegistry.OsmDataParser.Model.City;

namespace Gems.DataMergeServices.Tests;

public class DataMergeServiceTest
{
    private readonly ILogger _logger = new Mock<ILogger>().Object;

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
        DataMergeServices.Services.DataMergeService.MergeAddresses(addressesOsm,addressesFias, _logger);
        
        // Assert
        Assert.Equal(addressesFias.First().Building!.GeoJson, addressesOsm.First().GeoJson);
        Assert.Equal(addressesFias.First().RoadNetworkElement!.GeoJson, addressesOsm.First().Street!.GeoJson);
        Assert.Equal(addressesFias.First().City!.GeoJson, addressesOsm.First().Street!.City!.GeoJson);
        Assert.Equal(addressesFias.First().MunicipalArea!.GeoJson, addressesOsm.First().Street!.City!.District.GeoJson);
        Assert.Equal(addressesFias.First().Region!.GeoJson, addressesOsm.First().Street!.City!.District.Area.GeoJson);
    }

    [Fact]
    public async void MergeDataTest_Fail()
    {
        // Arrange
        var area = new Area { Id = 10L, Name = "Омская", GeoJson = "areaGeoJson" };
        var district = new District { Id = 20L, Name = "Омск", Area = area, GeoJson = "areaGeoJson" };
        var city = new City { Id = 30L, Name = "Омск", District = district, GeoJson = "areaGeoJson" };
        var street = new Street { Id = 40L, Name = "Мира", City = city, GeoJson = "areaGeoJson" };

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

        var addressesFias = new List<Address>();

        var building1 = new Building();
        building1.Number = "5";

        var roadNetworkElement = new RoadNetworkElement();
        roadNetworkElement.Name = "Мира";

        var municipalArea = new MunicipalArea();
        municipalArea.Name = "Омская";

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
        DataMergeServices.Services.DataMergeService.MergeAddresses(addressesOsm, addressesFias, _logger);

        // Assert
        Assert.NotEqual(addressesFias.First().Building!.GeoJson, addressesOsm.First().GeoJson);
        Assert.NotEqual(addressesFias.First().RoadNetworkElement!.GeoJson, addressesOsm.First().Street!.GeoJson);
        Assert.NotEqual(addressesFias.First().City!.GeoJson, addressesOsm.First().Street!.City!.GeoJson);
        Assert.NotEqual(addressesFias.First().MunicipalArea!.GeoJson, addressesOsm.First().Street!.City!.District.GeoJson);
        Assert.NotEqual(addressesFias.First().Region!.GeoJson, addressesOsm.First().Street!.City!.District.Area.GeoJson);
    }
}