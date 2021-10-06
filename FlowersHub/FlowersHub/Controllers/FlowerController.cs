using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FlowersHub.Interfaces;
using Microsoft.Extensions.Logging;

namespace FlowersHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowerController : ControllerBase
    {
        private readonly IFlowerService _flowerService;
        private readonly ILogger<FlowerController> _logger;
        public FlowerController(IFlowerService flowerService, ILogger<FlowerController> logger)
        {
            _flowerService = flowerService;
            _logger = logger;
        }

        [HttpPost("load/all")]
        public async Task LoadAll()
        {
            _logger.LogInformation("LoadAll");
            await _flowerService.LoadAllFlowers();
        }

        [HttpGet("popular/words")]
        public async Task<Dictionary<string,int>> GetPopularWords(int count)
        {
            return await _flowerService.GetPopularWords(count);
        }

        [HttpPost("fix/descriptions")]
        public async Task FixDescriptions()
        {
            await _flowerService.FixDescriptions();
        }
    }
}
