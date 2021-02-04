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

        /// <summary>
        /// Get all inventory records
        /// </summary>
        /// <returns>List of records</returns>
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

        /// <summary>
        /// Convert a csv string into a list of inventory records
        /// </summary>
        /// <param name="csvContent">string</param>
        /// <returns>List of Inventory</returns>
        public IEnumerable<Inventory> ParseInventoryRecords(string csvContent)
        {
            List<Inventory> inventory = new List<Inventory>();

            // Parse the inventory records
            var lines = csvContent.Split(
                new[] { Environment.NewLine },
                StringSplitOptions.None);

            foreach (var line in lines)
            {
                var record = line.Split(';')
                    .Select(s => s.Replace("\"", ""))
                    .ToArray();

                // Add record to the result
                inventory.Add(new Inventory
                {
                    Handle = record[0],
                    Location = record[1],
                    Amount = double.Parse(record[2])
                });
            }

            return inventory;
        }

        /// <summary>
        /// Update the inventory records (replace old records with new ones)
        /// </summary>
        /// <param name="inventories">List of inventory records</param>
        /// <returns>void</returns>
        public async Task UpdateInventory(IEnumerable<Inventory> inventories)
        {
            try
            {
                // Delete existing records
                _nu3Context.Inventories.RemoveRange(await _nu3Context.Inventories.ToListAsync());

                // Add new ones
                _nu3Context.Inventories.AddRange(inventories);

                // Update the DB
                await _nu3Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update the inventory: {ex.Message}");
            }
        }
    }
}
