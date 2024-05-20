using AutoMapper;
using Contracts.People;
using Entities.Models;
using Service.Contract.People;
using Shared.DataTransferObject.People;

namespace Service.People;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public PersonService(
        IPersonRepositoryFactory personRepositoryFactory,
        IMapper mapper)
    {
        _personRepository = personRepositoryFactory.GetPersonRepository();
        _mapper = mapper;
    }

    public PersonDto AddPerson(PersonDto person)
    {
        var personEntity= _personRepository.Create(_mapper.Map<Person>(person));
        return _mapper.Map<PersonDto>(personEntity);
    }

    public IReadOnlyCollection<PersonDto> GetPeople()
    {
        var persons = _personRepository.GetAll();
        return _mapper.Map<IReadOnlyCollection<PersonDto>>(persons);
    }

    public IReadOnlyCollection<PersonDto> GetPeopleByColor(int id)
    {
        var persons = _personRepository.GetByColor(id);
        return _mapper.Map<IReadOnlyCollection<PersonDto>>(persons);
    }

    public PersonDto GetPersonById(int id)
    {
        var persons = _personRepository.GetById(id);
        return _mapper.Map<PersonDto>(persons);
    }
}
