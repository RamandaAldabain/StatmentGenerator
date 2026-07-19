using Microsoft.AspNetCore.Mvc;
using StatementGeneratorService.Application.Dtos;
using StatementGeneratorService.Application.Interfaces;
using StatementGeneratorService.Application.IRepositories;
using StatementGeneratorService.Domain.Entites;
using System.Linq;

namespace StatementGeneratorService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {

        private readonly IRepository<Account> _repository;
       


        public AccountsController(
            IRepository<Account> repository)
        {
            _repository = repository;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _repository.GetAllAsync();

            var dtos = accounts.Select(a => new AccountDto
            {
                Id = a.Id,
                CustomerId = a.CustomerId,
                AccountNumber = a.AccountNumber,
                AccountType = a.AccountType.ToString(),
                Balance = a.Balance,
                Status = a.Status.ToString()
            });

            return Ok(dtos);
        }




        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var account = await _repository.GetByIdAsync(id);

            if (account == null)
                return NotFound();

            var dto = new AccountDto
            {
                Id = account.Id,
                CustomerId = account.CustomerId,
                AccountNumber = account.AccountNumber,
                AccountType = account.AccountType.ToString(),
                Balance = account.Balance,
                Status = account.Status.ToString()
            };

            return Ok(dto);
        }


        [HttpPost]
        public async Task<IActionResult> Create(AccountDto account)
        {
            var entity = new Account
            {
                CustomerId = account.CustomerId,
                AccountNumber = account.AccountNumber,
                AccountType = Enum.TryParse<StatementGeneratorService.Domain.Enum.AccountType>(account.AccountType, out var at) ? at : default,
                Balance = account.Balance,
                Status = Enum.TryParse<StatementGeneratorService.Domain.Enum.AccountStatus>(account.Status, out var st) ? st : default
            };

            await _repository.AddAsync(entity);

            await _repository.SaveChangesAsync();

            account.Id = entity.Id;

            return Ok(account);
        }





        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var account =
                await _repository.GetByIdAsync(id);


            if (account == null)
                return NotFound();



            _repository.Delete(account);

            await _repository.SaveChangesAsync();


            return NoContent();
        }
    }
}
