using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarMarket_WithORM.Models
{
    public class Car
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
        public int Price { get; set; }

        [Column("owner_id")]
        public int? OwnerID { get; set; }

        public Car()
        {

        }

        public Car(int id, string model, string color, int year, int price, int? owner_id)
        {
            this.ID = id;
            this.Price = price;
            this.Model = model;
            this.Color = color;
            this.Year = year;
            this.OwnerID = owner_id;  
        }
    }
}

