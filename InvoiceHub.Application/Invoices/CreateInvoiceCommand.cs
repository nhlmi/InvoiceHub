namespace InvoiceHub.Application.Invoices;

public class CreateInvoiceCommand
{
    public string InvoiceNumber { get; init; } = default!;
    public DateTime IssueDate { get; init; }
    public DateTime DueDate { get; init; }
    
    public List<CreateInvoiceItemDto> Items { get; init; } = new();
}

public class CreateInvoiceItemDto
{
    public string Description { get; init; } = default!;
    public decimal Amount { get; init; }
    public string Currency { get; init; } = default!;
    public int Quantity { get; init; }
}