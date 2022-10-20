using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using People.ModelsDB;

namespace People.Repositories
{
    public class PersonRepository : IPersonRepository, IDisposable
    {
        private readonly PersonContext _db;
        private readonly ILogger<PersonRepository> _logger;

        public PersonRepository(PersonContext createDB, ILogger<PersonRepository> logDB)
        {
            _db = createDB;
            _logger = logDB;
        }

        public List<Person> FindAll()
        {
            var people = _db.People.ToList();
            return people;
        }

        public Person FindById(int id)
        {
            var person = _db.People.Find(id);
            return person;
        }

        public Person Add(Person obj)
        {
            try
            {
                var id = _db.People.Count() + 1;
                
                obj.Id = id;
                _db.People.Add(obj);
                _db.SaveChanges();
                
                _logger.LogInformation("+PersonRep : Person {Number} was added to People", obj.Id);
                return obj;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+PersonRep : Error trying to add person to People");
                throw;
            }
        }

        public ExitCode Patch(Person obj)
        {
            try
            {
                _db.People.Update(obj);
                _db.SaveChanges();
                _logger.LogInformation("+PersonRep : Person {Number} was updated at People", obj.Id);
                return ExitCode.Success;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+PersonRep : Error trying to update person to People");
                return ExitCode.Error;
            }
        }

        public ExitCode DeleteById(int id)
        {
            try
            {
                var food = FindById(id);
                _db.People.Remove(food);
                _db.SaveChanges();
                _logger.LogInformation("+PersonRep : Person {Number} was deleted from People", id);
                return ExitCode.Success;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+PersonRep : Error trying to delete person {Number} from People", id);
                return ExitCode.Error;
            }
        }
        
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
