using InvoiceHub.Domain.Common;
using InvoiceHub.Domain.ValueObjects;

namespace InvoiceHub.Domain.Entities;

public class InvoiceItem : BaseEntity
{
    public string Description { get; private set; }
    public Money Price { get; private set; }
    public int Quantity { get; private set; }
    
    private InvoiceItem() { }
    
    public InvoiceItem(string description, Money price, int quantity)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is required.");
        
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");
        
        Description = description;
        Price = price;
        Quantity = quantity;
    }
}

