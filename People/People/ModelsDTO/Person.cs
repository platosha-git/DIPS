using People.ModelsDB;

namespace People.ModelsDTO
{
    public class PersonDTO
    {
        private int Id { get; set; }
        private string Name { get; set; }
        private int Age { get; set; }
        private string Address { get; set; }
        private string Work { get; set; }

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
