using Entities.Models;

namespace Contracts.People;

public interface IPersonRepository
{
    ICollection<Person> GetAll();
    Person GetById(int id);
    ICollection<Person> GetByColor(int color);
    Person Create(Person entity);
}
