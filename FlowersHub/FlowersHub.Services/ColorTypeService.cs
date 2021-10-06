using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowersHub.Data;
using FlowersHub.Infrastructure;
using FlowersHub.Interfaces;
using FlowersHub.Model;
using Microsoft.EntityFrameworkCore;

namespace FlowersHub.Services
{
    public class ColorTypeService : IColorTypeService
    {
        private readonly FlowersHubContext _context;

        public ColorTypeService(FlowersHubContext context)
        {
            _context = context;
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
        
        public async Task UpdateAllColorTypes()
        {
            await RemoveColorTypesFromAllFlowers();
            _context.Flowers
                .Include(f => f.Colors)
                .ToList()
                .ForEach(UpdateColors);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetAll()
        {
            return await _context.Colors.Select(item => item.Name).ToListAsync();
        }

        private async Task RemoveColorTypesFromAllFlowers()
        {
            _context.Flowers
                .Include(f => f.Colors)
                .ToList()
                .ForEach(f => f.Colors = new List<ColorType>());
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
