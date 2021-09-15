using System.Collections.Generic;
using System.Threading.Tasks;
using FlowersHub.Model;

namespace FlowersHub.Interfaces
{
    public interface IFlowersSource
    {
        Task<List<Flower>> Load();
    }
}
