using NSubstitute;
using Service.Contract.People;
using Shared.DataTransferObject.People;

namespace Presentation.API.Tests.Controllers;

public partial class PersonsController : IAsyncLifetime
{
    private TestFixture fixture { get; } = new();

    public Task DisposeAsync() => Task.CompletedTask;

    public Task InitializeAsync() => Task.CompletedTask;

    private class TestFixture
    {
        public readonly Mock Mocks = new();
        public readonly TestData Data = new();

        public class Mock
        {
            public readonly IPersonService personService = Substitute.For<IPersonService>();

            public void SetupGetPersons(IReadOnlyCollection<PersonDto> people)
            {
                personService
                    .GetPeople()
                    .Returns(people);
            }

            public void SetupGetPersonById(PersonDto person)
            {
                personService
                    .GetPersonById(Arg.Any<int>())
                    .Returns(person);
            }

            public void SetupGetPersonsByColor(IReadOnlyCollection<PersonDto> people)
            {
                personService
                    .GetPeopleByColor(Arg.Any<int>())
                    .Returns(people);
            }

            public void SetupAddPerson(PersonDto person)
            {
                personService
                    .AddPerson(person)
                    .Returns(person);
            }
        }

        public class TestData
        {
            public IReadOnlyCollection<PersonDto> People => new List<PersonDto>()
            {
                new PersonDto()
                {
                    id = 1,
                    name = "Pink",
                    lastname = "Panther",
                    zipcode = "12345",
                    city = "london",
                    color = "rot",
                },
                new PersonDto()
                {
                    id = 2,
                    name = "Mickey",
                    lastname = "Mouse",
                    zipcode = "54321",
                    city = "berlin",
                    color = "blau",
                },
                new PersonDto()
                {
                    id = 3,
                    name = "Donald",
                    lastname = "Duck",
                    zipcode = "456789",
                    city = "bonn",
                    color = "rot",
                }
            };

            public PersonDto Person =>
                new PersonDto()
                {
                    id = 1,
                    name = "Pink",
                    lastname = "Panther",
                    zipcode = "12345",
                    city = "london",
                    color = "rot",
                };
        }
    
        public API.Controllers.PersonsController CreateSystemUnderTest()
        {
            return new API.Controllers.PersonsController(Mocks.personService);
        }
    }
}
