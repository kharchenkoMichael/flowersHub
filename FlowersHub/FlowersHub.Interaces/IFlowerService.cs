using System.Collections.Generic;
using FlowersHub.Model;

namespace FlowersHub.Interfaces
{
    public interface IFlowerService
    {
        void LoadAllFlowers();
        void LoadFlowers(IFlowersSource source);
        void AddFlower(Flower flower);
        void AddFlowers(IEnumerable<Flower> flowers);
        void UpdateFlower(Flower flower);
        void UpdateColors(Flower flower);
        void UpdateFlowerTypes(Flower flower);
    }
}
