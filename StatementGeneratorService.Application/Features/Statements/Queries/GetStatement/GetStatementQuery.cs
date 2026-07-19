using MediatR;
using StatementGeneratorService.Application.Dtos;

namespace StatementGeneratorService.Application.Features.Statements.Queries.GetStatement
{
    public record GetStatementQuery(int CustomerId,int Month,int Year) : IRequest<StatementDto?>;
}
