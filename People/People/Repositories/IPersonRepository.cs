using System.Collections.Generic;
using People.ModelsDB;

namespace People.Repositories
{
    public enum ExitCode
    {
        Success,
        Constraint,
        Error
    }
    
    public interface IPersonRepository
    {
        List<Person> FindAll();
        Person FindById(int id);
        Person Add(Person obj);
        ExitCode Update(Person obj);
        ExitCode DeleteById(int id);
    }
}
