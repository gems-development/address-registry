using System.Xml;
using Microsoft.Extensions.Logging;
using Gems.AddressRegistry.Entities;
using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;

namespace Gems.DataMergeServices.Services
{
    public class FiasXmlToEntityConverter
    {
        Country country = new Country();
        
        Region region = new Region();
        Dictionary<int, AdministrativeArea> administrativeAreaDictionary = new Dictionary<int, AdministrativeArea>();
        Dictionary<int, MunicipalArea> municipalAreaDictionary = new Dictionary<int, MunicipalArea>();
        Dictionary<int, Territory> territoryDictionary = new Dictionary<int, Territory>();
        Dictionary<int, City> cityDictionary = new Dictionary<int, City>();
        Dictionary<int, Settlement> settlementDictionary = new Dictionary<int, Settlement>();
        Dictionary<int, PlaningStructureElement> planingStructureElementDictionary = new Dictionary<int, PlaningStructureElement>();
        Dictionary<int, RoadNetworkElement> roadNetworkElementDictionary = new Dictionary<int, RoadNetworkElement>();
        Dictionary<int, Building> buildingDictionary = new Dictionary<int, Building>();

        Dictionary<int, int> AdmHierarchy = new Dictionary<int, int>();
        Dictionary<int, int> MunHierarchy = new Dictionary<int, int>();

        Dictionary<int, Func<int, (BaseIdentifiableEntity BaseEntity, int LevelNumber)?>[]> levelToParentMap =
            new Dictionary<int, Func<int, (BaseIdentifiableEntity BaseEntity, int LevelNumber)?>[]>();

        List<Address> addresses = new List<Address>();

        List<string> targetNameParts = new List<string>() { "ЗАТО", "ПОСЕЛОК"};

    public FiasXmlToEntityConverter()
        {
            country.Name = "Russia";

            levelToParentMap.Add(
                9,
                new Func<int, (BaseIdentifiableEntity BaseEntity, int LevelNumber)?>[]
                {
                    key => planingStructureElementDictionary.TryGetValue(key, out var result) ? (result, 7) : null,
                    key => roadNetworkElementDictionary.TryGetValue(key, out var result) ? (result, 8) : null,
                });
            levelToParentMap.Add(
               8,
               new Func<int, (BaseIdentifiableEntity BaseEntity, int LevelNumber)?>[]
               {
                    key => planingStructureElementDictionary.TryGetValue(key, out var result) ? (result, 7) : null,
                    key => settlementDictionary.TryGetValue(key, out var result) ? (result, 6) : null,
                    key => cityDictionary.TryGetValue(key, out var result) ? (result, 5) : null,
               });
            levelToParentMap.Add(
               7,
               new Func<int, (BaseIdentifiableEntity BaseEntity, int LevelNumber)?>[]
               {
                    key => settlementDictionary.TryGetValue(key, out var result) ? (result, 6) : null,
                    key => cityDictionary.TryGetValue(key, out var result) ? (result, 5) : null,
               });
            levelToParentMap.Add(
               6,
               new Func<int, (BaseIdentifiableEntity BaseEntity, int LevelNumber)?>[]
               {
                    key => municipalAreaDictionary.TryGetValue(key, out var result) ? (result, 3) : null,
                    key => territoryDictionary.TryGetValue(key, out var result) ? (result, 4) : null,
                    key => cityDictionary.TryGetValue(key, out var result) ? (result, 5) : null,
               });
            levelToParentMap.Add(
               5,
               new Func<int, (BaseIdentifiableEntity BaseEntity, int LevelNumber)?>[]
               {
                    key => territoryDictionary.TryGetValue(key, out var result) ? (result, 4) : null,
                    key => municipalAreaDictionary.TryGetValue(key, out var result) ? (result, 3) : null,
                    key => administrativeAreaDictionary.TryGetValue(key, out var result) ? (result, 2) : null,
               });
            levelToParentMap.Add(
               4,
               new Func<int, (BaseIdentifiableEntity BaseEntity, int LevelNumber)?>[]
               {
                    key => municipalAreaDictionary.TryGetValue(key, out var result) ? (result, 3) : null,
               });
            // 3 и 2 уровень всегда ссылаются на единственный элемент первого уровня
        }

