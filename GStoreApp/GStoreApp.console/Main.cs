using System;
using System.Collections.Generic;
using System.Linq;
using GStoreApp.Library;

namespace GStoreApp.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int mainMenu = 0;
            Console.WriteLine("Welcome to GCSotre!");
            Console.WriteLine("How can I help you today?");
            Console.WriteLine("1. Place Order");
            Console.WriteLine("2. Add New Customer");
            Console.WriteLine("3. Search Customer by Name");
            Console.WriteLine("4. Display Details of a Previous Order by Number");
            Console.WriteLine("5. Display All History Order by Store");
            Console.WriteLine("6. Display All History Order by Customer");
            Console.WriteLine("7. Exit");
            Console.WriteLine("---------------------");


            //need to handle exception later
            do
            {
                Console.WriteLine("Please Enter Your Answer(1-7):  ");

                try
                {
                    mainMenu = Int32.Parse(Console.ReadLine());
                    if (mainMenu < 1 || mainMenu > 7)
                    {
                        Console.WriteLine("Input must be between 1 to 7");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Input must be between 1 to 7");
                }

            } while (mainMenu < 1 || mainMenu > 7);


            switch (mainMenu)
            {
                case 1:
                    //PlaceOrder();
                    break;
                case 2:
                    //AddCustomer();
                    break;
                case 3:
                    //SearchByName();
                    break;
                case 4:
                    //DetailsOrder();
                    break;
                case 5:
                    //HistoryByStore();
                    break;
                case 6:
                    //HistoryByStore();
                    break;
                default:
                    Console.WriteLine("Have a good day!");
                    break;

            }

        }
    }
}
