using Ehrlich.PizzaSOA.Domain.Entities;
using SMEAppHouse.Core.Patterns.Repo.Repository.Abstractions;

namespace Ehrlich.PizzaSOA.Domain.Interfaces.Repositories;

public interface IPizzaTypeRepository : IRepositoryForKeyedEntity<PizzaType, Guid>
{
}
