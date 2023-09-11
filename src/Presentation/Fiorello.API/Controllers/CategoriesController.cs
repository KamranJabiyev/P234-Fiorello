using Fiorello.Application.Abstraction.Services;
using Fiorello.Application.DTOs.CategoryDtos;
using Fiorello.Persistence.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Fiorello.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CategoryCreateDto createDto)
    {
        try
        {
            await _categoryService.AddAsync(createDto);
            return StatusCode((int)HttpStatusCode.Created);
        }
        catch (DuplicatedException ex)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, new
            {
                message= ex.Message,
            });
        }
        catch (Exception ex)
        {

            return StatusCode((int)HttpStatusCode.InternalServerError, new
            {
                message = ex.Message,
            });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            var category = await _categoryService.FindAsync(id);
            return Ok(category);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new
            {
                message = ex.Message,
            });
        }
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new
            {
                message = ex.Message,
            });
        }
    }
}
