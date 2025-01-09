using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models;
using Domain.Models.Product;

namespace Infrastructure.Contracts;

public interface IProductService
{
    Task<IEnumerable<ProductViewDto>> GetAllProductsAsync();
    Task<ProductViewDto> GetProductByIdAsync(int id);
    Task<ProductViewDto> CreateProductAsync(ProductInputDto productInputModel);
    Task<ProductViewDto> UpdateProductAsync(int id, ProductUpdateDto productUpdateModel);
    Task<bool> DeleteProductAsync(int id);
}