namespace InvoiceHub.Domain.ValueObjects;

public sealed class Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative.");
        
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency is required.");

        Amount = amount;
        Currency = currency.ToUpper();
    }

    public static Money Create(decimal amount, string currency) 
        => new(amount, currency);

    public Money Add(Money other)
    {
        if(Currency != other.Currency)
            throw new InvalidOperationException("Currencies must match.");
        
        return new Money(Amount + other.Amount, Currency);
    }
}