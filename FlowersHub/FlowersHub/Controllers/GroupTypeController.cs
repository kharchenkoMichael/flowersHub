using FlowersHub.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlowersHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupTypeController : ControllerBase
    {
        private IGroupTypeService _groupTypeService;

        public GroupTypeController(IGroupTypeService groupTypeService)
        {
            _groupTypeService = groupTypeService;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetGroupTypes()
        {
            return await _groupTypeService.GetAll();
        }

        [HttpPost("update/groupTypes")]
        public async Task UpdateGroupTypes()
        {
            await _groupTypeService.UpdateAllGroupTypes();
        }
    }
}
