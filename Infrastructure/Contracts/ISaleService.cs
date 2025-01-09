using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models;
using Domain.Models.Sale;

namespace Infrastructure.Contracts;

public interface ISaleService
{
    Task<IEnumerable<SaleViewDto>> GetAllSalesAsync();
    Task<SaleViewDto> GetSaleByIdAsync(int id);
    Task<SaleViewDto> CreateSaleAsync(SaleInputDto saleInputModel);
    Task<SaleViewDto> UpdateSaleAsync(int id, SaleUpdateDto saleUpdateModel);
    Task<bool> DeleteSaleAsync(int id);
}