namespace SabakaMarket.Domain.Entities;

public class Receipt
{
    public Guid Id { get; set; }
    public Seller Seller { get; set; } = null!;
    public CartSession Session { get; set; } = null!;
    public DateTime Date { get; set; }
    public decimal TotalPrice { get; set; }
}