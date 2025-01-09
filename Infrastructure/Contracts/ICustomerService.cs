using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Models;
using Domain.Models.Category;
using Domain.Models.Customer;

namespace Infrastructure.Contracts;

public interface ICustomerService
{
    Task<IEnumerable<CustomerViewDto>> GetAllCustomersAsync();
    Task<CustomerViewDto> GetCustomerByIdAsync(int id);
    Task<CustomerViewDto> CreateCustomerAsync(CustomerInputDto customerInputModel);
    Task<CustomerViewDto> UpdateCustomerAsync(int id, CustomerUpdateDto customerUpdateModel);
    Task<bool> DeleteCustomerAsync(int id);
}
