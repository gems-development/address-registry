using Gems.AddressRegistry.Entities;
using Gems.AddressRegistry.Entities.DataSources;
using System.Diagnostics;
using System.Xml;

namespace Gems.DataMergeServices.Services
{
    public class FiasXmlToEntityConverter
    {

        Region region = new Region();
        Dictionary<int, AdministrativeArea> administrativeAreaDictionary = new Dictionary<int, AdministrativeArea>();
        Dictionary<int, MunicipalArea> municipalAreaDictionary = new Dictionary<int, MunicipalArea>();
        Dictionary<int, Territory> territoryDictionary = new Dictionary<int, Territory>();
        Dictionary<int, City> cityDictionary = new Dictionary<int, City>();
        Dictionary<int, Settlement> settlementDictionary = new Dictionary<int, Settlement>();
        Dictionary<int, PlaningStructureElement> planingStructureElementDictionary = new Dictionary<int, PlaningStructureElement>();
        Dictionary<int, RoadNetworkElement> roadNetworkElementDictionary = new Dictionary<int, RoadNetworkElement>();
        List<Building> buildingList = new List<Building>();

        Dictionary<int,int> AdmHierarchy = new Dictionary<int, int>();
        Dictionary<int,int> MunHierarchy = new Dictionary<int, int>();

        List<Address> addresses = new List<Address>();

        public FiasXmlToEntityConverter() { }

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
                                    region.Name = reader.GetAttribute("NAME");
                                    RegionDataSource regionDataSource = new RegionDataSource();
                                    regionDataSource.Region = region;
                                    regionDataSource.Id = reader.GetAttribute("OBJECTID");
                                    regionDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                    region.DataSources.Add(regionDataSource);
                                    Debug.WriteLine(region.Name);
                                    break;

                                case ("2"):
                                    AdministrativeArea administrativeArea = new AdministrativeArea();
                                    administrativeArea.Name = reader.GetAttribute("NAME");
                                    AdministrativeAreaDataSource administrativeAreaDataSource = new AdministrativeAreaDataSource();
                                    administrativeAreaDataSource.AdministrativeArea = administrativeArea;
                                    administrativeAreaDataSource.Id = reader.GetAttribute("OBJECTID");
                                    administrativeAreaDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                    administrativeArea.DataSources.Add(administrativeAreaDataSource);
                                    administrativeAreaDictionary.Add(int.Parse(administrativeAreaDataSource.Id), administrativeArea);
                                    Debug.WriteLine(administrativeArea.Name);
                                    break;

                                case ("3"):
                                    MunicipalArea municipalArea = new MunicipalArea();
                                    municipalArea.Name = reader.GetAttribute("NAME");
                                    MunicipalAreaDataSource municipalAreaDataSource = new MunicipalAreaDataSource();
                                    municipalAreaDataSource.MunicipalArea = municipalArea;
                                    municipalAreaDataSource.Id = reader.GetAttribute("OBJECTID");
                                    municipalAreaDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                    municipalArea.DataSources.Add(municipalAreaDataSource);
                                    municipalAreaDictionary.Add(int.Parse(municipalAreaDataSource.Id), municipalArea);
                                    Debug.WriteLine(municipalArea.Name);
                                    break;
                                case ("4"):
                                    Territory territory = new Territory();
                                    territory.Name = reader.GetAttribute("NAME");
                                    TerritoryDataSource terrytoryDataSource = new TerritoryDataSource();
                                    terrytoryDataSource.Territory = territory;
                                    terrytoryDataSource.Id = reader.GetAttribute("OBJECTID");
                                    terrytoryDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                    territory.DataSources.Add(terrytoryDataSource);
                                    territoryDictionary.Add(int.Parse(terrytoryDataSource.Id), territory);
                                    Debug.WriteLine(territory.Name);
                                    break;
                                case ("5"):
                                    City city = new City();
                                    city.Name = reader.GetAttribute("NAME");
                                    CityDataSource cityDataSource = new CityDataSource();
                                    cityDataSource.City = city;
                                    cityDataSource.Id = reader.GetAttribute("OBJECTID");
                                    cityDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                    city.DataSources.Add(cityDataSource);
                                    cityDictionary.Add(int.Parse(cityDataSource.Id), city);
                                    Debug.WriteLine(city.Name);
                                    break;
                                case ("6"):
                                    Settlement settlement = new Settlement();
                                    settlement.Name = reader.GetAttribute("NAME");
                                    SettlementDataSource settlementDataSource = new SettlementDataSource();
                                    settlementDataSource.Settlement = settlement;
                                    settlementDataSource.Id = reader.GetAttribute("OBJECTID");
                                    settlementDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                    settlement.DataSources.Add(settlementDataSource);
                                    settlementDictionary.Add(int.Parse(settlementDataSource.Id), settlement);
                                    Debug.WriteLine(settlement.Name);
                                    break;
                                case ("7"):
                                    PlaningStructureElement planingStructure = new PlaningStructureElement();
                                    planingStructure.Name = reader.GetAttribute("NAME");
                                    EpsDataSource epsDataSource = new EpsDataSource();
                                    epsDataSource.Eps = planingStructure;
                                    epsDataSource.Id = reader.GetAttribute("OBJECTID");
                                    epsDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                    planingStructure.DataSources.Add(epsDataSource);
                                    planingStructureElementDictionary.Add(int.Parse(epsDataSource.Id), planingStructure);
                                    Debug.WriteLine(planingStructure.Name);
                                    break;
                                case ("8"):
                                    RoadNetworkElement roadNetworkElement = new RoadNetworkElement();
                                    roadNetworkElement.Name = reader.GetAttribute("NAME");
                                    ErnDataSource ernDataSource = new ErnDataSource();
                                    ernDataSource.Ern = roadNetworkElement;
                                    ernDataSource.Id = reader.GetAttribute("OBJECTID");
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
                            building.Number = reader.GetAttribute("HOUSENUM");
                            BuildingDataSource buildingDataSource = new BuildingDataSource();
                            buildingDataSource.Building = building;
                            buildingDataSource.Id = reader.GetAttribute("OBJECTID");
                            buildingDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                            building.DataSources.Add(buildingDataSource);
                            buildingList.Add(building);
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

                            if (reader.GetAttribute("ISACTUAL") != "1" || reader.GetAttribute("ISACTIVE") != "1")
                                break;
                            AdmHierarchy.Add(int.Parse(reader.GetAttribute("OBJECTID")), int.Parse(reader.GetAttribute("PARENTOBJID")));
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
        async public void ReadMunHierarchy(String uri)
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
                            MunHierarchy.Add(int.Parse(reader.GetAttribute("OBJECTID")), int.Parse(reader.GetAttribute("PARENTOBJID")));
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

        async public void BuildHierarchy()
        {
            foreach(Building building in buildingList)
            {
                Address address = new Address();
                address.Building = building;
                address.RoadNetworkElement = 

            }
        }
    }
}
