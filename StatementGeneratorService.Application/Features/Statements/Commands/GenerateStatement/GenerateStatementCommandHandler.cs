using MediatR;
using StatementGeneratorService.Application.Interfaces;

namespace StatementGeneratorService.Application.Features.Statements.Commands.GenerateStatement
{

        public class GenerateStatementCommandHandler: IRequestHandler<GenerateStatementCommand, bool>
        {

            private readonly IStatementService _statementService;


            public GenerateStatementCommandHandler(IStatementService statementService)
            {
                _statementService = statementService;
            }



            public async Task<bool> Handle(GenerateStatementCommand request,CancellationToken cancellationToken)
            {
                var result = await _statementService.GenerateStatementAsync(request.CustomerId, request.Month, request.Year);
                return result;
            }
        }
        
    }

