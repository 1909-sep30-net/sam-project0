using System;
using System.Collections.Generic;
using System.Text;
using GStoreApp;
using DB.Repo;

namespace GStoreApp.ConsoleApp
{
    public class Menu
    {
        public void MainMenu()
        {
            int mainMenu;

            Console.WriteLine("Welcome to GCSotre!");
            Console.WriteLine("How can I help you today?");
            Console.WriteLine("1. Place Order");
            Console.WriteLine("2. Display Details of a Previous Order by Order Id");
            Console.WriteLine("3. Display All History Order by Store");
            Console.WriteLine("4. Display All History Order by Customer");
            Console.WriteLine("0. Exit");
            Console.WriteLine("---------------------");

            mainMenu = InputCheckInt(1);

            switch (mainMenu)
            {
                case 1:
                    CustomerMenu();
                    break;
                case 2:
                    SearchOrder();
                    break;
                case 3:
                    //SearchByStore();
                    break;
                case 4:
                    //SearchByCustomer();
                    break;
                default:
                    break;
            }
        }

        public void CustomerMenu()
        {
            int poMenu = 0;
            Console.WriteLine("If you are a new customer, press 1");
            Console.WriteLine("to add new customer.");
            Console.WriteLine("Or press 2 to search your name."); ;
            Console.WriteLine("--------");
            Console.WriteLine("press 0 to back to Mainmenu.");
            Console.WriteLine("----------------------");
            Console.WriteLine("Please Enter: ");

            poMenu = InputCheckInt(2);

            switch (poMenu)
            {
                case 1:
                    Console.WriteLine("Please Enter your first name:");
                    string fName = Console.ReadLine();
                    Console.WriteLine("Please Enter your last name: ");
                    string lName = Console.ReadLine();
                    Console.WriteLine("Please enter your phone number: ");
                    Console.WriteLine("The format must be: (XXX)XXX-XXXX:  ");
                    string phone = Console.ReadLine();
                    Console.WriteLine("Please enter your default store: ");
                    int favStore = Int32.Parse(Console.ReadLine());
                    Repo newGuys = new Repo();
                    newGuys.AddCustomer(fName, lName, phone, favStore);
                    PlaceOrder(fName, lName, favStore);
                    break;

                case 2:
                    Console.WriteLine("Please Enter your first name:");
                    fName = Console.ReadLine();
                    Console.WriteLine("Please Enter your last name: ");
                    lName = Console.ReadLine();
                    Repo oldGuys = new Repo();
                    favStore = oldGuys.SearchCustomer(fName, lName);
                    PlaceOrder(fName, lName, favStore);
                    break;

                default:
                    break;
            }

            Console.Clear();
            MainMenu();
        }

        public void PlaceOrder( string fName, string lName, int store )
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
            //check input format
            string orderAmount = Console.ReadLine();
            Repo order = new Repo();
            order.OrderPlaced( fName, lName, store, orderAmount);

            MainMenu();
        }

        public void SearchOrder()
        {
            int orderNum;
            Console.WriteLine("Please Enter your order number: ");
            //Check Input Error
            orderNum = Int32.Parse(Console.ReadLine());
            Console.WriteLine(orderNum);
            Repo search = new Repo();
            search.SearchPastOrder(orderNum);
            MainMenu();
        }

        public void SearchByStore()
        {
            int storeId;
            Console.WriteLine("Please Enter StoreId");
            storeId = Int32.Parse(Console.ReadLine());
            Repo search = new Repo();
            search.DisplayOrderByStore(storeId);
        }

        public void SearchByCustomer()
        {
            Console.WriteLine("Please Enter the Customer ID:");
            int customerId = Int32.Parse(Console.ReadLine());
            Repo search = new Repo();
            search.DisplayOrderByCustomer(customerId);
        }

        //menuType = 1 --> MainMenu
        //         = 2 --> CustomerMenu
        public int InputCheckInt ( int menuType )
        {
            int finalInput = 0;
            int menuMaxOption = 0;
            
            if ( menuType == 1 )
            {
                menuMaxOption = 4;
            } else if ( menuType == 2 ){
                menuMaxOption = 2;
            }

            do
            {
                Console.WriteLine($"Please Enter Your Answer(0-{menuMaxOption}):  ");

                try
                {
                    finalInput = Int32.Parse(Console.ReadLine());
                    if (finalInput < 0 || finalInput > menuMaxOption)
                    {
                        Console.WriteLine($"Input must be between 0 to {menuMaxOption}");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Input must be between 0 to {menuMaxOption}");
                }

            } while (finalInput < 0 || finalInput > menuMaxOption );

            return finalInput;
        }
    }
}
