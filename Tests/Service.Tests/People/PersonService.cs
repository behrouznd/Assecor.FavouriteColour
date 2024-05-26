using FluentAssertions;
using Shared.DataTransferObject.People;

namespace Service.Tests.People;

public partial class PersonService
{
    public class GetPeople : PersonService
    {
        [Fact]
        public void ShouldReturnPersons()
        {
            // Arrange
            var expectedPersons = fixture.Data.ExpectedPeople;
            var Persons = fixture.Data.People;
            fixture.Mocks.SetupGetPersonRepository();
            fixture.Mocks.SetupGetAll(Persons);
            var sut = fixture.CreateSystemUnderTest();

            // Act
            var actionResult = sut.GetPeople();

            // Assert
            actionResult.Should()
                .NotBeNull()
                .And.NotBeEmpty()
                .And.HaveCount(expectedPersons.Count)
                .And.BeEquivalentTo(expectedPersons)
                ;
        }
    }

    public class GetById : PersonService
    {
        [Fact]
        public void ShouldReturnPerson_WhenDataExists() 
        {
            // Arrange
            var personId = 1;
            var expectedPersons = fixture.Data.ExpectedPerson;
            var Person = fixture.Data.Person;
            fixture.Mocks.SetupGetPersonRepository();
            fixture.Mocks.SetupGetById(Person);
            var sut = fixture.CreateSystemUnderTest();

            // Act
            var actionResult = sut.GetPersonById(personId);

            // Assert
            actionResult.Should()
                .NotBeNull()
                .And.BeEquivalentTo(expectedPersons)
                ;
        }

        [Fact]
        public void ShouldReturnNull_WhenDataDoesNotExist()
        {
            // Arrange
            var personId = 5;
            fixture.Mocks.SetupGetPersonRepository();
            fixture.Mocks.SetupGetById(null);
            var sut = fixture.CreateSystemUnderTest();

            // Act
            var actionResult = sut.GetPersonById(personId);

            // Assert
            actionResult.Should()
                .BeNull()
                ;
        }
    }

    public class GetByColor : PersonService
    {
        [Theory]
        [InlineData(1, "blau")]
        [InlineData(4, "rot")]
        public void ShouldReturnPerson_WhenDataWasFound(int colorId, string color)
        {
            // Arrange
            var expectedResult = fixture.Data.ExpectedPeople.Where(x => x.color == color).ToList();
            var Persons = fixture.Data.People.Where(x => x.Color == colorId).ToList();
            fixture.Mocks.SetupGetPersonRepository();
            fixture.Mocks.SetupGetByColor(Persons);
            var sut = fixture.CreateSystemUnderTest();

            // Act
            var actionResult = sut.GetPeopleByColor(colorId);

            // Assert
            actionResult.Should()
                .NotBeNull()
                .And.NotBeEmpty()
                .And.HaveCount(expectedResult.Count)
                .And.BeEquivalentTo(expectedResult)
                ;
        }

        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        public void ShouldReturnEmpty_WhenDataDoesNotExist(int colorId)
        {
            // Arrange
            var expectedResult = fixture.Data.People.Where(x => x.Color == colorId).ToList();
            fixture.Mocks.SetupGetPersonRepository();
            fixture.Mocks.SetupGetByColor(expectedResult);

            var sut = fixture.CreateSystemUnderTest();

            // Act
            var actionResult = sut.GetPeopleByColor(colorId);

            // Assert
            actionResult.Should()
                .NotBeNull()
                .And.BeEmpty()
                .And.HaveCount(0)
                ;
        }
    }

    public class AddPerson : PersonService
    {
        [Fact]
        public void ShouldReturnSavedObject_WhenRequestIsValid()
        {
            // Arrange
            var expectedResult = fixture.Data.ExpectedPerson;
            var person = fixture.Data.Person;
           
            fixture.Mocks.SetupGetPersonRepository();
            fixture.Mocks.SetupAddPerson(person);
            var sut = fixture.CreateSystemUnderTest();

            // Act
            var actionResult = sut.AddPerson(expectedResult);

            // Assert
            actionResult.Should()
                .NotBeNull()
                .And.BeEquivalentTo(expectedResult)
                ;
        }
    }
}
