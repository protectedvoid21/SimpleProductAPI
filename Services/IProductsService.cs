using WebApiTest.Dtos;
using WebApiTest.Models;

namespace WebApiTest.Services; 

public interface IProductsService {
    Task<IEnumerable<Product>> GetAll();

    Task<Product> GetById(int id);

    Task AddAsync(ProductCreateDto product);

    Task UpdateAsync(int id, ProductUpdateDto product);

    Task DeleteAsync(int id);
}