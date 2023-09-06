using Fiorello.Domain.Entitiesp;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Fiorello.Application.Abstraction.Repositories;

public interface IRepositoryBase<T> where T : BaseEntity, new()
{
    public DbSet<T> Table { get;}
    
}
