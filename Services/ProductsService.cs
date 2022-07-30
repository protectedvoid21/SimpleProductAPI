using System.Data;
using Dapper;
using WebApiTest.Dtos;
using WebApiTest.Models;

namespace WebApiTest.Services; 

public class ProductsService : IProductsService {
    private readonly StoreContext storeContext;

    public ProductsService(StoreContext storeContext) {
        this.storeContext = storeContext;
    }

    public async Task<IEnumerable<Product>> GetAll() {
        using var connection = storeContext.CreateConnection();

        var products = await connection.QueryAsync<Product>("SELECT * FROM Products");
        return products;
    }

    public async Task<Product> GetById(int id) {
        using var connection = storeContext.CreateConnection();

        var product = await connection.QueryAsync<Product>($"SELECT * FROM Products WHERE Id = {id}");
        return product.First();
    }

    public async Task AddAsync(ProductCreateDto product) {
        string query = "INSERT Products VALUES (@Name, @Price, @Count)";

        var parameters = GetParametersForProduct(product.Name, product.Price, product.Count);

        using var connection = storeContext.CreateConnection();
        await connection.ExecuteAsync(query, parameters);
    }

    public async Task UpdateAsync(int id, ProductUpdateDto product) {
        using var connection = storeContext.CreateConnection();
        string query = $"UPDATE Products SET Name = @Name, Price = @Price, Count = @Count WHERE Id = {id}";
        var parameters = GetParametersForProduct(product.Name, product.Price, product.Count);

        await connection.ExecuteAsync(query, parameters);
    }

    public async Task DeleteAsync(int id) {
        using var connection = storeContext.CreateConnection();
        string query = $"DELETE FROM Products WHERE Id = {id}";
        await connection.ExecuteAsync(query);
    }

    private DynamicParameters GetParametersForProduct(string name, decimal price, int count) {
        DynamicParameters parameters = new();
        parameters.Add("Name", name, DbType.String);
        parameters.Add("Price", price, DbType.Decimal);
        parameters.Add("Count", count, DbType.Int32);

        return parameters;
    }
}