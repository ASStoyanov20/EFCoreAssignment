using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Models.Product;
using Infrastructure.Contracts;

namespace WebHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/product
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // GET: api/product/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // POST: api/product
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductInputDto productInputModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdProduct = await _productService.CreateProductAsync(productInputModel);
            return CreatedAtAction(nameof(GetById), new { id = createdProduct.ProductId }, createdProduct);
        }

        // PUT: api/product/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDto productUpdateModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedProduct = await _productService.UpdateProductAsync(id, productUpdateModel);
            if (updatedProduct == null) return NotFound();
            return Ok(updatedProduct);
        }

        // DELETE: api/product/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _productService.DeleteProductAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
