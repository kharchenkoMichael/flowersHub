using System.Threading.Tasks;
using FlowersHub.Model;

namespace FlowersHub.Interfaces
{
    public interface IFlowerUpdater
    {
        Task Update(Flower flower);
    }
}
