using Gems.AddressRegistry.Entities;
using Gems.AddressRegistry.Entities.Common;
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
        private static readonly Dictionary<string, BuildingDataSource> OsmBuildingDataSources = new Dictionary<string, BuildingDataSource>();
        private static readonly Dictionary<string, ErnDataSource> OsmErnDataSources = new Dictionary<string, ErnDataSource>();
        private static readonly Dictionary<string, CityDataSource> OsmCityDataSources = new Dictionary<string, CityDataSource>();
        private static readonly Dictionary<string, SettlementDataSource> OsmSettlementDataSources = new Dictionary<string, SettlementDataSource>();
        private static readonly Dictionary<string, MunicipalAreaDataSource> OsmMunAreaDataSources = new Dictionary<string, MunicipalAreaDataSource>();
        private static readonly Dictionary<string, RegionDataSource> OsmRegionDataSources = new Dictionary<string, RegionDataSource>();
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
            
            foreach (var normalizedAddress in NormalizedOsmAddresses.Keys)
            {
                if (NormalizedFiasAddresses.TryGetValue(normalizedAddress, out var correspondingFiasAddress))
                {
                    var correspondingOsmAddress = NormalizedOsmAddresses[normalizedAddress];
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
            var FindedDataSource = FindBuildingDataSource(buildingDataSource.Id);
            if (FindedDataSource == null)
            {
                OsmBuildingDataSources.Add(buildingDataSource.Id, buildingDataSource);
                DataMergeHelper.TryAddOsmDataSource(address.Building, buildingDataSource);
            }
            else
                DataMergeHelper.TryAddOsmDataSource(address.Building, (BuildingDataSource)FindedDataSource);

            var roadNetworkElementDataSource = new ErnDataSource();
            roadNetworkElementDataSource.Ern = address.RoadNetworkElement;
            roadNetworkElementDataSource.Id = house.Street.Id.ToString();
            roadNetworkElementDataSource.SourceType = SourceType.Osm;
            
            address.RoadNetworkElement!.GeoJson = house.Street!.GeoJson;
            FindedDataSource = FindErnDataSource(roadNetworkElementDataSource.Id);
            if (FindedDataSource == null)
            {
                OsmErnDataSources.Add(roadNetworkElementDataSource.Id, roadNetworkElementDataSource);
                DataMergeHelper.TryAddOsmDataSource(address.RoadNetworkElement, roadNetworkElementDataSource);
            }
            else
                DataMergeHelper.TryAddOsmDataSource(address.RoadNetworkElement, (ErnDataSource)FindedDataSource);
            if (house.Street.City != null)
            {
                if(address.City != null)
                {

                    var cityDataSource = new CityDataSource();
                    cityDataSource.City = address.City;
                    cityDataSource.Id = house.Street.City.Id.ToString();
                    cityDataSource.SourceType = SourceType.Osm;
                
                    address.City.GeoJson = house.Street.City.GeoJson;
                    FindedDataSource = FindCityDataSource(cityDataSource.Id);
                    if (FindedDataSource == null)
                    {
                        OsmCityDataSources.Add(cityDataSource.Id, cityDataSource);
                        DataMergeHelper.TryAddOsmDataSource(address.City, cityDataSource);
                    }
                    else
                        DataMergeHelper.TryAddOsmDataSource(address.City, (CityDataSource)FindedDataSource);
                }
                else if (address.Settlement != null)
                {
                    var settelmentDataSource = new SettlementDataSource();
                    settelmentDataSource.Settlement = address.Settlement;
                    settelmentDataSource.Id = house.Street.City.Id.ToString();
                    settelmentDataSource.SourceType = SourceType.Osm;

                    address.Settlement.GeoJson = house.Street.City.GeoJson;
                    FindedDataSource = FindSettlementDataSource(settelmentDataSource.Id);
                    if (FindedDataSource == null)
                    {
                        OsmSettlementDataSources.Add(settelmentDataSource.Id, settelmentDataSource);
                        DataMergeHelper.TryAddOsmDataSource(address.Settlement, settelmentDataSource);
                    }
                    else
                        DataMergeHelper.TryAddOsmDataSource(address.Settlement, (SettlementDataSource)FindedDataSource);
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
                FindedDataSource = FindMunAreaDataSource(municipalAreaDataSource.Id);
                if (FindedDataSource == null)
                {
                    OsmMunAreaDataSources.Add(municipalAreaDataSource.Id, municipalAreaDataSource);
                    DataMergeHelper.TryAddOsmDataSource(address.MunicipalArea, municipalAreaDataSource);
                }
                else
                    DataMergeHelper.TryAddOsmDataSource(address.MunicipalArea, (MunicipalAreaDataSource)FindedDataSource);

                var regionDataSource = new RegionDataSource();
                regionDataSource.Region = address.Region;
                regionDataSource.Id = house.Street.City.District.Area.Id.ToString();
                regionDataSource.SourceType = SourceType.Osm;
                
                address.Region.GeoJson = house.Street.City.District.Area.GeoJson;
                FindedDataSource = FindRegionDataSource(regionDataSource.Id);
                if (FindedDataSource == null)
                {
                    OsmRegionDataSources.Add(regionDataSource.Id, regionDataSource);
                    DataMergeHelper.TryAddOsmDataSource(address.Region, regionDataSource);
                }
                else
                    DataMergeHelper.TryAddOsmDataSource(address.Region, (RegionDataSource)FindedDataSource);
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
                FindedDataSource = FindSettlementDataSource(settelmentDataSource.Id);
                if (FindedDataSource == null)
                    {
                        OsmSettlementDataSources.Add(settelmentDataSource.Id, settelmentDataSource);
                        DataMergeHelper.TryAddOsmDataSource(address.Settlement, settelmentDataSource);
                    }
                else
                    DataMergeHelper.TryAddOsmDataSource(address.Settlement, (SettlementDataSource)FindedDataSource);
                } 
                else if (address.City != null) 
                {
                    var cityDataSource = new CityDataSource();
                    cityDataSource.City = address.City;
                    cityDataSource.Id = house.Street.Village.Id.ToString();
                    cityDataSource.SourceType = SourceType.Osm;

                    address.City.GeoJson = house.Street.Village.GeoJson;
                    FindedDataSource = FindCityDataSource(cityDataSource.Id);
                    if (FindedDataSource == null)
                    {
                        OsmCityDataSources.Add(cityDataSource.Id, cityDataSource);
                        DataMergeHelper.TryAddOsmDataSource(address.City, cityDataSource);
                    }
                    else
                        DataMergeHelper.TryAddOsmDataSource(address.City, (CityDataSource)FindedDataSource);

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
                FindedDataSource = FindMunAreaDataSource(municipalAreaDataSource.Id);
                if (FindedDataSource == null)
                {
                    OsmMunAreaDataSources.Add(municipalAreaDataSource.Id, municipalAreaDataSource);
                    DataMergeHelper.TryAddOsmDataSource(address.MunicipalArea, municipalAreaDataSource);
                }
                else
                    DataMergeHelper.TryAddOsmDataSource(address.MunicipalArea, (MunicipalAreaDataSource)FindedDataSource);

                var regionDataSource = new RegionDataSource();
                regionDataSource.Region = address.Region;
                regionDataSource.Id = house.Street.Village.District.Area.Id.ToString();
                regionDataSource.SourceType = SourceType.Osm;
                
                address.Region.GeoJson = house.Street.Village.District.Area.GeoJson;
                FindedDataSource = FindRegionDataSource(regionDataSource.Id);
                if (FindedDataSource == null)
                {
                    OsmRegionDataSources.Add(regionDataSource.Id, regionDataSource);
                    DataMergeHelper.TryAddOsmDataSource(address.Region, regionDataSource);
                }
                else
                    DataMergeHelper.TryAddOsmDataSource(address.Region, (RegionDataSource)FindedDataSource);
            }
        }
        private static DataSourceBase FindBuildingDataSource(string id)
        {
            return OsmBuildingDataSources.FirstOrDefault(o => o.Key == id).Value;
        }
        private static DataSourceBase FindErnDataSource(string id)
        {
            return OsmErnDataSources.FirstOrDefault(o => o.Key == id).Value;
        }
        private static DataSourceBase FindCityDataSource(string id)
        {
            return OsmCityDataSources.FirstOrDefault(o => o.Key == id).Value;
        }
        private static DataSourceBase FindSettlementDataSource(string id)
        {
            return OsmSettlementDataSources.FirstOrDefault(o => o.Key == id).Value;
        }
        private static DataSourceBase FindMunAreaDataSource(string id)
        {
            return OsmMunAreaDataSources.FirstOrDefault(o => o.Key == id).Value;
        }
        private static DataSourceBase FindRegionDataSource(string id)
        {
            return OsmRegionDataSources.FirstOrDefault(o => o.Key == id).Value;
        }

    }
}
