using InvoiceHub.Application.Interfaces;
using InvoiceHub.Application.Invoices;
using InvoiceHub.Application.Invoices.Queries;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly IInvoiceRepository _invoiceRepository;
    
    public InvoicesController(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateInvoiceCommand cmd)
    {
        var handler = new CreateInvoiceHandler(_invoiceRepository);
        var result = await handler.Handle(cmd);

        if (!result.IsSuccess)
            return BadRequest(result.Error);
        
        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var handler = new GetInvoiceByIdHandler(_invoiceRepository);
        var result = await handler.Handle(new GetInvoiceByIdQuery { Id = id });
        
        
        if(!result.IsSuccess)
            return NotFound(result.Error);
        
        return Ok(result.Value);
    }
}