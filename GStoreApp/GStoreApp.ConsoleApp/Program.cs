using System;

namespace GStoreApp.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int mainMenu;
            Menu m = new Menu();

            do
            {
                Console.WriteLine("Welcome to GCSotre!");
                Console.WriteLine("How can I help you today?");
                Console.WriteLine("1. Place Order");
                Console.WriteLine("2. Display Details of a Previous Order by Order Id");
                Console.WriteLine("3. Display All History Order by Store");
                Console.WriteLine("4. Display All History Order by Customer");
                Console.WriteLine("0. Exit");
                Console.WriteLine("---------------------");

                mainMenu = m.InputCheckInt(1);

                switch (mainMenu)
                {
                    case 1:
                        m.CustomerMenu();
                        break;
                    case 2:
                        m.SearchOrder();
                        break;
                    case 3:
                        m.SearchByStore();
                        break;
                    case 4:
                        m.SearchByCustomer();
                        break;
                    default:
                        break;
                }
            } while (mainMenu != 0);
        }
    }
}
