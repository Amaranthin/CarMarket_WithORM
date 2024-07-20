using Microsoft.EntityFrameworkCore;
using CarMarket_WithORM.Models;

namespace CarMarket_WithORM.Data
{
    public class CarMarketContext: DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Person> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost; database=car_market; user=root; password=",
              new MySqlServerVersion(new Version(8, 0, 21)));
        }
    }
}
