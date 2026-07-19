using MediatR;
using StatementGeneratorService.Application.Interfaces;

namespace StatementGeneratorService.Application.Features.Statements.Commands.GenerateStatement
{

        public class GenerateStatementCommandHandler: IRequestHandler<GenerateStatementCommand, int>
        {

            private readonly IStatementService _statementService;


            public GenerateStatementCommandHandler(IStatementService statementService)
            {
                _statementService = statementService;
            }



            public async Task<int> Handle(GenerateStatementCommand request,CancellationToken cancellationToken)
            {

                var statement =await _statementService.GenerateStatementAsync(request.CustomerId,request.Month,request.Year);
                return statement.Id;
            }
        }
        
    }

