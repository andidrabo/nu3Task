using nu3Task.Entities;
using nu3Task.Interfaces;
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

        public async Task AddProducts(List<Product> products)
        {
            _nu3Context.AddRange(products);
            await _nu3Context.SaveChangesAsync();
        }
    }
}
