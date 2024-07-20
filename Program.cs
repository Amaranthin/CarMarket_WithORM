using CarMarket_WithORM.View;

namespace CarMarket_WithORM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Display display = new Display();
            display.ShowMainMenu();
        }
    }
}
