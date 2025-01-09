namespace Domain.Models.Customer;


public class CustomerUpdateDto
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
}