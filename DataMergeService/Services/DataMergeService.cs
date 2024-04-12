using Gems.AddressRegistry.Entities;
using Gems.AddressRegistry.Entities.DataSources;
using Gems.AddressRegistry.Entities.Enums;
using Gems.AddressRegistry.OsmDataParser.Model;

namespace Gems.DataMergeServices.Services
{
    public static class DataMergeService
    {
        public static void MergeAddresses(IReadOnlyCollection<House> addressesOsm, IReadOnlyCollection<Address> addressesFias)
        {
            var c = 0;
            foreach (var addressFias in addressesFias)
            {
                foreach (var addressOsm in addressesOsm)
                {
                    if (addressFias.GetNormalizedAddress() == addressOsm.GetNormalizedAddress())
                    {
                        AddGeometryToAddress(addressFias, addressOsm);
                        if(c++ > 3)
                            return;
                        break;
                    }
                }
            }
        }

        private static void AddGeometryToAddress(Address address, House house)
        {
            var buildingDataSource = new BuildingDataSource();
            buildingDataSource.Building = address.Building;
            buildingDataSource.Id = house.Id.ToString();
            buildingDataSource.SourceType = SourceType.Osm;
            
            address.Building!.GeoJson = house.GeoJson;
            address.Building.DataSources.Add(buildingDataSource);

            var roadNetworkElementDataSource = new ErnDataSource();
            roadNetworkElementDataSource.Ern = address.RoadNetworkElement;
            roadNetworkElementDataSource.Id = house.Street.Id.ToString();
            roadNetworkElementDataSource.SourceType = SourceType.Osm;
            
            address.RoadNetworkElement!.GeoJson = house.Street!.GeoJson;
            address.RoadNetworkElement.DataSources.Add(roadNetworkElementDataSource);
            if (house.Street.City != null)
            {
                var cityDataSource = new CityDataSource();
                cityDataSource.City = address.City;
                cityDataSource.Id = house.Street.City.Id.ToString();
                cityDataSource.SourceType = SourceType.Osm;
                
                address.City.GeoJson = house.Street.City.GeoJson;
                address.City.DataSources.Add(cityDataSource);
                
                var municipalAreaDataSource = new MunicipalAreaDataSource();
                municipalAreaDataSource.MunicipalArea = address.MunicipalArea;
                municipalAreaDataSource.Id = house.Street.City.District.Id.ToString();
                municipalAreaDataSource.SourceType = SourceType.Osm;
                
                address.MunicipalArea.GeoJson = house.Street.City.District.GeoJson;
                address.MunicipalArea.DataSources.Add(municipalAreaDataSource);
                
                var regionDataSource = new RegionDataSource();
                regionDataSource.Region = address.Region;
                regionDataSource.Id = house.Street.City.District.Area.Id.ToString();
                regionDataSource.SourceType = SourceType.Osm;
                
                address.Region.GeoJson = house.Street.City.District.Area.GeoJson;
                address.Region.DataSources.Add(regionDataSource);
            }

            else if (house.Street.Village != null)
            {
                var settelmentDataSource = new SettlementDataSource();
                settelmentDataSource.Settlement = address.Settlement;
                settelmentDataSource.Id = house.Street.Village.Id.ToString();
                settelmentDataSource.SourceType = SourceType.Osm;
                
                address.Settlement.GeoJson = house.Street.Village.GeoJson;
                address.Settlement.DataSources.Add(settelmentDataSource);
                
                var municipalAreaDataSource = new MunicipalAreaDataSource();
                municipalAreaDataSource.MunicipalArea = address.MunicipalArea;
                municipalAreaDataSource.Id = house.Street.City.District.Id.ToString();
                municipalAreaDataSource.SourceType = SourceType.Osm;
                
                address.MunicipalArea.GeoJson = house.Street.City.District.GeoJson;
                address.MunicipalArea.DataSources.Add(municipalAreaDataSource);
                
                var regionDataSource = new RegionDataSource();
                regionDataSource.Region = address.Region;
                regionDataSource.Id = house.Street.City.District.Area.Id.ToString();
                regionDataSource.SourceType = SourceType.Osm;
                
                address.Region.GeoJson = house.Street.City.District.Area.GeoJson;
                address.Region.DataSources.Add(regionDataSource);
            }
        }
        
    }
}
