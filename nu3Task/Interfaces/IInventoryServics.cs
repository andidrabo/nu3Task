using nu3Task.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nu3Task.Interfaces
{
    public interface IInventoryService
    {
        public Task UpdateInventory(IEnumerable<Inventory> inventories);

        public Task<IEnumerable<Inventory>> GetInventory();

        public IEnumerable<Inventory> ParseInventoryRecords(string csvContent);
    }
}
