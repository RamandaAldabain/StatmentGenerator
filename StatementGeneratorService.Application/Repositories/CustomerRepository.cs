using Microsoft.EntityFrameworkCore;
using StatementGeneratorService.Application.IRepositories;
using StatementGeneratorService.Domain.Entites;
using StatementGeneratorService.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatementGeneratorService.Application.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _context;


        public CustomerRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }
        public async Task<Customer?> GetByIdWithAccountAsync(int customerId)
        {
            return await _context.Customers.Include(x => x.Accounts).Where(x => x.Id == customerId).FirstOrDefaultAsync();
        }
    }
}
