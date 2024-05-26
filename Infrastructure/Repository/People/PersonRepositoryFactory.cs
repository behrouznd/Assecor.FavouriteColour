using Contracts.People;
using Microsoft.Extensions.Options;
using Repository.Configuration;
using Repository.Context;

namespace Repository.People;

public sealed class PersonRepositoryFactory : IPersonRepositoryFactory
{
    private readonly IPersonRepository _personRepository;

    public PersonRepositoryFactory(IOptions<ResourceOptions> options, DataContext dataContext)
    {
        var resourceOptions = options.Value;

        if (resourceOptions.IsDataBaseEnable)
        {
            _personRepository = new PersonRepositoryDB(dataContext);
        }
        else
        {
            _personRepository = new PersonRepositoryCSVFile(resourceOptions.Path);
        }
    }

    public IPersonRepository GetPersonRepository() => _personRepository;
}
