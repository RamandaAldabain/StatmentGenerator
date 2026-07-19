using StatementGeneratorService.Domain.Entites;

namespace StatementGeneratorService.Application.IRepositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account?> GetByAccountNumberAsync(string accountNumber);

        Task<List<Account>> GetByCustomerIdAsync(int customerId);
    }
}
