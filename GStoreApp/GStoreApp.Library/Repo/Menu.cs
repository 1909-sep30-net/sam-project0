using System;
using System.Collections.Generic;
using System.Text;
using GStoreApp.Library.Model;

namespace GStoreApp.Library.Repo
{
    public class Menu
    {
        const string store = "Arlington";
        public void MainMenu()
        {
            int mainMenu = 0;
            Console.WriteLine("Welcome to GCSotre!");
            Console.WriteLine("How can I help you today?");
            Console.WriteLine("1. Place Order");
            Console.WriteLine("2. Display Details of a Previous Order by Order Id");
            Console.WriteLine("3. Display All History Order by Store");
            Console.WriteLine("4. Display All History Order by Customer");
            Console.WriteLine("5. Exit");
            Console.WriteLine("---------------------");


            //need to handle exception later
            do
            {
                Console.WriteLine("Please Enter Your Answer(1-5):  ");

                try
                {
                    mainMenu = Int32.Parse(Console.ReadLine());
                    if (mainMenu < 1 || mainMenu > 5)
                    {
                        Console.WriteLine("Input must be between 1 to 5");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Input must be between 1 to 5");
                }

            } while (mainMenu < 1 || mainMenu > 5);


            switch (mainMenu)
            {
                case 1:
                    CusotmerMenu();
                    break;
                case 2:
                    SearchOrder();
                    break;
                case 3:
                    //SearchByName();
                    break;
                case 4:
                    //DetailsOrder();
                    break;
                default:
                    break;
            }
        }

        public void CusotmerMenu()
        {
            int poMenu = 0;
            string store = "Arlinton";
            Console.WriteLine("If you are a new customer, press 1");
            Console.WriteLine("to add new customer.");
            Console.WriteLine("Or press 2 to search your name."); ;
            Console.WriteLine("--------");
            Console.WriteLine("press 0 to back to Mainmenu.");
            Console.WriteLine("----------------------");
            Console.WriteLine("Please Enter: ");

            do
            {
                Console.WriteLine("Please Enter Your Answer(0-2):  ");

                try
                {
                    poMenu = Int32.Parse(Console.ReadLine());
                    if (poMenu < 0 || poMenu > 2)
                    {
                        Console.WriteLine("Input must be between 0 to 2");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Input must be between 0 to 2");
                }

            } while (poMenu < 0 || poMenu > 2);

            switch (poMenu)
            {
                case 1:
                    Console.WriteLine("Please Enter your first name:");
                    string fName = Console.ReadLine();
                    Console.WriteLine("Please Enter your last name: ");
                    string lName = Console.ReadLine();
                    //Console.WriteLine("Please enter you default store: ");
                    //string store = Console.ReadLine();
                    Customer newGuys = new Customer(fName, lName, store);
                    newGuys.AddCustomer(fName, lName, store);
                    PlaceOrder(newGuys);
                    break;

                case 2:
                    Console.WriteLine("Please Enter your first name:");
                    fName = Console.ReadLine();
                    Console.WriteLine("Please Enter your last name: ");
                    lName = Console.ReadLine();
                    Customer oldGuys = new Customer(fName, lName, store);
                    oldGuys.SearchCustomer(fName, lName);
                    PlaceOrder(oldGuys);
                    break;

                default:
                    break;
            }

            Console.Clear();
            MainMenu();
        }

        public void PlaceOrder( Customer customer )
        {
            Console.WriteLine("Here is our menu today: ");
            Console.WriteLine("1. Nintendo Switch");
            Console.WriteLine("2. Xbox ONE");
            Console.WriteLine("3. Playstation 4 Pro");
            Console.WriteLine("If you want NS*1, Xbox*1, PS4*1,");
            Console.WriteLine("Please Enter 111");
            Console.WriteLine("If you want Xbox*1, PS*1");
            Console.WriteLine("Please Enter 011");
            Console.WriteLine("-----------------------");
            Console.WriteLine("Please Enter you order: ");
            string order = Console.ReadLine();
            Repo yourorder = new Repo();
            yourorder.OrderPlaced(customer, order, store);

        }

        public void SearchOrder()
        {
            int orderNum;
            Console.WriteLine("Please Enter your order number: ");
            orderNum = Int32.Parse(Console.ReadLine());
            Console.WriteLine(orderNum);
            Repo search = new Repo();
            search.SearchPastOrder(orderNum);
        }


    }
}
