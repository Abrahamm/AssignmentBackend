using Assignment.Application.Interfaces.Repositiories;
using Assignment.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Assignment.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IGenericRepository<Product> _repository;

        public ProductRepository(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<List<Product>> GetProductsByValueAsync(int value)
        {
            return await _repository.Entities.Where(x => x.Value == value).ToListAsync();
        }
    }
}
