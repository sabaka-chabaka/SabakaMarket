namespace SabakaMarket.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string BarCode { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}