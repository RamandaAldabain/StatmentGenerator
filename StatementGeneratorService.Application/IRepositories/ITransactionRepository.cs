using StatementGeneratorService.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatementGeneratorService.Application.IRepositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<List<Transaction>> GetTransactionsByAccountAndMonthAsync(int accountId,int month,int year);
    }
}
