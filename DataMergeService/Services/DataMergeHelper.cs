using Microsoft.Extensions.Logging;
using Gems.AddressRegistry.Entities;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.DataMergeServices.Services
{
    public static class DataMergeHelper
    {
        public static void TryAddOsmDataSource(Building building, BuildingDataSource newBuildingDataSource, ILogger logger)
        {
            try
            {
                if(building.DataSources.Any(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Osm) )
                {
                    var oldDataSource = building.DataSources.First(o 
                        => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Osm);
                    building.DataSources.Remove(oldDataSource);
                }
                building.DataSources.Add(newBuildingDataSource);
            }catch (Exception ex)
            {
                logger.LogError($"Не удалось добавить источник данных, вызвано исключение: {ex}"); 
            }
        }
        public static void TryAddOsmDataSource(RoadNetworkElement roadNetworkElement, ErnDataSource newErnDataSource)
        {       
                var oldDataSource = roadNetworkElement.DataSources.FirstOrDefault(o 
                        => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Osm);

                if (oldDataSource == null)
                {
                    roadNetworkElement.DataSources.Add(newErnDataSource);  
                }
        }
        public static void TryAddOsmDataSource(Settlement settlement, SettlementDataSource newSettelmentDataSource, ILogger logger)
        {
            try
            {
                if (settlement.DataSources.Any(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Osm))
                {
                    var oldDataSource = settlement.DataSources.First(o
                        => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Osm);
                    settlement.DataSources.Remove(oldDataSource);
                }
                settlement.DataSources.Add(newSettelmentDataSource);
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось добавить источник данных, вызвано исключение: {ex}");
            }
        }
        public static void TryAddOsmDataSource(City city, CityDataSource newCityDataSource, ILogger logger)
        {
            try
            {
                if (city.DataSources.Any(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Osm))
                {
                    var oldDataSource = city.DataSources.First(o
                        => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Osm);
                    city.DataSources.Remove(oldDataSource);
                }
                city.DataSources.Add(newCityDataSource);
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось добавить источник данных, вызвано исключение: {ex}");
            }
        }
        public static void TryAddOsmDataSource(MunicipalArea municipalArea, MunicipalAreaDataSource newMunicipalAreaDataSource, ILogger logger)
        {
            try
            {
                if (municipalArea.DataSources.Any(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Osm))
                {
                    var oldDataSource = municipalArea.DataSources.First(o
                        => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Osm);
                    municipalArea.DataSources.Remove(oldDataSource);
                }
                municipalArea.DataSources.Add(newMunicipalAreaDataSource);
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось добавить источник данных, вызвано исключение: {ex}");
            }
        }
        public static void TryAddOsmDataSource(Region region, RegionDataSource newRegionDataSource, ILogger logger)
        {
            try
            {
                if (region.DataSources.Any(o => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Osm))
                {
                    var oldDataSource = region.DataSources.First(o
                        => o.SourceType == AddressRegistry.Entities.Enums.SourceType.Osm);
                    region.DataSources.Remove(oldDataSource);
                }
                region.DataSources.Add(newRegionDataSource);
            }
            catch (Exception ex)
            {
                logger.LogError($"Не удалось добавить источник данных, вызвано исключение: {ex}");
            }
        }
    }
}
