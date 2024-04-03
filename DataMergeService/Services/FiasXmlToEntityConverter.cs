﻿using Gems.AddressRegistry.Entities;
using Gems.AddressRegistry.Entities.Common;
using Gems.AddressRegistry.Entities.DataSources;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Xml;

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

        async public void ConvertRegion(String uri)
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
                                    regionDataSource.Id = reader.GetAttribute("OBJECTID")!;
                                    regionDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                    region.DataSources.Add(regionDataSource);
                                    Debug.WriteLine(region.Name);
                                    break;

                                case ("2"):
                                    AdministrativeArea administrativeArea = new AdministrativeArea();
                                    administrativeArea.Name = reader.GetAttribute("NAME")!;
                                    AdministrativeAreaDataSource administrativeAreaDataSource = new AdministrativeAreaDataSource();
                                    administrativeAreaDataSource.AdministrativeArea = administrativeArea;
                                    administrativeAreaDataSource.Id = reader.GetAttribute("OBJECTID")!;
                                    administrativeAreaDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                    administrativeArea.DataSources.Add(administrativeAreaDataSource);
                                    administrativeAreaDictionary.Add(int.Parse(administrativeAreaDataSource.Id), administrativeArea);
                                    Debug.WriteLine(administrativeArea.Name);
                                    break;

                                case ("3"):
                                    MunicipalArea municipalArea = new MunicipalArea();
                                    municipalArea.Name = reader.GetAttribute("NAME")!;
                                    MunicipalAreaDataSource municipalAreaDataSource = new MunicipalAreaDataSource();
                                    municipalAreaDataSource.MunicipalArea = municipalArea;
                                    municipalAreaDataSource.Id = reader.GetAttribute("OBJECTID")!;
                                    municipalAreaDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                    municipalArea.DataSources.Add(municipalAreaDataSource);
                                    municipalAreaDictionary.Add(int.Parse(municipalAreaDataSource.Id), municipalArea);
                                    Debug.WriteLine(municipalArea.Name);
                                    break;
                                case ("4"):
                                    Territory territory = new Territory();
                                    territory.Name = reader.GetAttribute("NAME")!;
                                    TerritoryDataSource terrytoryDataSource = new TerritoryDataSource();
                                    terrytoryDataSource.Territory = territory;
                                    terrytoryDataSource.Id = reader.GetAttribute("OBJECTID")!;
                                    terrytoryDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                    territory.DataSources.Add(terrytoryDataSource);
                                    territoryDictionary.Add(int.Parse(terrytoryDataSource.Id), territory);
                                    Debug.WriteLine(territory.Name);
                                    break;
                                case ("5"):
                                    City city = new City();
                                    city.Name = reader.GetAttribute("NAME")!;
                                    CityDataSource cityDataSource = new CityDataSource();
                                    cityDataSource.City = city;
                                    cityDataSource.Id = reader.GetAttribute("OBJECTID")!;
                                    cityDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                    city.DataSources.Add(cityDataSource);
                                    cityDictionary.Add(int.Parse(cityDataSource.Id), city);
                                    Debug.WriteLine(city.Name);
                                    break;
                                case ("6"):
                                    Settlement settlement = new Settlement();
                                    settlement.Name = reader.GetAttribute("NAME")!;
                                    SettlementDataSource settlementDataSource = new SettlementDataSource();
                                    settlementDataSource.Settlement = settlement;
                                    settlementDataSource.Id = reader.GetAttribute("OBJECTID")!;
                                    settlementDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                    settlement.DataSources.Add(settlementDataSource);
                                    settlementDictionary.Add(int.Parse(settlementDataSource.Id), settlement);
                                    Debug.WriteLine(settlement.Name);
                                    break;
                                case ("7"):
                                    PlaningStructureElement planingStructure = new PlaningStructureElement();
                                    planingStructure.Name = reader.GetAttribute("NAME")!;
                                    EpsDataSource epsDataSource = new EpsDataSource();
                                    epsDataSource.Eps = planingStructure;
                                    epsDataSource.Id = reader.GetAttribute("OBJECTID")!;
                                    epsDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                    planingStructure.DataSources.Add(epsDataSource);
                                    planingStructureElementDictionary.Add(int.Parse(epsDataSource.Id), planingStructure);
                                    Debug.WriteLine(planingStructure.Name);
                                    break;
                                case ("8"):
                                    RoadNetworkElement roadNetworkElement = new RoadNetworkElement();
                                    roadNetworkElement.Name = reader.GetAttribute("NAME")!;
                                    ErnDataSource ernDataSource = new ErnDataSource();
                                    ernDataSource.Ern = roadNetworkElement;
                                    ernDataSource.Id = reader.GetAttribute("OBJECTID")!;
                                    ernDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                    roadNetworkElement.DataSources.Add(ernDataSource);
                                    roadNetworkElementDictionary.Add(int.Parse(ernDataSource.Id), roadNetworkElement);
                                    Debug.WriteLine(roadNetworkElement.Name);
                                    break;
                            }

                            Debug.WriteLine($"Start Element {reader.GetAttribute("OBJECTGUID")} {reader.GetAttribute("NAME")}");
                            break;
                        case XmlNodeType.Text:
                            Console.WriteLine("Text Node: {0}",
                                     await reader.GetValueAsync());
                            break;
                        case XmlNodeType.EndElement:
                            Console.WriteLine("End Element {0}", reader.Name);
                            break;
                        default:
                            Console.WriteLine("Other node {0} with value {1}",
                                            reader.NodeType, reader.Value);
                            break;


                    }
                }
            }

        }

        async public void ConvertBuildings(String uri)
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
                            buildingDataSource.Id = reader.GetAttribute("OBJECTID")!;
                            buildingDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                            building.DataSources.Add(buildingDataSource);
                            if (!buildingDictionary.TryAdd(int.Parse(buildingDataSource.Id), building))
                                Debug.WriteLine($"Не удалось добавить здание с id: {int.Parse(buildingDataSource.Id)}" );
                            Debug.WriteLine(building.Number);

                            Debug.WriteLine($"Start Element {reader.GetAttribute("OBJECTGUID")} {reader.GetAttribute("NAME")}");
                            break;
                        case XmlNodeType.Text:
                            Console.WriteLine("Text Node: {0}",
                                     await reader.GetValueAsync());
                            break;
                        case XmlNodeType.EndElement:
                            Console.WriteLine("End Element {0}", reader.Name);
                            break;
                        default:
                            Console.WriteLine("Other node {0} with value {1}",
                                            reader.NodeType, reader.Value);
                            break;

                    }
                }
            }

        }

        async public void ReadAdmHierarchy(String uri)
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
                            Debug.WriteLine($"Start Element {reader.GetAttribute("OBJECTGUID")} {reader.GetAttribute("NAME")}");
                            if(region.Code == null)
                            region.Code = reader.GetAttribute("REGIONCODE")!;//TODO как вытащить код региона не выполняя этот код каждую итерацию
                            break;
                        case XmlNodeType.Text:
                            Console.WriteLine("Text Node: {0}",
                                     await reader.GetValueAsync());
                            break;
                        case XmlNodeType.EndElement:
                            Console.WriteLine("End Element {0}", reader.Name);
                            break;
                        default:
                            Console.WriteLine("Other node {0} with value {1}",
                                            reader.NodeType, reader.Value);
                            break;

                    }
                }
            }
        }
        public async Task ReadMunHierarchy(String uri)
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
                            Debug.WriteLine($"Start Element {reader.GetAttribute("OBJECTGUID")} {reader.GetAttribute("NAME")}");
                            break;
                        case XmlNodeType.Text:
                            Console.WriteLine("Text Node: {0}",
                                     await reader.GetValueAsync());
                            break;
                        case XmlNodeType.EndElement:
                            Console.WriteLine("End Element {0}", reader.Name);
                            break;
                        default:
                            Console.WriteLine("Other node {0} with value {1}",
                                            reader.NodeType, reader.Value);
                            break;

                    }
                }
            }

        }


        public IReadOnlyCollection<Address> BuildAddresses()
        {
            var addresses = new List<Address>();
            foreach(var item in buildingDictionary)
            {
                var address = new Address();
                address.Building = item.Value;
                //TODO убрать хардкод региона и страны
                address.Country = country;
                address.Region = region;
                FindParents(9, address);
                // Создание нормализованой строки.
                address.GetNormalizedAddress();
                var normalizedString = address.GetNormalizedAddress;
                Debug.WriteLine("NORMALIZED STRING: " + normalizedString);
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
                    objId = int.Parse(address.Building!.DataSources.First().Id);
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
                                    address.RoadNetworkElement = (RoadNetworkElement)entry.Value.BaseEntity;

                                else if(levelNumber == 7)
                                    address.PlaningStructureElement = (PlaningStructureElement)entry.Value.BaseEntity;
                                FindParents(levelNumber, address);
                            }
                        }
                    }
                    break;
                case (8):
                    objId = int.Parse(address.RoadNetworkElement!.DataSources.First().Id);
                    AdmHierarchy.TryGetValue(objId, out parentObjId);
                    levelEntryParentMappings = levelToParentMap[8];
                    foreach (var mapper in levelEntryParentMappings)
                    {
                        var entry = mapper(parentObjId);

                        if (entry is not null)
                        {
                            var levelNumber = entry.Value.LevelNumber;
                            if (levelNumber == 7)
                                address.PlaningStructureElement = (PlaningStructureElement)entry.Value.BaseEntity;
                            else if (levelNumber == 6)
                                address.Settlement = (Settlement)entry.Value.BaseEntity;
                            else if (levelNumber == 5)
                                address.City = (City)entry.Value.BaseEntity;
                            FindParents(levelNumber, address);
                        }
                    }
                    break;
                case (7):
                    objId = int.Parse(address.PlaningStructureElement!.DataSources.First().Id);
                    AdmHierarchy.TryGetValue(objId, out parentObjId);
                    levelEntryParentMappings = levelToParentMap[7];
                    foreach (var mapper in levelEntryParentMappings)
                    {
                        var entry = mapper(parentObjId);

                        if (entry is not null)
                        {
                            var levelNumber = entry.Value.LevelNumber;
                            if (levelNumber == 6)
                                address.Settlement = (Settlement)entry.Value.BaseEntity;
                            else if (levelNumber == 5)
                                address.City = (City)entry.Value.BaseEntity;
                            FindParents(levelNumber, address);
                        }
                    }
                    break;
                case (6):
                    objId = int.Parse(address.Settlement!.DataSources.First().Id);
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
                                address.City = (City)entry.Value.BaseEntity;
                            FindParents(levelNumber, address);
                        }
                        if (entryMunicipal is not null)
                        {
                            var LevelNumber = entryMunicipal.Value.LevelNumber;
                            if (LevelNumber == 4)
                                address.Territory = (Territory)entryMunicipal.Value.BaseEntity;
                            FindParents(LevelNumber, address);
                        }
                    }
                    break;
                case (5):
                    objId = int.Parse(address.City!.DataSources.First().Id);
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
                                address.AdministrativeArea = (AdministrativeArea)entry.Value.BaseEntity;
                        }

                        if (entryMunicipal is not null)
                        {
                            var levelNumber = entryMunicipal.Value.LevelNumber;
                            if (levelNumber == 4)
                                address.Territory = (Territory)entryMunicipal.Value.BaseEntity;
                            FindParents(levelNumber, address);
                            if (levelNumber == 3)
                                address.MunicipalArea = (MunicipalArea)entryMunicipal.Value.BaseEntity;
                            FindParents(levelNumber, address);
                        }
                    }
                    break;
                case (4):
                    objId = int.Parse(address.Territory!.DataSources.First().Id);
                    MunHierarchy.TryGetValue(objId, out parentMunicipalityObjId);
                    levelEntryParentMappings = levelToParentMap[4];
                    foreach (var mapper in levelEntryParentMappings)
                    {
                        var entryMunicipal = mapper(parentMunicipalityObjId);

                        if (entryMunicipal is not null)
                        {
                            var levelNumber = entryMunicipal.Value.LevelNumber;
                            if (levelNumber == 3)
                                address.MunicipalArea = (MunicipalArea)entryMunicipal.Value.BaseEntity;
                        }
                    }
                    break;
            }
        }

    }
}