using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowersHub.Data;
using FlowersHub.Infrastructure;
using FlowersHub.Interfaces;
using FlowersHub.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FlowersHub.Services
{
    public class FlowerService : IFlowerService
    {
        private readonly FlowersHubContext _context;
        private readonly ILogger<FlowerService> _logger;

        private readonly IFlowerTypeService _flowerTypeService;
        private readonly IColorTypeService _colorTypeService;
        private readonly IGroupTypeService _groupTypeService;

        public FlowerService(FlowersHubContext context,
            IFlowerTypeService flowerTypeService,
            IColorTypeService colorTypeService,
            IGroupTypeService groupTypeService,
            ILogger<FlowerService> logger)
        {
            _logger = logger;
            _context = context;
            _sources.Add(new FlowerUaSource(_context, logger));
            _flowerTypeService = flowerTypeService;
            _colorTypeService = colorTypeService;
            _groupTypeService = groupTypeService;
        }

        private readonly List<IFlowersSource> _sources = new List<IFlowersSource>();

        public async Task LoadAllFlowers()
        {
            _logger.LogInformation("LoadAllFlowers");
            foreach (var flowersSource in _sources)
            {
                var flowers = await flowersSource.Load();
                await AddFlowers(flowers);
            }
        }

        public async Task LoadFlowers(IFlowersSource source)
        {
            var flowers = await source.Load();
            await AddFlowers(flowers);
        }

        public async Task AddFlower(Flower flower)
        {
            _logger.LogInformation($"AddFlower {flower.Url}");
            var flowerDb = _context.Flowers.FirstOrDefault(item => item.Url == flower.Url);
            if (flowerDb != null)
            {
                await UpdateFlower(flower);
                return;
            }

            _flowerTypeService.UpdateFlowerTypes(flower);
            _colorTypeService.UpdateColors(flower);
            await _groupTypeService.AddGroupType(flower.Group);

            await _context.Flowers.AddAsync(flower);
            await _context.SaveChangesAsync();
        }


        public async Task AddFlowers(IEnumerable<Flower> flowers)
        {
            _logger.LogInformation("AddFlowers");
            await flowers.ForEachAsync(AddFlower);
        }

        public async Task UpdateFlower(Flower flower)
        {
            _logger.LogInformation("UpdateFlower");
            var flowerDb = _context.Flowers.FirstOrDefault(item => item.Url == flower.Url);
            if (flowerDb == null)
            {
                await AddFlower(flower);
                return;
            }

            _flowerTypeService.UpdateFlowerTypes(flower);
            _colorTypeService.UpdateColors(flower);
            await _groupTypeService.AddGroupType(flower.Group);

            flowerDb.Title = flower.Title;
            flowerDb.Description = flower.Description;
            flowerDb.Currency = flower.Currency;
            flowerDb.PriceDouble = flower.PriceDouble;
            flowerDb.ImageUrl = flower.ImageUrl;
            flowerDb.Group = flower.Group;

            await _context.SaveChangesAsync();
        }
        
        public async Task<Dictionary<string, int>> GetPopularWords(int count)
        {
            var variations = _context.FlowerTypes.ToList().SelectMany(item => item.Variations.Split(' '))
                .Union(_context.Colors.ToList().SelectMany(item => item.Variations.Split(' ')));

            var flowers = _context.Flowers.ToList();
            return flowers.SelectMany(flower => flower.Description.SplitAndReplace())
                .GroupBy(item => item)
                .ToDictionary(item => item.Key, item => item.Count())
                .Where(item => !variations.Contains(item.Key))
                .Where(item => item.Key.Length > 2)
                .OrderByDescending(item => item.Value)
                .Take(count)
                .ToDictionary(item => item.Key, item => item.Value);
        }

        public async Task FixDescriptions()
        {
            var flowers = _context.Flowers.ToList();
            flowers.ForEach(flower => flower.Description = flower.Description
                .Replace("&nbsp;", " ")
                .Replace("&#34;", "") 
                .Replace("Состав:", " Состав:")
                .Replace("букета:", "букета: "));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Flower>> GetAll(int skip, int take)
        {
            var result = _context.Flowers.OrderBy(item => item.PriceDouble).Skip(skip);
            if (take >= 0) {
                result = result.Take(take);
            }
            return await result.ToListAsync();
        }
    }
}
