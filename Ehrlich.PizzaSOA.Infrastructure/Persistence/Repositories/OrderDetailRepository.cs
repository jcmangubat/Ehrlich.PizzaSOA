using Ehrlich.PizzaSOA.Domain.Entities;
using Ehrlich.PizzaSOA.Domain.Interfaces.Repositories;
using SMEAppHouse.Core.Patterns.Repo.Repository.GuidPKBasedVariation;

namespace Ehrlich.PizzaSOA.Infrastructure.Persistence.Repositories;

public class OrderDetailRepository(ApplicationDbContext dbContext)
    : EntityRepositoryForKeyedEntity<OrderDetail>(dbContext), IOrderDetailRepository
{
}