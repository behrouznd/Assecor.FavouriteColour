using Contracts.People;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Context;

namespace Repository.People;

internal class PersonRepositoryDB : IPersonRepository
{
    protected DataContext _dataContext;
    public PersonRepositoryDB(DataContext dataContext) =>
        _dataContext = dataContext;

    public ICollection<Person> GetAll() =>
        _dataContext.People.AsNoTracking().ToList();
    
    public Person GetById(int id) =>
        _dataContext.People.First(x => x.Id == id);

    public ICollection<Person> GetByColor(int color) =>
        _dataContext.People.Where(x=> x.Color == color).ToList();

    public Person Create(Person entity)
    {
        _dataContext.People.Add(entity);
        _dataContext.SaveChanges();
        return entity;
    }
}
