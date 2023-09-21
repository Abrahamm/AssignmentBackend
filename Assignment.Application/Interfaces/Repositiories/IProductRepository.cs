using Assignment.Domain.Entities;

namespace Assignment.Application.Interfaces.Repositiories;

public interface IProductRepository
{
    Task<List<Product>> GetProductsByValueAsync(int value);
}
