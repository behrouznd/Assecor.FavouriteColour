using Entities.Models;
using System.Linq.Expressions;

namespace Contracts.People;

public interface IPersonRepository
{
    ICollection<Person> GetAll();
    Person GetById(int id);
    ICollection<Person> GetByColor(int color);
    ICollection<Person> GetByCondition(Expression<Func<Person, bool>> condition);
    Person Create(Person entity);

}
