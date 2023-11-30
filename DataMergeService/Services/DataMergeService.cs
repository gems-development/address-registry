using Gems.AddressRegistry.Entities;

namespace Gems.DataMergeService.Services
{
    public class DataMergeService
    {
        public DataMergeService() { }

        public Dictionary<RoadNetworkElement, String> MergeRoadNetworkElements()
        {
            //Импортируем улицы из OSM
            //В базе данных фиас каждой ищем все возможные родительские элементы
            //проверяем в каком из элементов содержиться эти улица с помощью выяснения пространственных отношений
            //составляем пары улица + id родителя
            //Далее в другой функции будем составлять адреса из таких кусочков
            return new Dictionary<RoadNetworkElement, String>();
        }
    }
}
