using InvoiceHub.Domain.Entities;

namespace InvoiceHub.Application.Interfaces;

public interface IInvoiceRepository
{
    Task AddAsync(Invoice invoice);
    Task<Invoice?> GetByIdAsync(Guid id);
    Task SaveChangesAsync();
}