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
        
        /// <summary>Get all Persons</summary>
        /// <returns>People information</returns>
        /// <response code="200">All Persons</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PersonDTO>))]
        public IActionResult GetAllPeople()
        {
            List<Person> people = _personController.GetAllPeople();
            List<PersonDTO> lPersonDTO = ListPersonDTO(people);
            return Ok(lPersonDTO);
        }
        
        /// <summary>Get Person by ID</summary>
        /// <returns>Person information</returns>
        /// <response code="200">Person for ID</response>
        /// <response code="404">Not found Person for ID</response>
        [HttpGet]
        [Route("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetPersonById([FromRoute(Name = "Id")] int id)
        {
            var person = _personController.GetPersonById(id);
            if (person is null)
            {
                return NotFound();
            }

            var personDTO = new PersonDTO(person);
            return Ok(personDTO);
        }
        
        /// <summary>Create new Person</summary>
        /// <param name="personDTO">Person to add</param>
        /// <returns>Added person</returns>
        /// <response code="201">Created new Person</response>
        /// <response code="400">Invalid data</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddPerson([FromBody] PersonDTO personDTO)
        {
            var person = personDTO.GetPerson();
            var result = _personController.AddPerson(person);

            if (result is null)
            {
                return BadRequest();
            }

            var header = $"api/v1/persons/{result.Id}";
            return Created(header, person);
        }

        void FixedPatchFields(Person personToPatch, Person userPerson)
        {
            if (userPerson.Name != null && userPerson.Name != "string")
            {
                personToPatch.Name = userPerson.Name;
            }
            
            if (userPerson.Address != null && userPerson.Address != "string")
            {
                personToPatch.Address = userPerson.Address;
            }
            
            if (userPerson.Work != null && userPerson.Work != "string")
            {
                personToPatch.Work = userPerson.Work;
            }

            if (userPerson.Age > 0)
            {
                personToPatch.Age = userPerson.Age;
            }
        }

        /// <summary>Update Person by ID</summary>
        /// <param name="personDTO">Person to update</param>
        /// <returns>Updated person</returns>
        /// <response code="200">Person for ID was updated</response>
        /// <response code="400">Invalid data</response>
        /// <response code="404">Not found Person for ID</response>
        [HttpPatch]
        [Route("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PersonDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PatchPerson([FromRoute(Name = "Id")] int id, [FromBody] PersonDTO personDTO)
        {
            var personToPatch = _personController.GetPersonById(id);
            if (personToPatch is null)
            {
                return NotFound();
            }
            
            var userPerson = personDTO.GetPerson(id);
            FixedPatchFields(personToPatch, userPerson);
            
            /*Console.WriteLine("Id = " + personToPatch.Id);
            Console.WriteLine("Name = " + personToPatch.Name);
            Console.WriteLine("Age = " + personToPatch.Age);
            Console.WriteLine("Address = " + personToPatch.Address);
            Console.WriteLine("Work = " + personToPatch.Work);
            */

            ExitCode result = _personController.PatchPerson(personToPatch);
            if (result == ExitCode.Constraint) 
            {
                return BadRequest();
            }
            
            var updatedPerson = new PersonDTO(personToPatch);
            return Ok(updatedPerson);
        }

        /// <summary>Remove Person by ID</summary>
        /// <returns>Removed person</returns>
        /// <response code="204">Person removed</response>
        [HttpDelete]
        [Route("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(PersonDTO))]
        public IActionResult DeletePerson([FromRoute(Name = "Id")] int id)
        {
            _personController.DeletePersonById(id);
            return NoContent();
        }
    }
}