        public async Task ConvertRegion(String uri, ILogger logger)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Async = true;

            using (XmlReader reader = XmlReader.Create(uri, settings))
            {
                reader.MoveToContent();
                while (await reader.ReadAsync())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:

                            if (reader.GetAttribute("ISACTUAL") != "1" || reader.GetAttribute("ISACTIVE") != "1")
                                break;
                            switch (reader.GetAttribute("LEVEL"))
                            {
                                case ("1"):
                                    region.Name = reader.GetAttribute("NAME")!;
                                    RegionDataSource regionDataSource = new RegionDataSource();
                                    regionDataSource.Region = region;
                                    regionDataSource.AuxiliaryId = reader.GetAttribute("OBJECTID")!;
                                    regionDataSource.Id = reader.GetAttribute("OBJECTGUID")!;
                                    regionDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                    region.DataSources.Add(regionDataSource);
                                    logger.LogTrace($"ФИАС || Считана область: {region.Name}");
                                    break;
                                case ("2"):
                                    AdministrativeArea administrativeArea = new AdministrativeArea();
                                    administrativeArea.Name = reader.GetAttribute("NAME")!;
                                    AdministrativeAreaDataSource administrativeAreaDataSource = new AdministrativeAreaDataSource();
                                    administrativeAreaDataSource.AdministrativeArea = administrativeArea;
                                    administrativeAreaDataSource.AuxiliaryId = reader.GetAttribute("OBJECTID")!;
                                    administrativeAreaDataSource.Id = reader.GetAttribute("OBJECTGUID")!;
                                    administrativeAreaDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                    administrativeArea.DataSources.Add(administrativeAreaDataSource);
                                    administrativeAreaDictionary.Add(int.Parse(administrativeAreaDataSource.AuxiliaryId), administrativeArea);
                                    logger.LogTrace($"ФИАС || Считана административная область: {administrativeArea.Name}");
                                    break;
                                case ("3"):
                                    MunicipalArea municipalArea = new MunicipalArea();
                                    municipalArea.Name = CheckAndCleanName(reader.GetAttribute("NAME")!);
                                    MunicipalAreaDataSource municipalAreaDataSource = new MunicipalAreaDataSource();
                                    municipalAreaDataSource.MunicipalArea = municipalArea;
                                    municipalAreaDataSource.AuxiliaryId = reader.GetAttribute("OBJECTID")!;
                                    municipalAreaDataSource.Id = reader.GetAttribute("OBJECTGUID")!;
                                    municipalAreaDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                    municipalArea.DataSources.Add(municipalAreaDataSource);
                                    municipalAreaDictionary.Add(int.Parse(municipalAreaDataSource.AuxiliaryId), municipalArea);
                                    logger.LogTrace($"ФИАС || Считана муниципальная бласть: {municipalArea.Name}");
                                    break;
                                case ("4"):
                                    Territory territory = new Territory();
                                    territory.Name = reader.GetAttribute("NAME")!;
                                    TerritoryDataSource terrytoryDataSource = new TerritoryDataSource();
                                    terrytoryDataSource.Territory = territory;
                                    terrytoryDataSource.AuxiliaryId = reader.GetAttribute("OBJECTID")!;
                                    terrytoryDataSource.Id = reader.GetAttribute("OBJECTGUID")!;
                                    terrytoryDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                    territory.DataSources.Add(terrytoryDataSource);
                                    territoryDictionary.Add(int.Parse(terrytoryDataSource.AuxiliaryId), territory);
                                    logger.LogTrace($"ФИАС || Считана территория: {territory.Name}");
                                    break;
                                case ("5"):
                                    City city = new City();
                                    city.Name = reader.GetAttribute("NAME")!;
                                    CityDataSource cityDataSource = new CityDataSource();
                                    cityDataSource.City = city;
                                    cityDataSource.AuxiliaryId = reader.GetAttribute("OBJECTID")!;
                                    cityDataSource.Id = reader.GetAttribute("OBJECTGUID")!;
                                    cityDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                    city.DataSources.Add(cityDataSource);
                                    cityDictionary.Add(int.Parse(cityDataSource.AuxiliaryId), city);
                                    logger.LogTrace($"ФИАС || Считан город: {city.Name}");
                                    break;
                                case ("6"):
                                    Settlement settlement = new Settlement();
                                    settlement.Name = reader.GetAttribute("NAME")!;
                                    SettlementDataSource settlementDataSource = new SettlementDataSource();
                                    settlementDataSource.Settlement = settlement;
                                    settlementDataSource.AuxiliaryId = reader.GetAttribute("OBJECTID")!;
                                    settlementDataSource.Id = reader.GetAttribute("OBJECTGUID")!;
                                    settlementDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                    settlement.DataSources.Add(settlementDataSource);
                                    settlementDictionary.Add(int.Parse(settlementDataSource.AuxiliaryId), settlement);
                                    logger.LogTrace($"ФИАС || Считано поселение: {settlement.Name}");
                                    break;
                                case ("7"):
                                    PlaningStructureElement planingStructure = new PlaningStructureElement();
                                    planingStructure.Name = reader.GetAttribute("NAME")!;
                                    EpsDataSource epsDataSource = new EpsDataSource();
                                    epsDataSource.Eps = planingStructure;
                                    epsDataSource.AuxiliaryId = reader.GetAttribute("OBJECTID")!;
                                    epsDataSource.Id = reader.GetAttribute("OBJECTGUID")!;
                                    epsDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                    planingStructure.DataSources.Add(epsDataSource);
                                    planingStructureElementDictionary.Add(int.Parse(epsDataSource.AuxiliaryId), planingStructure);
                                    logger.LogTrace($"ФИАС || Считан элемент планировочной структуры {planingStructure.Name}");
                                    break;
                                case ("8"):
                                    RoadNetworkElement roadNetworkElement = new RoadNetworkElement();
                                    roadNetworkElement.Name = reader.GetAttribute("NAME")!;
                                    ErnDataSource ernDataSource = new ErnDataSource();
                                    ernDataSource.Ern = roadNetworkElement;
                                    ernDataSource.AuxiliaryId = reader.GetAttribute("OBJECTID")!;
                                    ernDataSource.Id = reader.GetAttribute("OBJECTGUID")!;
                                    ernDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                    roadNetworkElement.DataSources.Add(ernDataSource);
                                    roadNetworkElementDictionary.Add(int.Parse(ernDataSource.AuxiliaryId), roadNetworkElement);
                                    logger.LogTrace($"ФИАС || Считан элемент улично-дорожной сети {roadNetworkElement.Name}");
                                    break;
                            }
                            break;
                        case XmlNodeType.Text:
                            logger.LogDebug("Text Node: {0}",
                                     await reader.GetValueAsync());
                            break;
                        case XmlNodeType.EndElement:
                            logger.LogDebug("End Element {0}", reader.Name);
                            break;
                        default:
                            logger.LogDebug("Other node {0} with value {1}",
                                            reader.NodeType, reader.Value);
                            break;
                    }
                }
            }
            logger.LogDebug("ФИАС || Завершено считывание файла региона");
        }

        public async Task ConvertBuildings(String uri, ILogger logger)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Async = true;

            using (XmlReader reader = XmlReader.Create(uri, settings))
            {
                reader.MoveToContent();
                while (await reader.ReadAsync())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.GetAttribute("ISACTUAL") != "1" || reader.GetAttribute("ISACTIVE") != "1")
                                break;
                            Building building = new Building();
                            building.Number = reader.GetAttribute("HOUSENUM")!;
                            BuildingDataSource buildingDataSource = new BuildingDataSource();
                            buildingDataSource.Building = building;
                            buildingDataSource.AuxiliaryId = reader.GetAttribute("OBJECTID")!;
                            buildingDataSource.Id = reader.GetAttribute("OBJECTGUID")!;
                            buildingDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                            building.DataSources.Add(buildingDataSource);
                            if (!buildingDictionary.TryAdd(int.Parse(buildingDataSource.AuxiliaryId), building))
                                logger.LogTrace($"ФИАС ||Не удалось добавить здание с id: {int.Parse(buildingDataSource.AuxiliaryId)}" );
                            logger.LogTrace($"ФИАС || Добавлен дом № {building.Number}");
                            break;
                        case XmlNodeType.Text:
                            logger.LogDebug("Text Node: {0}",
                                     await reader.GetValueAsync());
                            break;
                        case XmlNodeType.EndElement:
                            logger.LogDebug("End Element {0}", reader.Name);
                            break;
                        default:
                            logger.LogDebug("Other node {0} with value {1}",
                                            reader.NodeType, reader.Value);
                            break;
                    }
                }
            }
            logger.LogDebug("ФИАС || Завершено считывание файла с зданиями");
        }

        public async Task ReadAdmHierarchy(String uri, ILogger logger)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Async = true;

            using (XmlReader reader = XmlReader.Create(uri, settings))
            {
                reader.MoveToContent();
                while (await reader.ReadAsync())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.GetAttribute("ISACTIVE") != "1")
                                break;
                            var objId = reader.GetAttribute("OBJECTID");
                            var parentObjId = reader.GetAttribute("PARENTOBJID");
                            if(objId!= null && parentObjId != null)
                            AdmHierarchy.Add(int.Parse(objId), int.Parse(parentObjId));
                            if(region.Code == null)
                            region.Code = reader.GetAttribute("REGIONCODE")!;//TODO как вытащить код региона не выполняя этот код каждую итерацию
                            break;
                        case XmlNodeType.Text:
                            logger.LogDebug("Text Node: {0}",
                                     await reader.GetValueAsync());
                            break;
                        case XmlNodeType.EndElement:
                            logger.LogDebug("End Element {0}", reader.Name);
                            break;
                        default:
                            logger.LogDebug("Other node {0} with value {1}",
                                            reader.NodeType, reader.Value);
                            break;
                    }
                }
            }
            logger.LogDebug("ФИАС || Завершено считывание файла административной иерархии");
        }
        public async Task ReadMunHierarchy(String uri, ILogger logger)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Async = true;

            using (XmlReader reader = XmlReader.Create(uri, settings))
            {
                reader.MoveToContent();
                while (await reader.ReadAsync())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.GetAttribute("ISACTIVE") != "1")
                                break;
                            var objId = reader.GetAttribute("OBJECTID");
                            var parentObjId = reader.GetAttribute("PARENTOBJID");
                            if (objId != null && parentObjId != null)
                                MunHierarchy.Add(int.Parse(objId), int.Parse(parentObjId));
                           break;
                        case XmlNodeType.Text:
                            logger.LogDebug("Text Node: {0}",
                                     await reader.GetValueAsync());
                            break;
                        case XmlNodeType.EndElement:
                            logger.LogDebug("End Element {0}", reader.Name);
                            break;
                        default:
                            logger.LogDebug("Other node {0} with value {1}",
                                            reader.NodeType, reader.Value);
                            break;
                    }
                }
            }
            logger.LogDebug("ФИАС || Завершено считывание файла муниципальной иерархии");
        }


        public async Task<IReadOnlyCollection<Address>> ReadAndBuildAddresses(String pathAdm, String pathMun, String pathBuildings, String pathReg, ILogger logger)
        {
            await ReadAdmHierarchy(pathAdm, logger);
            await ReadMunHierarchy(pathMun, logger);
            await ConvertRegion(pathReg, logger);
            await ConvertBuildings(pathBuildings, logger);
            var addresses = new List<Address>();
            foreach(var item in buildingDictionary)
            {
                var address = new Address();
                address.Building = item.Value;
                
                address.Region = region;
                FindParents(9, address);
                logger.LogTrace($"ФИАС || Собрана иерархия адреса: {address.GetNormalizedAddress(logger)}");
                addresses.Add(address);
            }
            return addresses;
        }

        public void FindParents(int level, Address address)
        {
            int objId;
            int parentObjId;
            int parentMunicipalityObjId;
            Func<int, (BaseIdentifiableEntity BaseEntity, int LevelNumber)?>[] levelEntryParentMappings;

            switch (level)
            {
                case (9):
                    objId = int.Parse(address.Building!.DataSources.First().AuxiliaryId);
                    AdmHierarchy.TryGetValue(objId, out parentObjId);
                    if (parentObjId != 0)
                    {
                        levelEntryParentMappings = levelToParentMap[9];
                        foreach (var mapper in levelEntryParentMappings)
                        {
                            var entry = mapper(parentObjId);

                            if (entry is not null)
                            {
                                var levelNumber = entry.Value.LevelNumber;
                                if (levelNumber == 8)
                                {
                                    address.RoadNetworkElement = (RoadNetworkElement)entry.Value.BaseEntity;
                                    address.Building.RoadNetworkElement = address.RoadNetworkElement;
                                }
                                else if (levelNumber == 7)
                                {
                                    address.PlaningStructureElement = (PlaningStructureElement)entry.Value.BaseEntity;
                                    address.Building.PlaningStructureElement = address.PlaningStructureElement;
                                }
                                    
                                FindParents(levelNumber, address);
                            }
                        }
                    }
                    break;
                case (8):
                    objId = int.Parse(address.RoadNetworkElement!.DataSources.First().AuxiliaryId);
                    AdmHierarchy.TryGetValue(objId, out parentObjId);
                    levelEntryParentMappings = levelToParentMap[8];
                    foreach (var mapper in levelEntryParentMappings)
                    {
                        var entry = mapper(parentObjId);

                        if (entry is not null)
                        {
                            var levelNumber = entry.Value.LevelNumber;
                            if (levelNumber == 7)
                            {
                                address.PlaningStructureElement = (PlaningStructureElement)entry.Value.BaseEntity;
                                address.RoadNetworkElement.PlaningStructureElement = address.PlaningStructureElement;
                            }
                            else if (levelNumber == 6)
                            {
                                address.Settlement = (Settlement)entry.Value.BaseEntity;
                                address.RoadNetworkElement.Settlement = address.Settlement;
                            }
                            else if (levelNumber == 5)
                            {
                                address.City = (City)entry.Value.BaseEntity;
                                address.RoadNetworkElement.City = address.City;
                            }
                            FindParents(levelNumber, address);
                        }
                    }
                    break;
                case (7):
                    objId = int.Parse(address.PlaningStructureElement!.DataSources.First().AuxiliaryId);
                    AdmHierarchy.TryGetValue(objId, out parentObjId);
                    levelEntryParentMappings = levelToParentMap[7];
                    foreach (var mapper in levelEntryParentMappings)
                    {
                        var entry = mapper(parentObjId);

                        if (entry is not null)
                        {
                            var levelNumber = entry.Value.LevelNumber;
                            if (levelNumber == 6)
                            {
                                address.Settlement = (Settlement)entry.Value.BaseEntity;
                                address.PlaningStructureElement.Settlement = address.Settlement;
                            }
                            else if (levelNumber == 5)
                            {
                                address.City = (City)entry.Value.BaseEntity;
                                address.PlaningStructureElement.City = address.City;
                            }
                            FindParents(levelNumber, address);
                        }
                    }
                    break;
                case (6):
                    objId = int.Parse(address.Settlement!.DataSources.First().AuxiliaryId);
                    AdmHierarchy.TryGetValue(objId, out parentObjId);
                    MunHierarchy.TryGetValue(objId, out parentMunicipalityObjId);
                    levelEntryParentMappings = levelToParentMap[6];
                    foreach (var mapper in levelEntryParentMappings)
                    {
                        var entry = mapper(parentObjId);
                        var entryMunicipal = mapper(parentMunicipalityObjId);
                        if (entry is not null)
                        {
                            var levelNumber = entry.Value.LevelNumber;

                            if (levelNumber == 5)
                            {
                                address.City = (City)entry.Value.BaseEntity;
                                address.Settlement.City = address.City;
                            }
                                
                            FindParents(levelNumber, address);
                        }
                        if (entryMunicipal is not null)
                        {
                            var levelNumber = entryMunicipal.Value.LevelNumber;
                            if (levelNumber == 4)
                            {
                                address.Territory = (Territory)entryMunicipal.Value.BaseEntity;
                                address.Settlement.Territory = address.Territory;
                                FindParents(levelNumber, address);
                            }
                            else if (levelNumber == 3)
                            {
                                address.MunicipalArea = (MunicipalArea)entryMunicipal.Value.BaseEntity;
                                address.Settlement.MunicipalArea = address.MunicipalArea;
                            }
                        }
                    }
                    break;
                case (5):
                    objId = int.Parse(address.City!.DataSources.First().AuxiliaryId);
                    AdmHierarchy.TryGetValue(objId, out parentObjId);
                    MunHierarchy.TryGetValue(objId, out parentMunicipalityObjId);
                    levelEntryParentMappings = levelToParentMap[5];
                    foreach (var mapper in levelEntryParentMappings)
                    {
                        var entry = mapper(parentObjId);
                        var entryMunicipal = mapper(parentMunicipalityObjId);

                        if (entry is not null)
                        {
                            var levelNumber = entry.Value.LevelNumber;
                            if (levelNumber == 2)
                            {
                                address.AdministrativeArea = (AdministrativeArea)entry.Value.BaseEntity;
                                address.City.AdministrativeArea = address.AdministrativeArea;
                            }
                        }

                        if (entryMunicipal is not null)
                        {
                            var levelNumber = entryMunicipal.Value.LevelNumber;
                            if (levelNumber == 4)
                            {
                                address.Territory = (Territory)entryMunicipal.Value.BaseEntity;
                                address.City.Territory = address.Territory;
                                FindParents(levelNumber, address);
                            }
                            else if (levelNumber == 3)
                            {
                                address.MunicipalArea = (MunicipalArea)entryMunicipal.Value.BaseEntity;
                                address.City.MunicipalArea = address.MunicipalArea;
                            }
                        }
                    }
                    break;
                case (4):
                    objId = int.Parse(address.Territory!.DataSources.First().AuxiliaryId);
                    MunHierarchy.TryGetValue(objId, out parentMunicipalityObjId);
                    levelEntryParentMappings = levelToParentMap[4];
                    foreach (var mapper in levelEntryParentMappings)
                    {
                        var entryMunicipal = mapper(parentMunicipalityObjId);

                        if (entryMunicipal is not null)
                        {
                            var levelNumber = entryMunicipal.Value.LevelNumber;
                            if (levelNumber == 3)
                            {
                                address.MunicipalArea = (MunicipalArea)entryMunicipal.Value.BaseEntity;
                                address.Territory.MunicipalArea = address.MunicipalArea;
                            }
                        }
                    }
                    break;
            }

            if (address.AdministrativeArea is not null)
                address.AdministrativeArea.Region = address.Region;

            if (address.MunicipalArea is not null)
                address.MunicipalArea.Region = address.Region;
        }

        public string CheckAndCleanName(string name)
        {
            string newName = "";
            var chanks = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach(var partName in chanks)
            {
                if (!targetNameParts.Contains(partName.ToUpper()))
                {
                    if (newName != "")
                        newName = newName + " " + partName;
                    else newName += partName;
                } 
            }
            return newName;
        }
    }
}
