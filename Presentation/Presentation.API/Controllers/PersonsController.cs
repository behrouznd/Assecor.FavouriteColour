using Microsoft.AspNetCore.Mvc;
using Service.Contract.People;
using Shared.DataTransferObject.People;
using Shared.Enums;

namespace Presentation.API.Controllers;

[Route("persons")]
[ApiController]
public class PersonsController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonsController(IPersonService personService) =>
        this._personService = personService;

    /// <summary>
    /// Gets the list of all persons
    /// </summary>
    /// <returns>The persons list</returns>
    [HttpGet]
    public IActionResult GetPersons()
    {
        var persons = _personService.GetPeople();
        return Ok(persons);
    }

    /// <summary>
    /// Find person by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns code="200">The person</returns>
	/// <response code="404">If id does not exist</response>
    [HttpGet("{id}", Name = "PersonById")]
    public IActionResult GetPerson(int id) 
     { 
        var person = _personService.GetPersonById(id);
        if (person is null)
        {
            return NotFound(new { Message = $"Person with ID {id} not found." });
        }
        return Ok(person);
    }

    /// <summary>
    /// Search people by their favorite color
    /// </summary>
    /// <param name="color">valid values : blau, grün, violett, rot, gelb, türkis, weiß
    /// </param>
    /// <returns>The persons list</returns>
    /// <response code="200">Returns the people's list</response>
	/// <response code="400">If the param is invalid</response>
    [HttpGet("color/{color}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult GetPersonsByColor(string? color)
    {
        if (Enum.TryParse<Colour>(color, out Colour colour) && !Int32.TryParse(color, out int color_))
        {
            var person = _personService.GetPeopleByColor((int)colour);
            return Ok(person);
        }
        return BadRequest();
    }

    /// <summary>
    /// Add a new person
    /// </summary>
    /// <param name="personDto"></param>
    /// <returns>A newly created person</returns>
    /// <response code="201">Returns the newly created item</response>
	/// <response code="400">If the param is invalid</response>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public IActionResult CreatePerson([FromBody] PersonDto personDto)
    {
        if (!ModelState.IsValid)
    {
            return BadRequest(ModelState);
        }

        if (Enum.TryParse<Colour>(personDto.color, out Colour colour))
        {
            var person = _personService.AddPerson(personDto);
            return CreatedAtRoute("PersonById", new { id = person.id }, person);
        }
        return BadRequest();
    }
}
