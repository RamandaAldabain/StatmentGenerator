using StatementGeneratorService.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatementGeneratorService.Application.Interfaces
{
    public interface IStatementService 
    {
        Task<StatementDto?> GetStatementAsync(int customerId, int month,int year);
        Task<StatementDto> GenerateStatementAsync(int customerId, int month,int year);
    }
}
