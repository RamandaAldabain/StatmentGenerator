using StatementGeneratorService.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace StatementGeneratorService.Application.Interfaces
{
    public interface ITransactionService 
    {
       Task<List<TransactionDto>> GetMonthlyTransactionsAsync(int accountId,int month,int year);

       Task<TransactionDto> CreateTransactionAsync(TransactionDto dto);

       Task<TransactionDto> UpdateTransactionAsync(int id, TransactionDto dto);

       Task DeleteTransactionAsync(int id);
    }
}
