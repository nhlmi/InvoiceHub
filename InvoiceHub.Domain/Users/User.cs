namespace InvoiceHub.Domain.Users;

public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public string Role { get; private set; } = "User";

    private User() { }

    public User(string email, string passwordHash, string role = "User")
    {
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }

}