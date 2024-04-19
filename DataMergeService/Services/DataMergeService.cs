using Gems.AddressRegistry.Entities;
using Gems.AddressRegistry.Entities.DataSources;
using Gems.AddressRegistry.Entities.Enums;
using Gems.AddressRegistry.OsmDataParser.Model;
using System.Diagnostics;

namespace Gems.DataMergeServices.Services
{
    public static class DataMergeService
    {
        private static readonly Dictionary<string, House> NormalizedOsmAddresses = new Dictionary<string, House>();
        private static readonly Dictionary<string, Address> NormalizedFiasAddresses = new Dictionary<string, Address>();
        
        public static void MergeAddresses(IReadOnlyCollection<House> addressesOsm, IReadOnlyCollection<Address> addressesFias)
        {
            foreach (var addressOsm in addressesOsm)
            {
                var normalizedAddress = addressOsm.GetNormalizedAddress();
                NormalizedOsmAddresses[normalizedAddress] = addressOsm;
            }
            
            foreach (var addressFias in addressesFias)
            {
                var normalizedAddress = addressFias.GetNormalizedAddress();
                NormalizedFiasAddresses[normalizedAddress] = addressFias;
            }
            
            foreach (var normalizedAddress in NormalizedFiasAddresses.Keys)
            {
                if (NormalizedOsmAddresses.TryGetValue(normalizedAddress, out var correspondingOsmAddress))
                {
                    var correspondingFiasAddress = NormalizedFiasAddresses[normalizedAddress];
                    AddGeometryToAddress(correspondingFiasAddress, correspondingOsmAddress);
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
            address.GeoJson = house.GeoJson;
            DataMergeHelper.TryAddOsmDataSource(address.Building, buildingDataSource);

            var roadNetworkElementDataSource = new ErnDataSource();
            roadNetworkElementDataSource.Ern = address.RoadNetworkElement;
            roadNetworkElementDataSource.Id = house.Street.Id.ToString();
            roadNetworkElementDataSource.SourceType = SourceType.Osm;
            
            address.RoadNetworkElement!.GeoJson = house.Street!.GeoJson;
            DataMergeHelper.TryAddOsmDataSource(address.RoadNetworkElement, roadNetworkElementDataSource);
            if (house.Street.City != null)
            {
                if(address.City != null)
                {

                    var cityDataSource = new CityDataSource();
                    cityDataSource.City = address.City;
                    cityDataSource.Id = house.Street.City.Id.ToString();
                    cityDataSource.SourceType = SourceType.Osm;
                
                    address.City.GeoJson = house.Street.City.GeoJson;
                    DataMergeHelper.TryAddOsmDataSource(address.City, cityDataSource);
                }
                else if (address.Settlement != null)
                {
                    var settelmentDataSource = new SettlementDataSource();
                    settelmentDataSource.Settlement = address.Settlement;
                    settelmentDataSource.Id = house.Street.City.Id.ToString();
                    settelmentDataSource.SourceType = SourceType.Osm;

                    address.Settlement.GeoJson = house.Street.City.GeoJson;
                    DataMergeHelper.TryAddOsmDataSource(address.Settlement, settelmentDataSource);
                }
                else 
                {
                    Debug.WriteLine($"объект {house.Street.City.Name} не найден в системе ФИАС");
                    return; 
                }

                var municipalAreaDataSource = new MunicipalAreaDataSource();
                municipalAreaDataSource.MunicipalArea = address.MunicipalArea;
                municipalAreaDataSource.Id = house.Street.City.District.Id.ToString();
                municipalAreaDataSource.SourceType = SourceType.Osm;
                
                address.MunicipalArea.GeoJson = house.Street.City.District.GeoJson;
                DataMergeHelper.TryAddOsmDataSource(address.MunicipalArea, municipalAreaDataSource);

                var regionDataSource = new RegionDataSource();
                regionDataSource.Region = address.Region;
                regionDataSource.Id = house.Street.City.District.Area.Id.ToString();
                regionDataSource.SourceType = SourceType.Osm;
                
                address.Region.GeoJson = house.Street.City.District.Area.GeoJson;
                DataMergeHelper.TryAddOsmDataSource(address.Region, regionDataSource);
            }

            else if (house.Street.Village != null)
            {
                if( address.Settlement != null)
                {

                var settelmentDataSource = new SettlementDataSource();
                settelmentDataSource.Settlement = address.Settlement;
                settelmentDataSource.Id = house.Street.Village.Id.ToString();
                settelmentDataSource.SourceType = SourceType.Osm;
                
                address.Settlement.GeoJson = house.Street.Village.GeoJson;
                DataMergeHelper.TryAddOsmDataSource(address.Settlement, settelmentDataSource);
                } 
                else if (address.City != null) 
                {
                    var cityDataSource = new CityDataSource();
                    cityDataSource.City = address.City;
                    cityDataSource.Id = house.Street.Village.Id.ToString();
                    cityDataSource.SourceType = SourceType.Osm;

                    address.City.GeoJson = house.Street.Village.GeoJson;
                    DataMergeHelper.TryAddOsmDataSource(address.City, cityDataSource);

                }
                else
                {
                    Debug.WriteLine($"объект {house.Street.Village.Name} не найден в системе ФИАС");
                    return;
                }

                var municipalAreaDataSource = new MunicipalAreaDataSource();
                municipalAreaDataSource.MunicipalArea = address.MunicipalArea;
                municipalAreaDataSource.Id = house.Street.Village.District.Id.ToString();
                municipalAreaDataSource.SourceType = SourceType.Osm;
                
                address.MunicipalArea.GeoJson = house.Street.Village.District.GeoJson;
                DataMergeHelper.TryAddOsmDataSource(address.MunicipalArea, municipalAreaDataSource);

                var regionDataSource = new RegionDataSource();
                regionDataSource.Region = address.Region;
                regionDataSource.Id = house.Street.Village.District.Area.Id.ToString();
                regionDataSource.SourceType = SourceType.Osm;
                
                address.Region.GeoJson = house.Street.Village.District.Area.GeoJson;
                DataMergeHelper.TryAddOsmDataSource(address.Region, regionDataSource);
            }
        }
        
    }
}
