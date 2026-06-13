namespace SabakaMarket.CatalogService.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public Guid SellerId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }

    public Product(Guid id, Guid sellerId, string name, string description, decimal price, int quantity)
    {
        if (price < 0) 
            throw new ArgumentException("Price cannot be negative.", nameof(price));
            
        if (quantity < 0) 
            throw new ArgumentException("Asset count cannot be smaller than null.", nameof(quantity));

        Id = id;
        SellerId = sellerId;
        Name = name;
        Description = description;
        Price = price;
        Quantity = quantity;
        CreatedAt = DateTime.UtcNow;
        IsActive = quantity > 0;
    }

    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity < 0) throw new ArgumentException("Asset count cannot be smaller than null.");
        
        Quantity = newQuantity;
        IsActive = Quantity > 0;
    }

    public void DecreaseQuantity()
    {
        if (Quantity <= 0) throw new InvalidOperationException("Asset is out of stock!");
        
        Quantity--;
        IsActive = Quantity > 0;
    }
}