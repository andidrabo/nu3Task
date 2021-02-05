using nu3Task.Entities;
using nu3Task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nu3Task.Interfaces
{
    public interface IProductService
    {
        public products ParseProducts(string xmlContent);

        public Task UpdateProducts(products products);

        public Task<IEnumerable<Product>> GetProducts();
    }
}
