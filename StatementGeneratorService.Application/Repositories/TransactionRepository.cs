using Microsoft.EntityFrameworkCore;
using StatementGeneratorService.Application.IRepositories;
using StatementGeneratorService.Domain.Entites;
using StatementGeneratorService.Infrastructure.Database;

namespace StatementGeneratorService.Application.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(  ApplicationDbContext context)   : base(context)
        {
            _context = context;
        }
        public async Task<List<Transaction>> GetTransactionsByAccountAndMonthAsync(int accountId,int month,int year)
        {
            return await _context.Transactions.Where(x =>
                    x.AccountId == accountId &&
                    x.TransactionDate.Month == month &&
                    x.TransactionDate.Year == year)
                      .OrderBy(x => x.TransactionDate)
                      .ToListAsync();
        }
    }
}
