using InvoiceHub.Domain.Entities;
using InvoiceHub.Domain.ValueObjects;
using Xunit;

namespace InvoiceHub.Tests.Domain;

public class InvoiceItemTests
{
    [Fact]
    public void Should_Throw_When_Quantity_Is_Invalid()
    {
        var price = Money.Create(100, "MYR");
        
        Assert.Throws<ArgumentException>(() => 
            new InvoiceItem("Laptop", price, 0));
    }
}