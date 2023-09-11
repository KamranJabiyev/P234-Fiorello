using Fiorello.Application.DTOs.CategoryDtos;
using Fiorello.Domain.Entities;

namespace Fiorello.Application.Abstraction.Services;

public interface ICategoryService
{
    List<Category> GetAll();
    Task<Category?> FindAsync(int id);
    Task AddAsync(CategoryCreateDto categoryDto);
}
