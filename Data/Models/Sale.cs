using System;

namespace Data.Models;


public class Sale
{
    public int SaleId { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalPrice { get; set; }

    public Customer Customer { get; set; } // Navigation property
    public Product Product { get; set; }   // Navigation property
}