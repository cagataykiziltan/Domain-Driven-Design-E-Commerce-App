using Ecommence.Application.ProductServices;
using System.Threading.Tasks;

namespace Ecommence.Application.Common.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> CreateProduct(ProductDto product);

        Task<ProductDto> GetProduct(string productCode);

    }
}
