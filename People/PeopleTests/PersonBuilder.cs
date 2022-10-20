using System;
using People.ModelsDB;

namespace PeopleTests
{
    public class PersonBuilder
    {
        private int Id;
        private string Name;
        private int Age;
        private string Address;
        private string Work;

        public PersonBuilder()
        {
            Id = 0;
            Name = string.Empty;
            Age = 0;
            Address = string.Empty;
            Work = string.Empty;
        }

        public Person Build()
        {
            var person = new Person()
            {
                Id = Id,
                Name = Name,
                Age = Age,
                Address = Address,
                Work = Work
            };

            return person;
        }

        public PersonBuilder WherePersonId(int personId)
        {
            Id = personId;
            return this;
        }

        public PersonBuilder WhereAge(int age)
        {
            Age = age;
            return this;
        }
    }
}