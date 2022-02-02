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
    public class GroupTypeService : IGroupTypeService
    {
        private readonly FlowersHubContext _context;

        public GroupTypeService(FlowersHubContext context)
        {
            _context = context;
        }

        public async Task AddGroupType(string key)
        {
            if (string.IsNullOrEmpty(key))
                return;

            var groupValue = await _context.GroupTypes.FirstOrDefaultAsync(item => item.Name == key);
            if (groupValue != null)
                return;

            var groupType = new GroupType() { Name = key };

            await _context.GroupTypes.AddAsync(groupType);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetAll()
        {
            return await _context.GroupTypes.Select(item => item.Name).ToListAsync();
        }

        public async Task UpdateAllGroupTypes()
        {
            var groups = await _context.Flowers.Select(item => item.Group).Distinct().ToListAsync();
            await groups.ForEachAsync(AddGroupType);
        }
    }
}
