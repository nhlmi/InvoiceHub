using InvoiceHub.Domain.Common;
using InvoiceHub.Domain.ValueObjects;

namespace InvoiceHub.Domain.Entities;

public class Invoice : BaseEntity
{
    private readonly List<InvoiceItem> _items = new();
    
    public string InvoiceNumber { get; private set; }
    public DateTime IssueDate { get; private set; }
    public DateTime DueDate { get; private set; }
    
    public IReadOnlyCollection<InvoiceItem> Items 
        => _items.AsReadOnly();
    
    private Invoice() { }

    public Invoice(string invoiceNumber, DateTime issueDate, DateTime dueDate)
    {
        if (string.IsNullOrWhiteSpace(invoiceNumber))
            throw new ArgumentException("Invoice number is required.");

        if (dueDate <= issueDate)
            throw new ArgumentException("Due date must be after issue date.");
        
        InvoiceNumber = invoiceNumber;
        IssueDate = issueDate;
        DueDate = dueDate;
    }

    public void AddItem(string description, Money price, int quantity)
    {
        var item = new InvoiceItem(description, price, quantity);
        _items.Add(item);
    }

    public Money GetTotal()
    {
        if(!_items.Any())
            return Money.Create(0, "MYR");
        
        var currency = _items.First().Price.Currency;
        var total = Money.Create(0, currency);

        foreach (var item in _items)
        {
            var lineTotal = Money.Create(item.Price.Amount * item.Quantity, currency);
            total = total.Add(lineTotal);
        }

        return total;
    }
}