using Microsoft.EntityFrameworkCore;
using nu3Task.Entities;
using nu3Task.Interfaces;
using nu3Task.Models;
using nu3Task.Services;
using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace nu3Task.Test
{
    public class Tests
    {
        private IInventoryService _inventoryService;
        private IProductService _productService;

        private string productsFile = "\\products.xml";
        private string inventoryFile = "\\inventory.csv";

        [SetUp]
        public void Setup()
        {
            _inventoryService = new InventoryService();
            _productService = new ProductService();
        }

        [Test]
        public void Test1_ParseCSVIntoInventoryShouldNotThrowExceptionAndReturnNu3InventoryType()
        {
            try
            {
                FileStream fileStream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), inventoryFile), FileMode.Open);
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    reader.ReadLine();
                    string content = reader.ReadToEnd();

                    var inventory = _inventoryService.ParseInventoryRecords(content);
                    foreach (var record in inventory)
                    {
                        Assert.IsInstanceOf<Inventory>(record);
                    }

                    Assert.Pass();
                }
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        public void Test2_ParseXMLIntoProductsShouldNotThrowExceptionAndReturnNu3ProductType()
        {
            try
            {
                FileStream fileStream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), productsFile), FileMode.Open);
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string content = reader.ReadToEnd();

                    var products = _productService.ParseProducts(content);
                    foreach (var record in products.product)
                    {
                        Assert.IsInstanceOf<productsProduct>(record);
                    }

                    Assert.Pass();
                }
            }
            catch
            {
                Assert.Fail();
            }
        }

    }
}