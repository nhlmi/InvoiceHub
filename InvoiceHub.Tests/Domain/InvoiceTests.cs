using InvoiceHub.Domain.Entities;
using InvoiceHub.Domain.ValueObjects;
using Xunit;

namespace InvoiceHub.Tests.Domain;

public class InvoiceTests
{
    [Fact]
    public void Should_Calculate_Total_Correctly()
    {
        // Arrange
        var invoice = new Invoice(
            "INV-001",
            DateTime.UtcNow,
            DateTime.UtcNow.AddDays(7));
        
        var price = Money.Create(100, "MYR");
        
        invoice.AddItem("Laptop", price, 2);
        
        // Act
        var total = invoice.GetTotal();
        
        // Assert
        Assert.Equal(200, total.Amount);
        Assert.Equal("MYR", total.Currency);
    }
    
    [Fact]
    public void Should_Throw_When_DueDate_Is_Before_IssueDate()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => 
            new Invoice(
                "INV-002",
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(-1)));
    }
}