using System.Collections.Generic;
using System.Threading.Tasks;
using FlowersHub.Model;

namespace FlowersHub.Interfaces
{
    public interface IColorTypeService
    {
        void UpdateColors(Flower flower);
        Task AddColorType(string key, string[] variations);
        Task UpdateAllColorTypes();
        Task<IEnumerable<string>> GetAll();
    }
}
