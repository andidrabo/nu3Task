using Microsoft.EntityFrameworkCore;
using nu3Task.Entities;
using nu3Task.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nu3Task.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly nu3Context _nu3Context;

        public InventoryService(nu3Context nu3Context)
        {
            _nu3Context = nu3Context;
        }

        public async Task<IEnumerable<Inventory>> GetInventory()
        {
            var inventory = await _nu3Context.Inventories
                .Join(_nu3Context.Products,
                inv => inv.Handle,
                prod => prod.Handle,
                (inv, prod) => new
                 Inventory
                {
                    Id = inv.Id,
                    Handle = inv.Handle,
                    Amount = inv.Amount,
                    Location = inv.Location,
                    Title = prod.Title
                }).ToListAsync();

            return inventory;
        }

        public async Task UpdateInventory(List<Inventory> inventories)
        {
            _nu3Context.Inventories.RemoveRange(await _nu3Context.Inventories.ToListAsync());
            _nu3Context.Inventories.AddRange(inventories);

            await _nu3Context.SaveChangesAsync();
        }
    }
}
