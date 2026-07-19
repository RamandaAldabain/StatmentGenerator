using Microsoft.EntityFrameworkCore;
using StatementGeneratorService.Application.IRepositories;
using StatementGeneratorService.Domain.Entites;
using StatementGeneratorService.Infrastructure.Database;

namespace StatementGeneratorService.Application.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Account?> GetByAccountNumberAsync(string accountNumber)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        }

        public async Task<List<Account>> GetByCustomerIdAsync(int customerId)
        {
            return await _context.Accounts.Where(a => a.CustomerId == customerId).ToListAsync();
        }
    }
}
