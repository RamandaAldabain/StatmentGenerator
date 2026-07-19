using StatementGeneratorService.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatementGeneratorService.Application.IRepositories
{
    public interface ICustomerRepository  : IRepository<Customer>
    {
        Task<Customer?> GetByIdWithAccountAsync(int customerId);

    }
}
