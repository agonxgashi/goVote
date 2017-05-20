using System.Collections.Generic;

namespace GoVote.API.Models.AppModels
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        private static List<Person> _list = new List<Person>()
        {
            new Person() {
                Id = 1,
                Name = "Agon",
                Surname = "Gashi"
            },
            new Person() {
                Id = 2,
                Name = "Besart",
                Surname = "kuleta"
            }
        };


        public static IEnumerable<Person> GetAll()
        {
            return _list;
        }
    }
}