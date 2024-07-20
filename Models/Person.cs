using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarMarket_WithORM.Models
{
    public class Person
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }
        public int Money { get; set; }

        public Person() { }

        public Person(int id, string firstName, string lastName, int money)
        {
            this.ID = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Money = money;     
        }
    }
}

