using MediatR;
using StatementGeneratorService.Application.Dtos;
using StatementGeneratorService.Application.IRepositories;

namespace StatementGeneratorService.Application.Features.Statements.Queries.GetStatement
{
    public class GetStatementQueryHandler: IRequestHandler<GetStatementQuery, StatementDto?>
    {

        private readonly IStatementRepository _statementRepository;


        public GetStatementQueryHandler(IStatementRepository statementRepository)
        {
            _statementRepository = statementRepository;
        }



        public async Task<StatementDto?> Handle(GetStatementQuery request,CancellationToken cancellationToken)
        {

            var statement =await _statementRepository
                    .GetByCustomerAndMonthAsync(request.CustomerId,request.Month,request.Year);

               if (statement == null)
                return null;



            return new StatementDto
            {
                Id = statement.Id,
                CustomerId = statement.Account.CustomerId,

                Month = statement.Month,

                Year = statement.Year,

                OpeningBalance = statement.OpeningBalance,

                TotalCredits = statement.TotalCredits,

                TotalDebits = statement.TotalDebits,

                ClosingBalance = statement.ClosingBalance
            };
        }
    }
    }
