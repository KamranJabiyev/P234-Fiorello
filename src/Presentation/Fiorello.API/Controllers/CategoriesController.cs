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
}
