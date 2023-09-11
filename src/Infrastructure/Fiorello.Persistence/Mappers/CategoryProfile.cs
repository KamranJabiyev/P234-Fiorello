using AutoMapper;
using Fiorello.Application.DTOs.CategoryDtos;
using Fiorello.Domain.Entities;

namespace Fiorello.Persistence.Mappers;

public class CategoryProfile:Profile
{
    public CategoryProfile()
    {
        CreateMap<Category,CategoryCreateDto>().ReverseMap();
        CreateMap<Category,CategoryGetDto>().ReverseMap();


    }
}
