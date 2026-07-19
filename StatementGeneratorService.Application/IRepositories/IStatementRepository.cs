using StatementGeneratorService.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatementGeneratorService.Application.IRepositories
{
    public interface IStatementRepository : IRepository<Statement>
    {
        Task<Statement?> GetByCustomerAndMonthAsync(int customerId,int month,int year);
        Task<Statement?> GetByAccountAndMonthAsync(int accountId,int month,int year);
    }
}
