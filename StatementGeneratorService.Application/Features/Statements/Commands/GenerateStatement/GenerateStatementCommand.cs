using MediatR;

namespace StatementGeneratorService.Application.Features.Statements.Commands
{
    public record GenerateStatementCommand(int CustomerId,int Month,int Year ) : IRequest<bool>;
}
