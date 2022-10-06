using System;
using People.ModelsDB;

namespace PeopleTests
{
    public class PersonBuilder
    {
        private int Personid;
        private string Firstname;
        private string Lastname;
        private string Gender;
        private int Age;

        public PersonBuilder()
        {
            Personid = 0;
            Firstname = string.Empty;
            Lastname = string.Empty;
            Gender = string.Empty;
            Age = 0;
        }

        public Person Build()
        {
            var person = new Person()
            {
                Personid = Personid,
                Firstname = Firstname,
                Lastname = Lastname,
                Gender = Gender,
                Age = Age
            };

            return person;
        }

        public PersonBuilder WherePersonId(int personId)
        {
            Personid = personId;
            return this;
        }

        public PersonBuilder WhereAge(int age)
        {
            Age = age;
            return this;
        }
    }
}