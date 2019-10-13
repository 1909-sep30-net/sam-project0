using System;
using System.Collections.Generic;
using System.Text;
using GStoreApp;
using System.Linq;
using DB.Repo;
using GStoreApp.Library;

namespace GStoreApp.ConsoleApp
{
    public class Menu
    {
        public void MainMenu()
        {
            //int mainMenu;

            //Console.WriteLine("Welcome to GCSotre!");
            //Console.WriteLine("How can I help you today?");
            //Console.WriteLine("1. Place Order");
            //Console.WriteLine("2. Display Details of a Previous Order by Order Id");
            //Console.WriteLine("3. Display All History Order by Store");
            //Console.WriteLine("4. Display All History Order by Customer");
            //Console.WriteLine("0. Exit");
            //Console.WriteLine("---------------------");

            //mainMenu = InputCheckInt(1);

            //switch (mainMenu)
            //{
            //    case 1:
            //        CustomerMenu();
            //        break;
            //    case 2:
            //        SearchOrder();
            //        break;
            //    case 3:
            //        //SearchByStore();
            //        break;
            //    case 4:
            //        //SearchByCustomer();
            //        break;
            //    default:
            //        break;
            //}
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

                    Customer customerNew = new Customer(fName, lName, phone, favStore ) ;

                    Repo newGuys = new Repo();
                    newGuys.AddCustomer(customerNew);
                    PlaceOrder(customerNew, newGuys);
                    break;

                case 2:
                    Console.WriteLine("Please Enter your first name:");
                    fName = Console.ReadLine();
                    Console.WriteLine("Please Enter your last name: ");
                    lName = Console.ReadLine();

                    Customer customerOld = new Customer(fName, lName);

                    Repo oldGuys = new Repo();
                    Customer customerFound = oldGuys.SearchCustomer(customerOld);
                    Console.WriteLine(customerFound.CustomerId);
                    Console.WriteLine(customerFound.FirstName);
                    Console.WriteLine(customerFound.LastName);
                    Console.WriteLine(customerFound.FavoriteStore);
                    // call repo to get data from database, then send back here
                    PlaceOrder(customerFound, oldGuys);
                    break;

                default:
                    break;
            }

            Console.Clear();
            //MainMenu();
        }

        public void PlaceOrder( Customer customer, Repo repo )
        {
            Console.WriteLine("Here is our menu today: ");
            Console.WriteLine("1. Nintendo Switch: $199.99");
            Console.WriteLine("2. Xbox ONE: $300.00");
            Console.WriteLine("3. Playstation 4 Pro: $299.99");
            Console.WriteLine("If you want NS*1, Xbox*1, PS4*1,");
            Console.WriteLine("Please Enter 111");
            Console.WriteLine("If you want Xbox*1, PS*1");
            Console.WriteLine("Please Enter 011");
            Console.WriteLine("Enter 000 to cancel your order and back to main menu.");
            Console.WriteLine("-----------------------");
            Console.WriteLine("Please Enter you order: ");

            string order;
            double a = -1;
            double b = -1;
            double c = -1;
            bool amountCheck = a == -1 || b == -1 || c == -1;
            do
            {
                order = Console.ReadLine();
                if (order.Length == 3)
                {
                    a = Char.GetNumericValue(order[0]);
                    b = Char.GetNumericValue(order[1]);
                    c = Char.GetNumericValue(order[2]);
                    amountCheck = a == -1 || b == -1 || c == -1;
                    Console.WriteLine($"\n{a} {b} {c}\n");
                    if (order == "000")
                    {
                        break;
                    }
                    else if (amountCheck)
                    {
                        Console.WriteLine("The Order must be 3 numbers.");
                        Console.WriteLine("Please type again");
                        Console.WriteLine("Or 000 to back to main menu:  ");
                    }
                } else
                {
                    Console.WriteLine("The Order must be 3 numbers.");
                    Console.WriteLine("Please type again");
                    Console.WriteLine("Or 000 to back to main menu:  ");
                }
            } while ( amountCheck );



            if (order != "000")
            {
                int ns = Int32.Parse(order[0].ToString());
                int xb = Int32.Parse(order[1].ToString());
                int ps = Int32.Parse(order[2].ToString());
                decimal totalPrice = (ns * 199.99m + xb * 300.00m + ps * 299.99m);

                Console.WriteLine("\nYour Order: ");
                Console.WriteLine($"Customer Name: {customer.FirstName}  {customer.LastName}");
                Console.WriteLine($"Nintendo Switch:  {ns}");
                Console.WriteLine($"XBox One:  {xb}");
                Console.WriteLine($"Playstation 4 Pro:  {ps}");
                Console.WriteLine($"Total price is: ${totalPrice}");
                Console.WriteLine($"Is that correct?(y/n)");

                string confirm = "";
                bool confirmCheck;

                do
                {
                    confirm = Console.ReadLine();
                    confirmCheck = confirm != "n" && confirm != "y";
                    if (confirmCheck)
                    {
                        Console.WriteLine("The input must be y or n");
                        Console.WriteLine("Plese type again(y/n):  ");
                    }
                } while (confirmCheck);

                if (confirm == "y")
                {
                    Order newOrder = new Order(customer, ns, xb, ps, DateTime.Today, totalPrice);
                    repo.OrderPlaced(newOrder);
                    MainMenu();
                }
                else
                {
                    PlaceOrder(customer, repo);
                }
            }
            //MainMenu();
        }

