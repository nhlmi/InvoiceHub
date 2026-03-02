using InvoiceHub.Application.Common;
using InvoiceHub.Application.Interfaces;
using InvoiceHub.Domain.Entities;
using InvoiceHub.Domain.ValueObjects;

namespace InvoiceHub.Application.Invoices;

public class CreateInvoiceHandler
{
    private readonly IInvoiceRepository _repository;
    
    public CreateInvoiceHandler(IInvoiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(CreateInvoiceCommand cmd)
    {
        try
        {
            var invoice = new Invoice(
                cmd.InvoiceNumber,
                cmd.IssueDate,
                cmd.DueDate);

            foreach (var itm in cmd.Items)
            {
                var money = Money.Create(itm.Amount, itm.Currency);
                invoice.AddItem(itm.Description, money, itm.Quantity);
            }

            await _repository.AddAsync(invoice);
            await _repository.SaveChangesAsync();

            return Result<Guid>.Success(invoice.Id);
        }
        catch (Exception ex)
        {
            return Result<Guid>.Failure(ex.Message);
        }
    }
}