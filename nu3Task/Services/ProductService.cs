using Microsoft.EntityFrameworkCore;
using nu3Task.Entities;
using nu3Task.Helpers;
using nu3Task.Interfaces;
using nu3Task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nu3Task.Services
{
    public class ProductService : IProductService
    {
        private readonly nu3Context _nu3Context;

        public ProductService(nu3Context nu3Context)
        {
            _nu3Context = nu3Context;
        }

        /// <summary>
        /// Update old products (including related images) in db with new ones
        /// </summary>
        /// <param name="products">products</param>
        /// <returns>void</returns>
        public async Task UpdateProducts(products products)
        {
            try
            {
                List<Product> productRecords = new List<Product>();
                List<Image> imageRecords = new List<Image>();

                foreach (var product in products.product)
                {
                    productRecords.Add(new Product
                    {
                        BodyHtml = product.bodyhtml,
                        CreatedAt = product.createdat.Value,
                        Handle = product.handle,
                        ProductId = long.Parse(product.id.Value.ToString()),
                        ProductType = product.producttype,
                        PublishedScope = product.publishedscope,
                        Tags = product.tags,
                        Title = product.title,
                        Vendor = product.vendor
                    });

                    imageRecords.Add(new Image
                    {
                        ProductId = long.Parse(product.image.productid.Value.ToString()),
                        ImageId = long.Parse(product.image.id.Value.ToString()),
                        CreatedAt = product.image.createdat.Value,
                        UpdatedAt = product.image.updatedat.Value,
                        Height = product.image.height.Value,
                        Width = product.image.width.Value,
                        Src = product.image.src
                    });
                }

                // Update products
                _nu3Context.Products.RemoveRange(await _nu3Context.Products.ToListAsync());
                _nu3Context.Products.AddRange(productRecords);

                // Update images
                _nu3Context.Images.RemoveRange(await _nu3Context.Images.ToListAsync());
                _nu3Context.Images.AddRange(imageRecords);

                // Save changes to db
                await _nu3Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update products: {ex.Message}");
            }
        }

        /// <summary>
        /// Parse the xml string into a list of nu3 products
        /// </summary>
        /// <param name="xmlContent">string</param>
        /// <returns>products</returns>
        public products ParseProducts(string xmlContent)
        {
            try
            {
                var products = xmlContent.ParseXml<products>();
                return products;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to parse products, please check the file: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _nu3Context.Products
                .Join(_nu3Context.Images,
                prod => prod.ProductId,
                img => img.ProductId,
                (prod, img) => new
                Product{
                    ProductId = prod.ProductId,
                    Title = prod.Title,
                    Vendor = prod.Vendor,
                    CreatedAt = prod.CreatedAt,
                    ImgSrc = img.Src
                }).ToListAsync();

            return products;
        }
    }
}
