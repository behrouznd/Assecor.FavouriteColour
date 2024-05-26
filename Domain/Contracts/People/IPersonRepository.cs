using Entities.Models;

namespace Contracts.People;

public interface IPersonRepository
{
    IReadOnlyCollection<Person> GetAll();
    Person? GetById(int id);
    IReadOnlyCollection<Person> GetByColor(int color);
    Person Create(Person entity);
}
