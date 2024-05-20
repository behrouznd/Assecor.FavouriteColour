using Shared.DataTransferObject.People;

namespace Service.Contract.People;

public interface IPersonService
{
    IReadOnlyCollection<PersonDto> GetPeople();
    PersonDto GetPersonById(int id);
    IReadOnlyCollection<PersonDto> GetPeopleByColor(int id);
    PersonDto AddPerson(PersonDto person);
}
