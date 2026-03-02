namespace InvoiceHub.Application.Invoices.Queries;

public class InvoiceDto
{
    public Guid Id { get; init; }
    public string InvoiceNumber { get; init; } = default!;
    public DateTime IssueDate { get; init; }
    public DateTime DueDate { get; init; }
    public decimal TotalAmount { get; init; }
    public string Currency { get; init; } = default!;
    public List<InvoiceItemDto> Items { get; init; } = new();
}

public class InvoiceItemDto
{
    public string Description { get; init; } = default!;
    public decimal Amount { get; init; }
    public string Currency { get; init; } = default!;
    public int Quantity { get; init; }
}