using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain.Models.Sale;
using Infrastructure.Contracts;

namespace WebHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        // GET: api/sale
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sales = await _saleService.GetAllSalesAsync();
            return Ok(sales);
        }

        // GET: api/sale/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);
            if (sale == null) return NotFound();
            return Ok(sale);
        }

        // POST: api/sale
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaleInputDto saleInputModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdSale = await _saleService.CreateSaleAsync(saleInputModel);
            return CreatedAtAction(nameof(GetById), new { id = createdSale.SaleId }, createdSale);
        }

        // PUT: api/sale/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SaleUpdateDto saleUpdateModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedSale = await _saleService.UpdateSaleAsync(id, saleUpdateModel);
            if (updatedSale == null) return NotFound();
            return Ok(updatedSale);
        }

        // DELETE: api/sale/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _saleService.DeleteSaleAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
