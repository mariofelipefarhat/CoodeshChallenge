using ApiSketch.Application.Commands;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Coodesh.API.Controllers;

[ApiController]
[Route("transactions")]
public class TransactionController : ControllerBase
{
    private readonly ISender _sender;

    public TransactionController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("upload")]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Create([Required] IFormFile file)
    {
        CreateTransactionCommand command = new(file);
        ErrorOr<bool> result = await _sender.Send(command);

        if (result.IsError)
            return BadRequest(new { result.Errors });

        return Ok();
    }
}