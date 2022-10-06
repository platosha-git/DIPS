using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using People.WebControllers;
using People.ModelsDB;
using People.ModelsDTO;
using People.Repositories;

namespace People.APIControllers
{
    [ApiController]
    [Route("/api/v1/persons")]
    public class ApiPersonController : ControllerBase
    {
        private readonly PersonController _personController;

        public ApiPersonController(PersonController personController)
        {
            _personController = personController;
        }
        
        private List<PersonDTO> ListPersonDTO(List<Person> people)
        {
            var peopleDTO = new List<PersonDTO>();
            foreach (var person in people)
            {
                var personDTO = new PersonDTO(person);
                peopleDTO.Add(personDTO);
            }

            return peopleDTO;
        }
        
        /// <summary>Get all people</summary>
        /// <returns>People information</returns>
        /// <response code="200">People found</response>
        /// <response code="204">No people</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Person>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllPeople()
        {
            List<Person> people = _personController.GetAllPeople();
            if (people.Count == 0)
            {
                return NoContent();
            }

            List<PersonDTO> lPersonDTO = ListPersonDTO(people);
            return Ok(lPersonDTO);
        }
        
        /// <summary>Person by ID</summary>
        /// <returns>Person information</returns>
        /// <response code="200">Person found</response>
        /// <response code="404">No person</response>
        [HttpGet]
        [Route("{personId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPersonById([FromRoute(Name = "personId")] int personID)
        {
            var person = _personController.GetPersonById(personID);
            if (person is null)
            {
                return NotFound();
            }

            var personDTO = new PersonDTO(person);
            return Ok(personDTO);
        }
        
        /// <summary>Adding person</summary>
        /// <param name="personDTO">Person to add</param>
        /// <returns>Added person</returns>
        /// <response code="201">Person added</response>
        /// <response code="409">Constraint error</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddPerson([FromBody] PersonDTO personDTO)
        {
            var person = personDTO.GetPerson();
            var result = _personController.AddPerson(person);

            if (result is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var header = $"api/v1/persons/{result.Personid}";
            return Created(header, person);
        }
        
        /// <summary>Updating person</summary>
        /// <param name="personDTO">Person to update</param>
        /// <returns>Updated person</returns>
        /// <response code="200">Person updated</response>
        /// <response code="409">Constraint error</response>
        /// <response code="500">Internal server error</response>
        [HttpPatch]
        [Route("{personId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdatePerson(
            [FromRoute(Name = "personId")] int personID, 
            [FromBody] PersonDTO personDTO)
        {
            Person person = personDTO.GetPerson(personID);
            ExitCode result = _personController.UpdatePerson(person);
            
            if (result == ExitCode.Constraint) 
            {
                return Conflict();
            }

            if (result == ExitCode.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var updatedPerson = new PersonDTO(person);
            return Ok(updatedPerson);
        }

        /// <summary>Removing person by ID</summary>
        /// <returns>Removed person</returns>
        /// <response code="200">Person removed</response>
        /// <response code="404">No person</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete]
        [Route("{personId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePerson([FromRoute(Name = "personId")] int personID)
        {
            var person = _personController.GetPersonById(personID);
            if (person == null)
            {
                return NotFound();
            }
            
            ExitCode result = _personController.DeletePersonById(personID);
            if (result == ExitCode.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
            var personDTO = new PersonDTO(person);
            return Ok(personDTO);
        }
    }
}
