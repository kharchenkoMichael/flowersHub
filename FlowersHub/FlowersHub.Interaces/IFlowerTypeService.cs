using System.Collections.Generic;
using System.Threading.Tasks;
using FlowersHub.Model;

namespace FlowersHub.Interfaces
{
    public interface IFlowerTypeService
    {
        void UpdateFlowerTypes(Flower flower);
        Task AddFlowerType(string key, string[] variations);
        Task UpdateAllFlowerTypes();
        Task<IEnumerable<string>> GetAll();
    }
}
