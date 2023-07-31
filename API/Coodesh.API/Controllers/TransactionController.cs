using Coodesh.API.Contracts.Requests;
using Coodesh.Application.Commands;
using Coodesh.Application.Common.Responses;
using Coodesh.Application.Queries;
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

    [HttpGet("list")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<TransactionQueryResponse>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<List<TransactionQueryResponse>> List([FromQuery] TransactionsRequest request)
    {
        GetAllTransactionsQuery query = new(request.Page, request.PageSize, request.SortBy, request.SortDirection);
        List<TransactionQueryResponse> results = await _sender.Send(query);
        return results;
    }
}