using InvoiceHub.Application.Common;
using InvoiceHub.Application.Interfaces;

namespace InvoiceHub.Application.Invoices.Queries;

public class GetInvoiceByIdHandler
{
    private readonly IInvoiceRepository _repository;
    
    public GetInvoiceByIdHandler(IInvoiceRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<InvoiceDto>> Handle(GetInvoiceByIdQuery query)
    {
        var invoice = await _repository.GetByIdAsync(query.Id);
        if(invoice == null)
            return Result<InvoiceDto>.Failure("Invoice not found");

        var total = invoice.GetTotal();

        var dto = new InvoiceDto()
        {
            Id = invoice.Id,
            InvoiceNumber = invoice.InvoiceNumber,
            IssueDate = invoice.IssueDate,
            DueDate = invoice.DueDate,
            TotalAmount = total.Amount,
            Currency = total.Currency,
            Items = invoice.Items.Select(x => new InvoiceItemDto()
            {
                Description = x.Description,
                Amount = x.Price.Amount,
                Currency = x.Price.Currency,
                Quantity = x.Quantity
            }).ToList()
        };
        
        return Result<InvoiceDto>.Success(dto);
    }
}