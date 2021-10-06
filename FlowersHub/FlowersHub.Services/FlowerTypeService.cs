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

namespace FlowersHub.Services
{
    public class FlowerTypeService : IFlowerTypeService
    {
        private readonly FlowersHubContext _context;

        public FlowerTypeService(FlowersHubContext context)
        {
            _context = context;
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

        public async Task UpdateAllFlowerTypes()
        {
            await RemoveFlowerTypesFromAllFlowers();
            _context.Flowers
                .Include(f => f.FlowerTypes)
                .ToList()
                .ForEach(UpdateFlowerTypes);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetAll()
        {
            return await _context.FlowerTypes.Select(item => item.Name).ToListAsync();
        }

        private async Task RemoveFlowerTypesFromAllFlowers()
        {
            _context.Flowers
                .Include(f => f.FlowerTypes)
                .ToList()
                .ForEach(f => f.FlowerTypes = new List<FlowerType>());
            await _context.SaveChangesAsync();
        }
    }
}
