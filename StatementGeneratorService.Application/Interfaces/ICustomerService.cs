using StatementGeneratorService.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatementGeneratorService.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetCustomerAsync(int id);

        Task<List<CustomerDto>> GetCustomersAsync();
    }
}
