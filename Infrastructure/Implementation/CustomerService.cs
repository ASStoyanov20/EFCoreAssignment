using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Data.Data;
using Data.Models;
using Domain.Models.Customer;
using Infrastructure.Contracts;

namespace Infrastructure.Implementation;

public class CustomerService : ICustomerService
{
    private readonly ApplicationDbContext _context;

    public CustomerService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CustomerViewDto>> GetAllCustomersAsync()
    {
        return await _context.Customers
            .Select(customer => new CustomerViewDto
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address,
                DateRegistered = customer.DateRegistered
            })
            .ToListAsync();
    }

    public async Task<CustomerViewDto> GetCustomerByIdAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return null;

        return new CustomerViewDto
        {
            CustomerId = customer.CustomerId,
            Name = customer.Name,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            Address = customer.Address,
            DateRegistered = customer.DateRegistered
        };
    }

    public async Task<CustomerViewDto> CreateCustomerAsync(CustomerInputDto customerInputModel)
    {
        var customer = new Customer
        {
            Name = customerInputModel.Name,
            Email = customerInputModel.Email,
            PhoneNumber = customerInputModel.PhoneNumber,
            Address = customerInputModel.Address,
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return new CustomerViewDto
        {
            CustomerId = customer.CustomerId,
            Name = customer.Name,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            Address = customer.Address,
            DateRegistered = customer.DateRegistered
        };
    }

    public async Task<CustomerViewDto> UpdateCustomerAsync(int id, CustomerUpdateDto customerUpdateModel)
    {
        var existingCustomer = await _context.Customers.FindAsync(id);
        if (existingCustomer == null) return null;

        existingCustomer.Name = customerUpdateModel.Name;
        existingCustomer.Email = customerUpdateModel.Email;
        existingCustomer.PhoneNumber = customerUpdateModel.PhoneNumber;
        existingCustomer.Address = customerUpdateModel.Address;

        await _context.SaveChangesAsync();

        return new CustomerViewDto
        {
            CustomerId = existingCustomer.CustomerId,
            Name = existingCustomer.Name,
            Email = existingCustomer.Email,
            PhoneNumber = existingCustomer.PhoneNumber,
            Address = existingCustomer.Address,
            DateRegistered = existingCustomer.DateRegistered
        };
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return false;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return true;
    }
}
