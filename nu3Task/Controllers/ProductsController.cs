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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private const string PRODUCTS_FILE_EXT = ".xml";
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get all products (including images)
        /// </summary>
        /// <returns>List of products</returns>
        [HttpGet("get-products")]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            try
            {
                var products = await _productService.GetProducts();
                return products;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get products: {ex.Message}");
            }
        }

        /// <summary>
        /// Update the products from xml file
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpPost("update-products")]
        public async Task<IActionResult> UpdateProducts()
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
                else if (extension != PRODUCTS_FILE_EXT)
                {
                    throw new Exception($"Products file must be in {PRODUCTS_FILE_EXT} format!");
                }
                else
                {
                    // Create the stream reader
                    using StreamReader reader = new StreamReader(file.OpenReadStream());

                    // Get xml file content
                    string content = reader.ReadToEnd();

                    // Parse the file into nu3 product entities
                    var products = _productService.ParseProducts(content);

                    // Update the db with new products
                    await _productService.UpdateProducts(products);

                    return new OkResult();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update the products: {ex.Message}");
            }
        }
    }
}
