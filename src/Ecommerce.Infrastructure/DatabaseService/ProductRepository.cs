using Ecommence.Domain.Products;
using System.Collections.Generic;
using System.Linq;
using Ecommence.Application.Common.Interfaces;

namespace Ecommence.Infrastructure.DatabaseService
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products;
        public ProductRepository()
        {
            _products = new List<Product>();
        }
        public void Create(Product entity) => _products.Add(entity);
   
        public Product GetByProductCode(string productCode) => _products.FirstOrDefault(x => x.ProductCode == productCode);
        
    }
}
