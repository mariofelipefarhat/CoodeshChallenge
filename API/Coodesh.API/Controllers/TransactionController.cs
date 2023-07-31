using Coodesh.Infrastructure;
using Coodesh.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Coodesh.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    private readonly TransactionContext _productContext;
    public TransactionController(TransactionContext ctx)
    {
        _productContext = ctx;
    }

    [HttpPost]
    public IActionResult Create(Transaction p)
    {
        _productContext.Add(p);
        _productContext.SaveChanges();
        return Ok();
    }
}