        public void SearchOrder()
        {
            int orderId;
            Console.WriteLine("Please Enter your order number: ");
            orderId = InputCheckInt(999999);
            Repo search = new Repo();
            if (search.SearchPastOrder(orderId) != null)
            {
                int StoreId = search.SearchPastOrder(orderId).StoreId;
                int customerId = search.SearchPastOrder(orderId).CustomerId;
                DateTime orderDate = search.SearchPastOrder(orderId).OrderDate;
                decimal totalPrice = search.SearchPastOrder(orderId).TotalPrice;

                Console.WriteLine("Your Order Detail");
                Console.WriteLine("-------------------");
                Console.WriteLine($"Order ID:   {orderId}");
                Console.WriteLine($"Store ID:   {StoreId}");
                Console.WriteLine($"Costomer ID:  {customerId}");
                Console.WriteLine($"Total Price:  {totalPrice}");

                List<OrderItem> items = search.SearchPastOrderItem(orderId).ToList();
                string productName;
                for (int i = 0; i < items.Count(); i++)
                {

                    if (items[i].ProductName == "NSwitch")
                    {
                        productName = "NSwitch";
                    }
                    else if (items[i].ProductName == "Xbox One")
                    {
                        productName = "Xbox One";
                    }
                    else
                    {
                        productName = "Playstation 4 Pro";
                    }
                    Console.WriteLine($"{productName}:  {items[i].Amount}");
                }
            } else
            {
                Console.WriteLine("Sorry! We cannot find your record.");
                Console.WriteLine("Back to main menu...");
            }
            Console.WriteLine("");
        }

        public void SearchByStore()
        {
            int storeId;
            Console.WriteLine("Please Enter StoreId");
            storeId = InputCheckInt(999999);
             
            Repo search = new Repo();
            List<OrderOverView> history = search.DisplayOrderByStore(storeId).ToList();
            if ( history.Count() != 0)
            {
                int oId;
                int cId;
                DateTime date;
                decimal p;
                Console.WriteLine($"Here is Order History at store with Store ID: {storeId}");
                Console.WriteLine("OrderId, CustomerId, OrderDaTe,          TotalPrice");
                for( int i = 0; i < history.Count(); i++)
                {
                    oId = history[i].OrderId;
                    cId = history[i].CustomerId;
                    date = history[i].OrderDate;
                    p = history[i].TotalPrice;
                    Console.WriteLine($"{oId}        {cId}           {date}   {p}");
                }
            } else
            {
                Console.WriteLine("Sorry! There's no record of this store ID.");
                Console.WriteLine("Back to main menu...");
            }
            Console.WriteLine("Press enter to continue.");
            string stop = Console.ReadLine();
        }

        public void SearchByCustomer()
        {
            Console.WriteLine("Please Enter the Customer ID:");
            int customerId = InputCheckInt(999999);
            Repo search = new Repo();
            List<OrderOverView> history = search.DisplayOrderByCustomer(customerId).ToList();

            if (history.Count() != 0)
            {
                int oId;
                int cId;
                DateTime date;
                decimal p;
                Console.WriteLine($"Here is Order History at store with Customer ID: {customerId}");
                Console.WriteLine("OrderId, StoreId, OrderDaTe,          TotalPrice");
                for (int i = 0; i < history.Count(); i++)
                {
                    oId = history[i].OrderId;
                    cId = history[i].StoreId;
                    date = history[i].OrderDate;
                    p = history[i].TotalPrice;
                    Console.WriteLine($"{oId}        {cId}        {date}   {p}");
                }
            }
            else
            {
                Console.WriteLine("Sorry! There's no record of this customer ID.");
                Console.WriteLine("Back to main menu...");
            }
            Console.WriteLine("Press enter to continue.");
            string stop = Console.ReadLine();
        }

        //menuType = 1 --> MainMenu
        //         = 2 --> CustomerMenu
        public int InputCheckInt ( int menuType )
        {
            int finalInput = -1;
            int menuMaxOption = 0;
            
            if ( menuType == 1 )
            {
                menuMaxOption = 4;
            } else if ( menuType == 2 ){
                menuMaxOption = 2;
            } else
            {
                menuMaxOption = 999999;
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

            } while ( finalInput < 0 || finalInput > menuMaxOption );

            return finalInput;
        }
    }
}
