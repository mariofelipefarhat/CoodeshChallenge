using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Coodesh.Application.Commands;

public record CreateTransactionCommand(IFormFile Stream) : IRequest<ErrorOr<bool>>;
