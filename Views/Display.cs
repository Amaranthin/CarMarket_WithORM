using CarMarket_WithORM.Data;
using CarMarket_WithORM.Models;
using CarMarket_WithORM.Services;

namespace CarMarket_WithORM.View
{
    internal class Display
    {
        List<Car> _cars = null; //Кеширани списъци, които да използваме
        List<Person> _users = null; //докато няма промени в данните

        public Display()
        {
            RefreshLocalLists(); 
        }

        private void RefreshLocalLists()
        {   //Ъпдейт на инфото в кешираните списъци от реалната база
            CarMarketContext db = new CarMarketContext();
            _cars = db.Cars.ToList();
            _users = db.Users.ToList();
        }

        public void ShowMainMenu()
        {
            RefreshLocalLists();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(); //Да не се залепя за последните операции
            Console.WriteLine("1) Покажи списък на хората и колекциите им");
            Console.WriteLine("2) Покажи списък на колите");
            Console.WriteLine("3) Закупуване на нова кола");
            Console.WriteLine("4) Продажба на кола");
            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.Write("Изберете вашето действие: ");
            Console.ForegroundColor = ConsoleColor.White;
            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine();

            switch (choice)
            {
                case 1: { ShowPeopleCarCollection(); break; }
                case 2: { ShowCarsInList(_cars); break; }
                case 3: { BuyNewCar(); break; }
                case 4: { SellCar(); break; }
            }

            ShowMainMenu(); //Самоизвикване на менюто вместо while(true){}
        }

        private void BuyNewCar()
        {
            //Без верификация на изборите, за да не усложняваме излишно
            Console.WriteLine("Изберете кой ще бъде купувача:");
            ShowPeople();
            Console.ForegroundColor = ConsoleColor.White;
            int buyerNum = int.Parse(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Изберете коя кола иска да закупи:");
            ShowCarsInList(_cars);

            Console.ForegroundColor = ConsoleColor.White;
            int chosenCarNum = int.Parse(Console.ReadLine());

            Person buyer = _users[buyerNum - 1];
            Car chosenCar = _cars[chosenCarNum - 1];

            Console.ForegroundColor = ConsoleColor.Magenta;
            if (chosenCar.OwnerID is null)
            {
                if (buyer.Money >= chosenCar.Price)
                {
                    Service.BuyNewCar(chosenCar, buyer);

                    //ъпдейт на локалните списъци след покупката
                    RefreshLocalLists();
                    Console.WriteLine("Успешна покупка!");
                }
                else Console.WriteLine("Недостатъчна наличност!");  
            }
            else Console.WriteLine("Тази кола вече е закупена!");

            ShowPeopleCarCollection();
        }

        private void SellCar()
        {
            //Без верификация на изборите, за да не усложняваме излишно
            Console.WriteLine("Изберете кой ще бъде продавача:");
            ShowPeople();
            Console.ForegroundColor = ConsoleColor.White;
            int buyerNum = int.Parse(Console.ReadLine());
            Person buyer = _users[buyerNum - 1];

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Изберете коя кола иска да продаде:");

            var ownedCars = _cars.Where(c => c.OwnerID == buyer.ID).ToList();

            ShowCarsInList(ownedCars);

            //С верификация
            Console.ForegroundColor = ConsoleColor.White;
            int chosenCarNum = int.Parse(Console.ReadLine());
            Console.ForegroundColor = ConsoleColor.Magenta;

            if (chosenCarNum >0 && chosenCarNum < ownedCars.Count)
            {
                Car chosenCar = ownedCars[chosenCarNum - 1];
                Service.SellCar(chosenCar, buyer);
                RefreshLocalLists();
               
                Console.WriteLine("Колата е продадена!");
            }
            else Console.WriteLine("Некоректен избор. Моля опитайте отново!");

            ShowPeopleCarCollection();
        }

        private void ShowPeople()
        {
            int br = 0;
            foreach(Person prs in _users)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{br + 1}) {prs.FirstName} {prs.LastName} {prs.Money}$");
                br++;
            }
        }

        private void ShowCarsInList(List<Car> cars)
        {
            int br = 0;
            foreach(Car car in cars)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"{br + 1}) {car.Color} {car.Model} {car.Year} ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{car.Price}$");
                br++;

                if (!(car.OwnerID is null))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine(" закупена");
                }
                else Console.WriteLine();
            }
        }

        public void ShowPeopleCarCollection()
        {
            foreach(Person prs in _users)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine($"{prs.FirstName} {prs.LastName} {prs.Money}$");
                Console.ForegroundColor = ConsoleColor.Blue;

               
                int br = 0;
                foreach (Car car in _cars)
                {
                    if (car.OwnerID == prs.ID)
                    {
                        Console.WriteLine($"--- {car.Color} {car.Model} {car.Year} {car.Price}$");
                        br++;
                    }
                }
                if (br == 0) Console.WriteLine("--- няма закупени коли ---");
            }
        }
    }
}

