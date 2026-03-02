using InvoiceHub.Domain.Users;

namespace InvoiceHub.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
}