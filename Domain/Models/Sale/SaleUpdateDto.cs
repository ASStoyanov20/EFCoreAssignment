namespace Domain.Models.Sale;

public class SaleUpdateDto
{
    public int SaleId { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime SaleDate { get; set; }
}