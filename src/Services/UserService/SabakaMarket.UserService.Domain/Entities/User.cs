namespace SabakaMarket.UserService.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string Username { get; private set; } = string.Empty;
    public decimal Balance { get; private set; }
    public bool IsBlocked { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public User(Guid id, string email, string passwordHash, string username)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
        Username = username;
        Balance = 0m;
        IsBlocked = false;
        CreatedAt = DateTime.UtcNow;
    }
    
    private User() {}

    public void UpdateProfile(string newUsername)
    {
        if (string.IsNullOrEmpty(newUsername)) throw new ArgumentException("Username cannot be empty.", nameof(newUsername));
        if (newUsername.Length < 3) throw new ArgumentException("Username must be at least 3 characters long.", nameof(newUsername));
        if (newUsername.Length > 50) throw new ArgumentException("Username must be at most 50 characters long.", nameof(newUsername));
        if (newUsername == Username) throw new ArgumentException("Username cannot be the same as the current one.", nameof(newUsername));
        Username = newUsername;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Amount must be greater than zero.", nameof(amount));
        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Amount must be greater than zero.", nameof(amount));
        if (amount > Balance) throw new ArgumentException("Insufficient funds.", nameof(amount));
        Balance -= amount;   
    }
    
    public void Block()
    {
        IsBlocked = true;
    }
    
    public void Unblock()
    {
        IsBlocked = false;
    }
}