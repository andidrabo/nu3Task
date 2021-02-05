using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nu3Task.Entities;
using nu3Task.Interfaces;

namespace nu3Task.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private const string INVENTORY_FILE_EXT = ".csv";
        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        /// <summary>
        /// Get all inventory records
        /// </summary>
        /// <returns>The inventory</returns>
        [HttpGet("get-inventory")]
        public async Task<IEnumerable<Inventory>> GetInventory()
        {
            try
            {
                var inventory = await _inventoryService.GetInventory();
                return inventory;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get inventory: {ex.Message}");
            }
        }

        /// <summary>
        /// Update the inventory from csv file
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpPost("update-inventory")]
        public async Task<IActionResult> UpdateInventory()
        {
            try
            {
                // Get uploaded file
                var file = Request.Form.Files[0];
                string extension = Path.GetExtension(file.FileName);

                if (file.Length == 0)
                {
                    return BadRequest();
                }
                else if (extension != INVENTORY_FILE_EXT)
                {
                    throw new Exception($"Inventory file must be in {INVENTORY_FILE_EXT} format!");
                }
                else
                {
                    // Create the stream reader
                    using StreamReader reader = new StreamReader(file.OpenReadStream());

                    // Skip the first line
                    reader.ReadLine();

                    string content = reader.ReadToEnd();

                    // Parse the inventory records
                    var records = _inventoryService.ParseInventoryRecords(content);

                    // Update the db with the new inventory
                    await _inventoryService.UpdateInventory(records);

                    return new OkResult();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update the inventory: {ex.Message}");
            }
        }
    }
}
