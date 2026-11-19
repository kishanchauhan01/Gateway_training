using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsManagement.Models;
using ProductsManagement.Services;

namespace ProductsManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        // static List<Product> products = new List<Product>
        //     {
        //         new Product {Id = 1, Name="Laptop", Description = "Good laptop", Price = 999.99m},
        //         new Product {Id = 2, Name="Smartphone", Price = 500.00m},
        //         new Product {Id = 3, Name="Tablet", Price = 700.55m}

        //     };

        private readonly IProductService service;
        public ProductsController(IProductService productService)
        {
            service = productService;
        }


        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(service.GetAllProducts());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetProductById([FromRoute] int id)
        {
            var response = service.GetProductById(id);

            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);

        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateProduct([FromRoute] int id, [FromBody] Product product)
        {
            service.UpdateProduct(id, product);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            try
            {
                service.DeleteProduct(id);
                return NoContent();
            }
            catch (Exception)
            {

                return NoContent();
            }
        }
    }
}
