namespace SabakaMarket.Domain.Entities;

public class CartSession
{
    public Guid Id { get; set; }
    public Seller Seller { get; set; } = null!;
    public ICollection<Product> Products { get; set; } = null!;
    public decimal TotalPrice { get; set; }
}