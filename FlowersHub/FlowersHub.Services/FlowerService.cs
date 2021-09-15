using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowersHub.Data;
using FlowersHub.Infrastructure;
using FlowersHub.Interfaces;
using FlowersHub.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace FlowersHub.Services
{
    public class FlowerService : IFlowerService
    {
        private readonly FlowersHubContext _context;
        private readonly ILogger<FlowerService> _logger;

        public FlowerService(FlowersHubContext context, ILogger<FlowerService> logger)
        {
            _logger = logger;
            _context = context;
            _sources.Add(new FlowerUaSource(_context, logger));
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

            UpdateFlowerTypes(flower);
            UpdateColors(flower);

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

            UpdateFlowerTypes(flower);
            UpdateColors(flower);

            flowerDb.Title = flower.Title;
            flowerDb.Description = flower.Description;
            flowerDb.Currency = flower.Currency;
            flowerDb.Price = flower.Price;
            flowerDb.ImageUrl = flower.ImageUrl;
            flowerDb.Group = flower.Group;

            await _context.SaveChangesAsync();
        }

        public void UpdateFlowerTypes(Flower flower)
        {
            _context.FlowerTypes.Include(item => item.Flowers)
                .ToList()
                .ForEach(flowerType => UpdateFlowerType(flower, flowerType));
        }

        private void UpdateFlowerType(Flower flower, FlowerType flowerType)
        {
            var variations = flowerType.Variations.Split(' ');
            var words = flower.Description.SplitAndReplace();

            if (!words.Any(variations.Contains))
                return;

            if (!flower.FlowerTypes.Contains(flowerType))
                flower.FlowerTypes.Add(flowerType);

            if (!flowerType.Flowers.Contains(flower))
                flowerType.Flowers.Add(flower);
        }

        public Dictionary<string, int> GetPopularWords(int count)
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
                .Replace("Состав:", " Состав:")
                .Replace("букета:", "букета: "));
            await _context.SaveChangesAsync();
        }

        public async Task AddFlowerType(string key, string[] variations)
        {
            var flowerTypeDb = _context.FlowerTypes.FirstOrDefault(item => item.Name == key);
            if (flowerTypeDb != null)
                throw new ArgumentException($"{key} has already been in db");

            var flowerType = new FlowerType
            {
                Name = key,
                Variations = variations.Aggregate((item1, item2) => item1 + " " + item2)
            };
            _context.FlowerTypes.Add(flowerType);
            var flowers = _context.Flowers.Include(item => item.FlowerTypes).ToList();
            flowers.ForEach(flower => UpdateFlowerType(flower, flowerType));
            await _context.SaveChangesAsync();
        }

        public async Task AddColorType(string key, string[] variations)
        {
            var colorTypeDb = _context.Colors.FirstOrDefault(item => item.Name == key);
            if (colorTypeDb != null)
                throw new ArgumentException($"{key} has already been in db");

            var colorType = new ColorType
            {
                Name = key,
                Variations = variations.Aggregate((item1, item2) => item1 + " " + item2)
            };
            _context.Colors.Add(colorType);
            var flowers = _context.Flowers.Include(item => item.Colors).ToList();
            flowers.ForEach(flower => UpdateColor(flower, colorType));
            await _context.SaveChangesAsync();
        }

        public void UpdateColors(Flower flower)
        {
            _context.Colors.Include(item => item.Flowers)
                .ToList()
                .ForEach(colorType => UpdateColor(flower, colorType));
        }

        private void UpdateColor(Flower flower, ColorType colorType)
        {
            var variations = colorType.Variations.Split(' ');
            var words = flower.Description.SplitAndReplace();

            if (!words.Any(variations.Contains))
                return;

            if (!flower.Colors.Contains(colorType))
                flower.Colors.Add(colorType);

            if (!colorType.Flowers.Contains(flower))
                colorType.Flowers.Add(flower);
        }
    }
}
