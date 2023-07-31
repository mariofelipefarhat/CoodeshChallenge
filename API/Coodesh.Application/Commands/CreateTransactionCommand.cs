using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ApiSketch.Application.Commands;

public record CreateTransactionCommand(IFormFile Stream) : IRequest<ErrorOr<bool>>;
