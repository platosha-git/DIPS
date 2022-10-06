using People.ModelsDB;

namespace People.ModelsDTO
{
    public class PersonDTO
    {
        public int Personid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }

        public PersonDTO() { }
        
        public PersonDTO(Person person)
        {
            Personid = person.Personid;
            Firstname = person.Firstname;
            Lastname = person.Lastname;
            Gender = person.Gender;
            Age = person.Age;
        }

        public Person GetPerson(int personID = 0)
        {
            var person = new Person()
            {
                Personid = personID,
                Firstname = Firstname,
                Lastname = Lastname,
                Gender = Gender,
                Age = Age
            };

            return person;
        }
    }
}
