using nu3Task.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nu3Task.Interfaces
{
    public interface IProductService
    {
        public Task AddProducts(List<Product> products);
    }
}
