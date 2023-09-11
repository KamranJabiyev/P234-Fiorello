using Fiorello.Application.DTOs.CategoryDtos;
using Fiorello.Domain.Entities;

namespace Fiorello.Application.Abstraction.Services;

public interface ICategoryService
{
    Task<List<CategoryGetDto>> GetAllAsync();
    Task<CategoryGetDto?> FindAsync(Guid id);
    Task AddAsync(CategoryCreateDto categoryDto);
}
