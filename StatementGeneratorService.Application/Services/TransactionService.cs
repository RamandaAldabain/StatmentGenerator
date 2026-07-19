using StatementGeneratorService.Application.Dtos;
using StatementGeneratorService.Application.Interfaces;
using StatementGeneratorService.Application.IRepositories;
using StatementGeneratorService.Domain.Entites;
using StatementGeneratorService.Domain.Enum;

namespace StatementGeneratorService.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;
        private readonly IRepository<Account> _accountRepository;

        public TransactionService(
            ITransactionRepository repository,
            IRepository<Account> accountRepository)
        {
            _repository = repository;
            _accountRepository = accountRepository;
        }

        public async Task<List<TransactionDto>> GetMonthlyTransactionsAsync(int accountId, int month, int year)
        {
            var transactions = await _repository.GetTransactionsByAccountAndMonthAsync(accountId,month,year);

            return transactions.Select(x => new TransactionDto
                {
                    Id = x.Id,
                    Date = x.TransactionDate,
                    Description = x.Description,
                    Amount = x.Amount,
                    Type = x.Type.ToString(),
                    ReferenceNumber = x.ReferenceNumber,
                    AccountId = x.AccountId
                })
                .ToList();
        }

        public async Task<TransactionDto> CreateTransactionAsync(TransactionDto dto)
        {
            var account = await _accountRepository.GetByIdAsync(dto.AccountId);
            if (account == null)
                throw new Exception("Account not found");

            var entity = new Transaction
            {
                AccountId = dto.AccountId,
                TransactionDate = dto.Date,
                Description = dto.Description,
                ReferenceNumber = dto.ReferenceNumber,
                Amount = dto.Amount,
                Type = Enum.TryParse<TransactionType>(dto.Type, out var tt) ? tt : default
            };

            if (entity.Type == TransactionType.Deposit)
                account.Balance += entity.Amount;
            else if (entity.Type == TransactionType.Withdrawal)
                account.Balance -= entity.Amount;

            await _repository.AddAsync(entity);
            _accountRepository.Update(account);

            await _repository.SaveChangesAsync();
            await _accountRepository.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<TransactionDto> UpdateTransactionAsync(int id, TransactionDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new Exception("Transaction not found");

            var oldAccount = await _accountRepository.GetByIdAsync(existing.AccountId);

            if (oldAccount != null)
            {
                if (existing.Type == TransactionType.Deposit)
                    oldAccount.Balance -= existing.Amount;
                else if (existing.Type == TransactionType.Withdrawal)
                    oldAccount.Balance += existing.Amount;
            }

            var newType = Enum.TryParse<TransactionType>(dto.Type, out var nt) ? nt : default;
            var newAccountId = dto.AccountId;

            Account? newAccount = null;
            if (newAccountId != existing.AccountId)
            {
                newAccount = await _accountRepository.GetByIdAsync(newAccountId);
                if (newAccount == null)
                    throw new Exception("New account not found");
            }
            else
            {
                newAccount = oldAccount;
            }

            if (newAccount != null)
            {
                if (newType == TransactionType.Deposit)
                    newAccount.Balance += dto.Amount;
                else if (newType == TransactionType.Withdrawal)
                    newAccount.Balance -= dto.Amount;
            }

            existing.AccountId = newAccountId;
            existing.TransactionDate = dto.Date;
            existing.Description = dto.Description;
            existing.ReferenceNumber = dto.ReferenceNumber;
            existing.Amount = dto.Amount;
            existing.Type = newType;

            _repository.Update(existing);
            if (oldAccount != null)
                _accountRepository.Update(oldAccount);
            if (newAccount != null && newAccount != oldAccount)
                _accountRepository.Update(newAccount);

            await _repository.SaveChangesAsync();
            await _accountRepository.SaveChangesAsync();

            dto.Id = existing.Id;
            return dto;
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await _repository.GetByIdAsync(id);
            if (transaction == null)
                throw new Exception("Transaction not found");

            var account = await _accountRepository.GetByIdAsync(transaction.AccountId);
            if (account != null)
            {
                if (transaction.Type == TransactionType.Deposit)
                    account.Balance -= transaction.Amount;
                else if (transaction.Type == TransactionType.Withdrawal)
                    account.Balance += transaction.Amount;

                _accountRepository.Update(account);
            }

            _repository.Delete(transaction);

            await _repository.SaveChangesAsync();
            await _accountRepository.SaveChangesAsync();
        }
    }
}
