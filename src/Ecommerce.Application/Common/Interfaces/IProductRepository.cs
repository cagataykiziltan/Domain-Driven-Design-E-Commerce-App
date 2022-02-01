using Ecommence.Domain.Products;

namespace Ecommence.Application.Common.Interfaces
{
    public interface IProductRepository
    {
        void Create(Product entity);
        Product GetByProductCode(string productCode);
    }
}
