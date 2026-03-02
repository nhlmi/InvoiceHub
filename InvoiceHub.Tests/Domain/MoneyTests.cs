using InvoiceHub.Domain.ValueObjects;
using Xunit;

namespace InvoiceHub.Tests.Domain;

public class MoneyTests
{
    [Fact]
    public void Should_Throw_When_Adding_Different_Currencies()
    {
        var usd = Money.Create(100, "USD");
        var eur = Money.Create(100, "EUR");
        
        Assert.Throws<InvalidOperationException>(() 
            => usd.Add(eur));
    }
}