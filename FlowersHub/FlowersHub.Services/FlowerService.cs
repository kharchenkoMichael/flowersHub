using System.Collections.Generic;
using System.Linq;
using FlowersHub.Data;
using FlowersHub.Infrastructure;
using FlowersHub.Interfaces;
using FlowersHub.Model;

namespace FlowersHub.Services
{
    public class FlowerService : IFlowerService
    {
        private readonly FlowersHubContext _context;

        public FlowerService(FlowersHubContext context)
        {
            _context = context;
        }

        private List<IFlowersSource> _sources = new List<IFlowersSource>()
        {
            new FlowerUaSource()
        };

        public void LoadAllFlowers()
        {
            foreach (var flowersSource in _sources)
            {
                flowersSource.Load();
            }
        }

        public void LoadFlowers(IFlowersSource source)
        {
            source.Load();
        }

        public void AddFlower(Flower flower)
        {
            var flowerDb = _context.Flowers.FirstOrDefault(item => item.Url == flower.Url);
            if (flowerDb != null)
            {
                UpdateFlower(flower);
                return;
            }

            UpdateFlowerTypes(flower);
            UpdateColors(flower);

            _context.Flowers.Add(flower);
            _context.SaveChanges();
        }


        public void AddFlowers(IEnumerable<Flower> flowers)
        {
            flowers.ForEach(AddFlower);
        }

        public void UpdateFlower(Flower flower)
        {
            var flowerDb = _context.Flowers.FirstOrDefault(item => item.Url == flower.Url);
            if (flowerDb == null)
            {
                AddFlower(flower);
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

            _context.SaveChanges();
        }

        public void UpdateFlowerTypes(Flower flower)
        {
            flower.FlowerTypes.ForEach(flowerType => flowerType.Flowers.Remove(flower));
            flower.FlowerTypes = new List<FlowerType>();

            foreach (var flowerType in _context.FlowerTypes)
            {
                var variations = flowerType.Variations.Split(' ');
                var words = flower.Description
                    .ToLower()
                    .Replace(".", "")
                    .Replace(",", "")
                    .Replace("(", "")
                    .Replace(")", "")
                    .Split(' ');

                if (!words.Any(word => variations.Contains(word)))
                    continue;

                flower.FlowerTypes.Add(flowerType);
                flowerType.Flowers.Add(flower);
            }
        }

        public void UpdateColors(Flower flower)
        {
            flower.Colors.ForEach(color => color.Flowers.Remove(flower));
            flower.Colors = new List<ColorType>();

            foreach (var color in _context.Colors)
            {
                var variations = color.Variations.Split(' ');
                var words = flower.Description
                    .ToLower()
                    .Replace(".", "")
                    .Replace(",", "")
                    .Replace("(", "")
                    .Replace(")", "")
                    .Split(' ');

                if (!words.Any(word => variations.Contains(word)))
                    continue;

                flower.Colors.Add(color);
                color.Flowers.Add(flower);
            }
        }
    }
}
