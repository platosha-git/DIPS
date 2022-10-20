using System.Collections.Generic;
using People.Repositories;
using People.ModelsDB;
using People.WebControllers;
using Xunit;
using Moq;

namespace PeopleTests
{
    public class UnitTests
    {
        [Fact]
        public void FindAll_NotNull()
        {
            // Arrange
            var expPerson = new PersonBuilder().Build();
            var expPeople = new List<Person>() {expPerson};

            var mock = new Mock<IPersonRepository>();
            mock.Setup(x => x.FindAll())
                .Returns(expPeople);
            var personController = new PersonController(mock.Object);

            // Act
            var actPeople = personController.GetAllPeople();
            
            // Assert
            Assert.NotNull(expPeople);
            Assert.Equal(expPeople, actPeople);
        }
        
        [Fact]
        public void FindById_FirstElement_NotNull()
        {
            const int personId = 1;
            
            // Arrange
            var expPerson = new PersonBuilder()
                .WherePersonId(personId)
                .Build();
            
            var mock = new Mock<IPersonRepository>();
            mock.Setup(x => x.FindById(personId))
                .Returns(expPerson);
            var personController = new PersonController(mock.Object);
            
            // Act
            var actPerson = personController.GetPersonById(personId);
            
            // Assert
            Assert.NotNull(expPerson);
            Assert.Equal(expPerson, actPerson);
        }

        [Fact]
        public void AddPerson_Ok()
        {
            // Arrange
            var accessObject = new PeopleAccessObject();
            var personToAdd = CreatePerson();
            AddEntity(accessObject, personToAdd);
            
            // Act
            accessObject.personRepository.Add(personToAdd);

            // Assert
            var addedPerson = accessObject.personRepository.FindById(personToAdd.Id);
            
            Assert.NotNull(addedPerson);
            Assert.Equal(personToAdd, addedPerson);

            Cleanup(accessObject);
        }
        
        [Fact]
        public void UpdatePerson_Ok()
        {
            // Arrange
            var accessObject = new PeopleAccessObject();
            var personToUpdate = CreatePerson();
            AddEntity(accessObject, personToUpdate);
            
            // Act
            personToUpdate.Age += 5;
            accessObject.personRepository.Patch(personToUpdate);

            // Assert
            var updatedPerson = accessObject.personRepository.FindById(personToUpdate.Id);
            Assert.NotNull(updatedPerson);
            Assert.Equal(personToUpdate, updatedPerson);
            
            Cleanup(accessObject);
        }
        
        [Fact]
        public void DeletePersonById_Ok()
        {
            // Arrange
            var accessObject = new PeopleAccessObject();
            var personToDelete = CreatePerson();
            AddEntity(accessObject, personToDelete);

            // Act
            var id = personToDelete.Id;
            accessObject.personRepository.DeleteById(id);

            // Assert
            var removedPerson = accessObject.personRepository.FindById(id);
            Assert.Null(removedPerson);
            
            Cleanup(accessObject);
        }

        Person CreatePerson()
        {
            var person = new Person()
            {
                Id = 1,
                Name = "Test",
                Age = 1,
                Address = "Address",
                Work = "Pass"
            };
            return person;
        }

        void AddEntity(PeopleAccessObject accessObject, Person person)
        {
            accessObject.peopleContext.ChangeTracker.Clear();
            accessObject.peopleContext.People.Add(person);
            accessObject.peopleContext.SaveChanges();
        }

        void Cleanup(PeopleAccessObject accessObject)
        {
            accessObject.peopleContext.ChangeTracker.Clear();
            accessObject.peopleContext.People.RemoveRange(accessObject.peopleContext.People);
            accessObject.peopleContext.SaveChanges();
        }
    }
}
