using AutoMapper;
using Contracts.People;
using Entities.Models;
using NSubstitute;
using Service.Contract.People;
using Service.People;
using Shared.DataTransferObject.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Tests.People;

public partial class PersonService : IAsyncLifetime
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
            public readonly IPersonRepositoryFactory personRepositoryFactory = Substitute.For<IPersonRepositoryFactory>();
            public readonly IPersonRepository personRepository = Substitute.For<IPersonRepository>();
            
            public IMapper mapper => new Mapper
            (
                new MapperConfiguration(config =>
                {
                    config.AddProfile<MappingProfile>();
                })
            );

            public void SetupGetPersonRepository()
            {

                personRepositoryFactory
                    .GetPersonRepository()
                    .Returns(personRepository);
            }

            public void SetupGetAll(IReadOnlyCollection<Person> people)
            {
                personRepository
                    .GetAll()
                    .Returns(people);
            }

            public void SetupGetById(Person? person)
            {
                personRepository
                    .GetById(Arg.Any<int>())
                    .Returns(person);
            }

            public void SetupGetByColor(IReadOnlyCollection<Person> people)
            {
                personRepository
                    .GetByColor(Arg.Any<int>())
                    .Returns(people);
            }

            public void SetupAddPerson(Person person)
            {
                personRepository
                    .Create(Arg.Any<Person>())
                    .Returns(person);
            }
        }

        public class TestData 
        {
            public IReadOnlyCollection<Person> People => new List<Person>()
            {
                new Person()
                {
                    Id = 1,
                    Name = "Pink",
                    LastName = "Panther",
                    Address = "12345 london",
                    Color = 4,
                },
                new Person()
                {
                    Id = 2,
                    Name = "Mickey",
                    LastName = "Mouse",
                    Address = "54321 berlin",
                    Color = 1,
                },
                new Person()
                {
                    Id = 3,
                    Name = "Donald",
                    LastName = "Duck",
                    Address = "456789 bonn",
                    Color = 4,
                }
            };

            public IReadOnlyCollection<PersonDto> ExpectedPeople => new List<PersonDto>()
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

            public Person Person =>
                new Person()
                {
                    Id = 1,
                    Name = "Pink",
                    LastName = "Panther",
                    Address = "12345 london",
                    Color = 1,
                };

            public PersonDto ExpectedPerson =>
                new PersonDto()
                {
                    id = 1,
                    name = "Pink",
                    lastname = "Panther",
                    zipcode = "12345",
                    city = "london",
                    color = "blau",
                };
        }

        public Service.People.PersonService CreateSystemUnderTest()
        {
            return new Service.People.PersonService(Mocks.personRepositoryFactory, Mocks.mapper);
        }
    }
}
