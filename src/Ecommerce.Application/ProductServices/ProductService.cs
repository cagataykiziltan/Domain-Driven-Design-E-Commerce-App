using System;
using System.Threading.Tasks;
using AutoMapper;
using Ecommence.Application.Common.Aspects;
using Ecommence.Application.Common.Interfaces;
using Ecommence.Domain.Products;
using Ecommence.Domain.SeedWork;

namespace Ecommence.Application.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork,
                              IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        [LoggerAspect]
        [TransactionAspect]
        public async Task<ProductDto> CreateProduct(ProductDto productDto)
        {
            if (productDto == null)
                 throw new Exception(MessageConstants.NullParameterError);

            var existingProduct =  _unitOfWork.ProductRepository.GetByProductCode(productDto.ProductCode);

            if (existingProduct != null)
                throw new Exception(MessageConstants.DuplicateProductError);

            var product = Product.Create(Guid.NewGuid(), productDto.ProductCode, productDto.Price, productDto.Stock);

            _unitOfWork.ProductRepository.Create(product);
            _unitOfWork.SaveChanges();
        
            return productDto;
        }

      
        [LoggerAspect]
         public async Task<ProductDto> GetProduct(string productCode)
        {
            var product = _unitOfWork.ProductRepository.GetByProductCode(productCode);

            if (product == null)
                throw new Exception(MessageConstants.NullParameterError);

            var campaign = _unitOfWork.CampaignRepository.GetByProductCode(productCode);

            if (campaign != null)
            {
                var promotedPrice = campaign.ApplyCampaignToPrice(product.Price);
                product.SetPromotedPrice(promotedPrice);
            }

            var productDto = _mapper.Map<ProductDto>(product);
            
            return productDto;
        }
    }
}
