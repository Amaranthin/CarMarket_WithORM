using CarMarket_WithORM.Data;
using CarMarket_WithORM.Models;

namespace CarMarket_WithORM.Services
{
    public class Service
    {
        public static void BuyNewCar(Car choosenCar, Person buyer)
        {
            CarMarketContext db = new CarMarketContext();

            var carDB = db.Cars
                .Where(c => c.ID == choosenCar.ID).FirstOrDefault();

            var buyerDB = db.Users
                .Where(u => u.ID == buyer.ID).FirstOrDefault();

            buyerDB.Money -= choosenCar.Price;
            carDB.OwnerID = buyer.ID;
            db.SaveChanges();
        }

        public static void SellCar(Car choosenCar, Person buyer)
        {
            CarMarketContext db = new CarMarketContext();

            var carDB = db.Cars
                .Where(c => c.ID == choosenCar.ID).FirstOrDefault();

            var buyerDB = db.Users
                .Where(u => u.ID == buyer.ID).FirstOrDefault();

            buyerDB.Money += choosenCar.Price;
            carDB.OwnerID = null;
            db.SaveChanges();
        }
    }
}
