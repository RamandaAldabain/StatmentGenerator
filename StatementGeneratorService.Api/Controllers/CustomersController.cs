using Microsoft.AspNetCore.Mvc;
using StatementGeneratorService.Application.Dtos;
using StatementGeneratorService.Application.IRepositories;
using StatementGeneratorService.Domain.Entites;

namespace StatementGeneratorService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {

        private readonly ICustomerRepository _repository;


        public CustomersController(
            ICustomerRepository repository)
        {
            _repository = repository;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _repository.GetAllAsync();

            var dtos = customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Address = c.Address
            });

            return Ok(dtos);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var customer = await _repository.GetByIdAsync(id);

            if (customer == null)
                return NotFound("Customer not found");

            var dto = new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address
            };

            return Ok(dto);
        }




        [HttpPost]
        public async Task<IActionResult> Create(CustomerDto customer)
        {

            var entity = new Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address
            };

            await _repository.AddAsync(entity);

            await _repository.SaveChangesAsync();

            customer.Id = entity.Id;

            return Ok(customer);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CustomerDto customer)
        {
            var existing = await _repository.GetByIdAsync(id);

            if (existing == null)
                return NotFound();

            existing.FirstName = customer.FirstName;
            existing.LastName = customer.LastName;
            existing.Email = customer.Email;
            existing.PhoneNumber = customer.PhoneNumber;
            existing.Address = customer.Address;

            _repository.Update(existing);

            await _repository.SaveChangesAsync();

            customer.Id = existing.Id;

            return Ok(customer);
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer =
                await _repository.GetByIdAsync(id);


            if (customer == null)
                return NotFound("Customer not found");



            _repository.Delete(customer);

            await _repository.SaveChangesAsync();


            return Ok("Customer deleted successfully");
        }
    }
}
