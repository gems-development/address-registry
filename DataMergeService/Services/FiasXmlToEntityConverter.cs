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
            List<AdministrativeArea> administrativeAreas = new List<AdministrativeArea>();
            List<MunicipalArea> municipalAreas = new List<MunicipalArea>();
            List<City> cityList = new List<City>();
            List<PlaningStructureElement> planingStructureElements = new List<PlaningStructureElement>();
            List<RoadNetworkElement> roadNetworkElements = new List<RoadNetworkElement>;
            List<Building> buildings = new List<Building>();


            using (XmlReader reader = XmlReader.Create(uri, settings))
            {
            reader.MoveToContent();
                while (await reader.ReadAsync())
                {

                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.GetAttribute("NEXTID") != "0" && reader.GetAttribute("OPERTYPEID") != "20")
                                break;
                            switch (reader.GetAttribute("TYPENAME"))
                            {
                                case "обл":
                                    break;

                                case ("г"):
                                    City city = new City();
                                    city.Name = reader.GetAttribute("NAME");
                                    CityDataSource cityDataSource = new CityDataSource();
                                    cityDataSource.City = city;
                                    cityDataSource.Id = reader.GetAttribute("OBJECTGUID");
                                    cityDataSource.SourceType = AddressRegistry.Entities.Enums.SourceType.Fias;
                                    city.DataSources.Add(cityDataSource);
                                    cityList.Add(city);
                                    Debug.WriteLine(city.Name);
                                    break;
                                case (""):
                                    break;
                                case ("ул"):
                                    Debug.WriteLine("Улица");
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


            return region;
        }
    }
}
