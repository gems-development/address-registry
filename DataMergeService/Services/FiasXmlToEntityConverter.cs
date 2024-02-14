using Gems.AddressRegistry.Entities;
using Gems.AddressRegistry.Entities.DataSources;
using System.Diagnostics;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Xml;

namespace Gems.DataMergeServices.Services
{
    public class FiasXmlToEntityConverter
    {
        public FiasXmlToEntityConverter() { }

        async public Task<Region> ConvertRegion(String uri)
        {

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Async = true;

            Region region = new Region();
            List<AdministrativeArea> administrativeAreaList = new List<AdministrativeArea>();
            List<MunicipalArea> municipalAreaList = new List<MunicipalArea>();
            List<Territory> territoryList = new List<Territory>();
            List<City> cityList = new List<City>();
            List<Settlement> settlementList = new List<Settlement>();
            List<PlaningStructureElement> planingStructureElementList = new List<PlaningStructureElement>();
            List<RoadNetworkElement> roadNetworkElementList = new List<RoadNetworkElement>();
            List<LandPlot> landPlotList = new List<LandPlot>();



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
                                    administrativeAreaList.Add(administrativeArea);
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
                                    municipalAreaList.Add(municipalArea);
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
                                    territoryList.Add(territory);
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
                                    cityList.Add(city);
                                    Debug.WriteLine(city.Name);
                                    break;
                                case("6"):
                                    Settlement settlement = new Settlement();
                                    settlement.Name = reader.GetAttribute("NAME");
                                    SettlementDataSource settlementDataSource = new SettlementDataSource();
                                    settlementDataSource.Settlement = settlement;
                                    settlementDataSource.Id = reader.GetAttribute("OBJECTID");
                                    settlementDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                    settlement.DataSources.Add(settlementDataSource);
                                    settlementList.Add(settlement);
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
                                    planingStructureElementList.Add(planingStructure);
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
                                    roadNetworkElementList.Add(roadNetworkElement);
                                    Debug.WriteLine(roadNetworkElement.Name);
                                    break;
                                    // В другом файле участки
                                //case ("9"):
                                //    LandPlot landPlot = new LandPlot();
                                //    landPlot.Name = reader.GetAttribute("NAME");
                                //    LandPlotDataSource landPlotDataSource = new LandPlotDataSource();
                                //    landPlotDataSource.LandPlot = landPlot;
                                //    landPlotDataSource.Id = reader.GetAttribute("OBJECTGUID");
                                //    landPlotDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                //    landPlot.DataSources.Add(landPlotDataSource);
                                //    landPlotList.Add(landPlot);
                                //    Debug.WriteLine(landPlot.Name);
                                //    break;


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
            Debug.WriteLine("_______________________________________________");
            Debug.WriteLine("Region");
            Debug.WriteLine(region.Name);
            Debug.WriteLine("A area");
            Debug.WriteLine(administrativeAreaList.Count);
            Debug.WriteLine("M area");
            Debug.WriteLine(municipalAreaList.Count);
            Debug.WriteLine("Territory");
            Debug.WriteLine(territoryList.Count);
            Debug.WriteLine("City");
            Debug.WriteLine(cityList.Count);
            Debug.WriteLine("Settlement");
            Debug.WriteLine(settlementList.Count);
            Debug.WriteLine("PSE");
            Debug.WriteLine(planingStructureElementList.Count);
            Debug.WriteLine("RNE");
            Debug.WriteLine(roadNetworkElementList.Count);
            Debug.WriteLine("Landplots");
            Debug.WriteLine(landPlotList.Count);
            Debug.WriteLine("_______________________________________________");

            return region;
        }
    }
}
