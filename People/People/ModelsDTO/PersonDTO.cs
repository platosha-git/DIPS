using People.ModelsDB;

namespace People.ModelsDTO
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Work { get; set; }

        public PersonDTO() { }
        
        public PersonDTO(Person person)
        {
            Id = person.Id;
            Name = person.Name;
            Age = person.Age;
            Address = person.Address;
            Work = person.Work;
        }

        public Person GetPerson(int personId = 0)
        {
            var person = new Person()
            {
                Id = personId,
                Name = Name,
                Age = Age,
                Address = Address,
                Work = Work
            };

            return person;
        }
    }
}
