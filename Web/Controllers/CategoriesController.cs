using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Contracts;
using Domain.Models.Category;

namespace WebHost.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null) return NotFound();
        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryInputDto categoryInputModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var createdCategory = await _categoryService.CreateCategoryAsync(categoryInputModel);
        return CreatedAtAction(nameof(GetById), new { id = createdCategory.CategoryId }, createdCategory);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CategoryUpdateDto categoryUpdateModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var updatedCategory = await _categoryService.UpdateCategoryAsync(id, categoryUpdateModel);
        if (updatedCategory == null) return NotFound();
        return Ok(updatedCategory);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _categoryService.DeleteCategoryAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}