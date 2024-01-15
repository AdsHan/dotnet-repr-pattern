using ReprPattern.API.Data.Entities;

namespace ReprPattern.API.Data.Repositories;

public interface IProductRepository : IDisposable
{
    Task<IEnumerable<ProductModel>> GetAllAsync();
    Task SaveAsync();
    void Add(ProductModel obj);
}