using AutoMapper;
using Fiorello.Application.Validators.CategoryValidators;
using Fiorello.Persistence.Contexts;
using Fiorello.Persistence.Mappers;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Fluent Validation
builder.Services.AddValidatorsFromAssemblyContaining<CategoryCrateDtoValidator>();
builder.Services.AddAutoMapper(typeof(CategoryProfile).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
