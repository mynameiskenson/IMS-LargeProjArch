using IMS.Core.Entities;
using IMS.Core.Interfaces;
using IMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Infrastructure.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IMSDevContext _context;

        public InventoryRepository(IMSDevContext context)
        {
            _context = context;
        }
        public async Task<List<Inventory>> GetInventoriesByName(string name)
        {
            return await _context.Inventories.Where(x => x.InventoryName.Contains(name) || string.IsNullOrWhiteSpace(name)).ToListAsync();
        }
    }
}
