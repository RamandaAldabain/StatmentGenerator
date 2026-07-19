using Microsoft.AspNetCore.Mvc;
using StatementGeneratorService.Application.Dtos;
using StatementGeneratorService.Application.IRepositories;
using StatementGeneratorService.Application.Interfaces;
using StatementGeneratorService.Domain.Entites;
using StatementGeneratorService.Domain.Enum;

namespace StatementGeneratorService.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {

        private readonly ITransactionRepository _repository;
        private readonly ITransactionService _transactionService;
        private readonly IAccountRepository _accountRepository;



        public TransactionsController(ITransactionRepository repository,ITransactionService transactionService,IAccountRepository accountRepository)
        {
            _repository = repository;
            _transactionService = transactionService;
            _accountRepository = accountRepository;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transactions =await _repository.GetAllAsync();


            return Ok(transactions);
        }




        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var transaction =await _repository.GetByIdAsync(id);


            if (transaction == null)
                return NotFound("Transaction not found.");


            return Ok(transaction);
        }





        [HttpPost]
        public async Task<IActionResult> Create(
            TransactionDto transaction)
        {
            var created = await _transactionService.CreateTransactionAsync(transaction);
            return Ok(created);
        }





        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TransactionDto dto)
        {
            var updated = await _transactionService.UpdateTransactionAsync(id, dto);
            return Ok(updated);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id)
        {

            await _transactionService.DeleteTransactionAsync(id);

            return Ok("Transaction deleted successfully.");
        }
    }
}