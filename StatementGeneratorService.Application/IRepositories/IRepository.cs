using StatementGeneratorService.Domain.Common;

namespace StatementGeneratorService.Application.IRepositories
{
    public interface IRepository<T>  where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);

        Task<List<T>> GetAllAsync();

        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task<bool> ExistsAsync(int id);

        Task SaveChangesAsync();
    }
}
