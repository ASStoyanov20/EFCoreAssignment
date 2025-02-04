﻿namespace Domain.Models.Product;

public class ProductUpdateDto
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
}