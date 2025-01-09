using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data.Data;
using Domain.Models.Sale;
using Infrastructure.Contracts;

namespace Infrastructure.Implementation;

public class SaleService : ISaleService
{
    private readonly ApplicationDbContext _context;

    public SaleService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SaleViewDto>> GetAllSalesAsync()
    {
        return await _context.Sales
            .Include(s => s.Product)
            .Include(s => s.Customer)
            .Select(sale => new SaleViewDto
            {
                SaleId = sale.SaleId,
                ProductName = sale.Product.Name,
                CustomerName = sale.Customer.Name,
                SaleDate = sale.SaleDate,
                Quantity = sale.Quantity,
            })
            .ToListAsync();
    }

    public async Task<SaleViewDto> GetSaleByIdAsync(int id)
    {
        var sale = await _context.Sales
            .Include(s => s.Product)
            .Include(s => s.Customer)
            .FirstOrDefaultAsync(s => s.SaleId == id);

        if (sale == null) return null;

        return new SaleViewDto
        {
            SaleId = sale.SaleId,
            ProductName = sale.Product.Name,
            CustomerName = sale.Customer.Name,
            SaleDate = sale.SaleDate,
            Quantity = sale.Quantity,
        };
    }

    public async Task<SaleViewDto> CreateSaleAsync(SaleInputDto saleInputModel)
    {
        var sale = new Data.Models.Sale
        {
            ProductId = saleInputModel.ProductId,
            CustomerId = saleInputModel.CustomerId,
            SaleDate = saleInputModel.SaleDate,
            Quantity = saleInputModel.Quantity,
        };

        _context.Sales.Add(sale);
        await _context.SaveChangesAsync();

        return new SaleViewDto
        {
            SaleId = sale.SaleId,
            ProductName = sale.Product.Name,
            CustomerName = sale.Customer.Name,
            SaleDate = sale.SaleDate,
            Quantity = sale.Quantity,
        };
    }

    public async Task<SaleViewDto> UpdateSaleAsync(int id, SaleUpdateDto saleUpdateModel)
    {
        var existingSale = await _context.Sales.FindAsync(id);
        if (existingSale == null) return null;

        existingSale.ProductId = saleUpdateModel.ProductId;
        existingSale.CustomerId = saleUpdateModel.CustomerId;
        existingSale.SaleDate = saleUpdateModel.SaleDate;
        existingSale.Quantity = saleUpdateModel.Quantity;

        await _context.SaveChangesAsync();

        return new SaleViewDto
        {
            SaleId = existingSale.SaleId,
            ProductName = existingSale.Product.Name,
            CustomerName = existingSale.Customer.Name,
            SaleDate = existingSale.SaleDate,
            Quantity = existingSale.Quantity,
        };
    }

    public async Task<bool> DeleteSaleAsync(int id)
    {
        var sale = await _context.Sales.FindAsync(id);
        if (sale == null) return false;

        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync();
        return true;
    }
}
