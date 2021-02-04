using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using nu3Task.Entities;
using nu3Task.Interfaces;

namespace nu3Task.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet("get-inventory")]
        public async Task<IEnumerable<Inventory>> GetInventory()
        {
            return await _inventoryService.GetInventory();
        }

        [HttpGet("update-inventory")]
        public async Task<IActionResult> UpdateInventory()
        {
            var inventories = new List<Inventory>
            {
                new Inventory
                {
                    Handle = "2-x-beavita-vitalkost-plus-inshape-biomed-schokolade",
                    Location = "Berlin",
                    Amount = 225
                }
            };

            await _inventoryService.UpdateInventory(inventories);
            return new OkObjectResult("OK");
        }
    }
}
