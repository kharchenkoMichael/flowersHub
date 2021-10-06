using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlowersHub.Interfaces
{
    public interface IGroupTypeService
    {
        Task AddGroupType(string key);
        Task UpdateAllGroupTypes();
        Task<IEnumerable<string>> GetAll();
    }
}
