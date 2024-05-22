using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObject.People;

namespace Presentation.API.Tests.Controllers;

public partial class PersonsController
{
    public class GetPersons : PersonsController
    {
        [Fact]
        public void ShouldReturnPersons()
        {
            //Arrange
            var expectedPersons = fixture.Data.People;
            fixture.Mocks.SetupGetPersons(expectedPersons);
            var sut = fixture.CreateSystemUnderTest();

            //Act
            var actionResult = sut.GetPersons();

            //Assert
            var result = actionResult.Should()
                .NotBeNull()
                .And.BeOfType<OkObjectResult>()
                .Subject
                ;

            var actualPersonsResult = Assert.IsAssignableFrom<IReadOnlyCollection<PersonDto>>(result.Value);

            actualPersonsResult.Should()
                .NotBeNull()
                .And.NotBeEmpty()
                .And.HaveCount(expectedPersons.Count)
                .And.BeEquivalentTo(expectedPersons)
                ;
        }
    }

    public class GetPerson : PersonsController
    {
        [Fact]
        public void ShouldReturnPerson()
        {
            //Arrange
            var personId = 1;
            var expectedPerson = fixture.Data.Person;
            fixture.Mocks.SetupGetPersonById(expectedPerson);
            var sut = fixture.CreateSystemUnderTest();

            //Act
            var actionResult = sut.GetPerson(personId);

            //Assert
            var result = actionResult.Should()
                .NotBeNull()
                .And.BeOfType<OkObjectResult>()
                .Subject
                ;

            var actualPersonResult = Assert.IsAssignableFrom<PersonDto>(result.Value);

            actualPersonResult.Should()
                .NotBeNull()
                .And.BeEquivalentTo(expectedPerson)
                ;
        }
    }

    public class GetPersonsByColor : PersonsController 
    {
        [Theory]
        [InlineData("rot")]
        [InlineData("blau")]
        public void ShouldReturnPerson_WhenDataWasFound(string color)
        {
            // Arrange
            var expectedResult = fixture.Data.People.Where(x => x.color == color).ToList();
            fixture.Mocks.SetupGetPersonsByColor(expectedResult);
            var sut = fixture.CreateSystemUnderTest();

            // Act
            var  actionResult = sut.GetPersonsByColor(color);

            // Assert
            var result = actionResult.Should()
                .NotBeNull()
                .And.BeOfType<OkObjectResult>()
                .Subject
                ;

            var actualPersonResult = Assert.IsAssignableFrom<IReadOnlyCollection<PersonDto>>(result.Value);
            actualPersonResult.Should()
                .NotBeNull()
                .And.HaveCount(expectedResult.Count)
                .And.BeEquivalentTo(expectedResult)
                .And.AllSatisfy(actualPerson =>
                {
                    actualPerson.color.Should().Be(color);
                })
                ;
        }

        [Fact]
        public void ShouldReturnPerson_WhenDataDoesNotExist()
        {
            // Arrange
            string color = "gelb";
            var expectedResult = fixture.Data.People.Where(x => x.color == color).ToList();
            fixture.Mocks.SetupGetPersonsByColor(expectedResult);
            var sut = fixture.CreateSystemUnderTest();

            // Act
            var actionResult = sut.GetPersonsByColor(color);

            // Assert
            var result = actionResult.Should()
                .NotBeNull()
                .And.BeOfType<OkObjectResult>()
                .Subject
                ;

            var actualPersonResult = Assert.IsAssignableFrom<IReadOnlyCollection<PersonDto>>(result.Value);

            actualPersonResult.Should()
                .NotBeNull()
                .And.BeEmpty()
                ;
        }


        [Fact]
        public void ShouldReturnBadRequest_WhenParamIsInvalid()
        {
            // Arrange
            var expectedResult = fixture.Data.People;
            fixture.Mocks.SetupGetPersonsByColor(expectedResult);
            var sut = fixture.CreateSystemUnderTest();

            // Act
            var actionResult = sut.GetPersonsByColor("abc");

            // Assert
            var result = actionResult.Should()
                .NotBeNull()
                .And.BeOfType<BadRequestResult>()
                ;
        }
    }

    public class AddPerson : PersonsController
    {
        [Fact]
        public  void ShouldReturnSavedObject_WhenRequestIsValid()
        {
            // Arrange
            var expectedResult = fixture.Data.Person;
            fixture.Mocks.SetupAddPerson(expectedResult);
            var sut = fixture.CreateSystemUnderTest();

            // Act
            var actionResult = sut.AddPerson(expectedResult);

            // Assert
            var result = actionResult.Should()
                .NotBeNull()
                .And.BeOfType<CreatedAtRouteResult>()
                .Subject
                ;

            var actualPersonResult = Assert.IsAssignableFrom<PersonDto>(result.Value);

            actualPersonResult.Should()
                .NotBeNull()
                .And.BeEquivalentTo(expectedResult)
                ;
        }
    }
}
