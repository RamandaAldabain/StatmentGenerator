using Microsoft.EntityFrameworkCore;
using StatementGeneratorService.Application.IRepositories;
using StatementGeneratorService.Domain.Entites;
using StatementGeneratorService.Infrastructure.Database;

namespace StatementGeneratorService.Application.Repositories
{
    public class StatementRepository : Repository<Statement>, IStatementRepository
    {
        private readonly ApplicationDbContext _context;


        public StatementRepository(
            ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }


        public async Task<Statement?> GetByCustomerAndMonthAsync(int customerId,int month,int year)
        {
            return await _context.Statements
                .Include(x => x.Transactions)
                 .Include(x => x.Account)
                .FirstOrDefaultAsync(x =>
                x.Account.CustomerId == customerId &&
                x.Month == month &&
                x.Year == year);
        }

        public async Task<Statement?> GetByAccountAndMonthAsync(int accountId,int month,int year)
        {
            return await _context.Statements
                .Include(x => x.Transactions)
                .Include(x => x.Account)
                .FirstOrDefaultAsync(x =>
                    x.AccountId == accountId &&
                    x.Month == month &&
                    x.Year == year);
        }
    }
}