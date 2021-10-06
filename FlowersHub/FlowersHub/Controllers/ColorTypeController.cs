using System.Collections.Generic;
using System.Threading.Tasks;
using FlowersHub.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlowersHub.Controllers
{
    [Route("api/[controller]")]
    public class ColorTypeController
    {
        private IColorTypeService _colorTypeService;

        public ColorTypeController(IColorTypeService colorTypeService)
        {
            _colorTypeService = colorTypeService;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetColorType()
        {
            return await _colorTypeService.GetAll();
        }

        [HttpPost("add/colorType")]
        public async Task AddColorType(string key, string[] variations)
        {
            await _colorTypeService.AddColorType(key, variations);
        }

        [HttpPost("update/colorTypes")]
        public async Task UpdateColorTypes()
        {
            await _colorTypeService.UpdateAllColorTypes();
        }
    }
}
