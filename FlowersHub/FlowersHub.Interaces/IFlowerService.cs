using System.Collections.Generic;
using System.Threading.Tasks;
using FlowersHub.Model;

namespace FlowersHub.Interfaces
{
    public interface IFlowerService
    {
        Task LoadAllFlowers();
        Task LoadFlowers(IFlowersSource source);
        Task AddFlower(Flower flower);
        Task AddFlowers(IEnumerable<Flower> flowers);
        Task UpdateFlower(Flower flower);
        void UpdateColors(Flower flower);
        void UpdateFlowerTypes(Flower flower);
        Dictionary<string,int> GetPopularWords(int count);
        Task FixDescriptions();
        Task AddFlowerType(string key, string[] variations);
        Task AddColorType(string key, string[] variations);
    }
}
