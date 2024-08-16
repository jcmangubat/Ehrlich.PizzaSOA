using Ehrlich.PizzaSOA.Domain.Entities;
using SMEAppHouse.Core.Patterns.Repo.Repository.Abstractions;

namespace Ehrlich.PizzaSOA.Domain.Interfaces.Repositories;

public interface IPizzaRepository : IRepositoryForKeyedEntity<Pizza, Guid>
{
}
