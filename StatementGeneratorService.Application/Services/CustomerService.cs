using StatementGeneratorService.Application.Dtos;
using StatementGeneratorService.Application.Interfaces;
using StatementGeneratorService.Application.IRepositories;
using StatementGeneratorService.Domain.Entites;

namespace StatementGeneratorService.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;


        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }



        public async Task<CustomerDto?> GetCustomerAsync(int id)
        {
            var customer =await _repository.GetByIdAsync(id);


            if (customer == null)
                return null;


            return new CustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address,

            };
        }



        public async Task<List<CustomerDto>> GetCustomersAsync()
        {
            var customers =await _repository.GetAllAsync();


            return customers
                .Select(x => new CustomerDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Address = x.Address

                })
                .ToList();
        }
    }
}
