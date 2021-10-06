using System.Collections.Generic;
using System.Threading.Tasks;
using FlowersHub.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlowersHub.Controllers
{
    [Route("api/[controller]")]
    public class FlowerTypeController
    {
        private IFlowerTypeService _flowerTypeService;

        public FlowerTypeController(IFlowerTypeService flowerTypeService)
        {
            _flowerTypeService = flowerTypeService;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetFlowerTypes()
        {
            return await _flowerTypeService.GetAll();
        }

        [HttpPost("add/flowerType")]
        public async Task AddFlowerType(string key, string[] variations)
        {
            await _flowerTypeService.AddFlowerType(key, variations);
        }

        [HttpPost("update/flowerTypes")]
        public async Task UpdateFlowerTypes()
        {
            await _flowerTypeService.UpdateAllFlowerTypes();
        }
    }
}
