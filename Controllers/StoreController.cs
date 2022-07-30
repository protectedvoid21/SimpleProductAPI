using Microsoft.AspNetCore.Mvc;
using WebApiTest.Dtos;
using WebApiTest.Models;
using WebApiTest.Services;

namespace WebApiTest.Controllers {
    [Route("api/store")]
    [ApiController]
    public class StoreController : ControllerBase {
        private readonly IProductsService productsService;

        public StoreController(IProductsService productsService) {
            this.productsService = productsService;
        }

        [HttpGet(Name = "GetAllProducts")]
        public async Task<IEnumerable<Product>> GetAllProducts() {
            var products = await productsService.GetAll();
            return products;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<Product> GetProduct(int id) {
            var product = await productsService.GetById(id);
            return product;
        }

        [HttpPost]
        public async Task Post([FromBody] ProductCreateDto product) {
            await productsService.AddAsync(product);
        }

        [HttpPut("{id:int}")]
        public async Task Put(int id, [FromBody] ProductUpdateDto product) {
            await productsService.UpdateAsync(id, product);
        }

        [HttpDelete("{id:int}")]
        public async Task Delete(int id) {
            await productsService.DeleteAsync(id);
        }
    }
}
