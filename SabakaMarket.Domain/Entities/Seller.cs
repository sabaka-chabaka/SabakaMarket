namespace SabakaMarket.Domain.Entities;

public class Seller
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
}