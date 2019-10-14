using System;
using System.Collections.Generic;
using System.Text;
using GStoreApp;
using System.Linq;
using DB.Repo;
using GStoreApp.Library;
using System.Text.RegularExpressions;
using NLog;


namespace GStoreApp.ConsoleApp
{
    public class Menu
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
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
                    Console.WriteLine("Please enter your 10 digit phone number ");
                    Console.WriteLine("without () or - :   ");
                    string phone = PhoneCheck();
                    Console.WriteLine($"Your phone number is: {phone}");
                    Console.WriteLine("Please enter your default store: ");
                    int favStore = InputCheckInt(999999);
                    Repo newGuys = new Repo();
                    Store storeFound = newGuys.CheckIfStoreExists( favStore );
                    if (storeFound != null)
                    {
                        Console.WriteLine($"Your first name is {fName}");
                        Console.WriteLine($"Your last name is {lName}");
                        Console.WriteLine($"Your phone number is {phone}");
                        Console.WriteLine($"Your favorite Store is {storeFound.StoreName}");
                        Console.WriteLine($"Is That correct?(y/n):   ");
                        
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
                                logger.Warn($"Invalid Input:  {confirmCheck}");
                            }
                        } while (confirmCheck);

                        if (confirm == "y")
                        {
                            Customer customerNew = new Customer(fName, lName, phone, favStore);
                            newGuys.AddCustomer(customerNew);
                            //PlaceOrder(customerNew, newGuys);
                        }

                    } else
                    {
                        Console.WriteLine("Sorry! The Store ID doesn't not exist.");
                        Console.WriteLine("Please press Enter to back to main menu.");
                        string back = Console.ReadLine();
                    }
                    break;

                case 2:
                    Console.WriteLine("Please Enter your first name:");
                    fName = Console.ReadLine();
                    Console.WriteLine("Please Enter your last name: ");
                    lName = Console.ReadLine();

                    Customer customerOld = new Customer(fName, lName);

                    Repo oldGuys = new Repo();
                    List<Customer> customerFound = oldGuys.SearchCustomer(customerOld).ToList();
                    if (customerFound.Count > 0 )
                    {
                        for (int i = 0; i < customerFound.Count; i++)
                        {
                            Console.WriteLine(customerFound[i].FirstName);
                            Console.WriteLine(customerFound[i].LastName);
                            Console.WriteLine(customerFound[i].PhoneNumber);
                            Console.WriteLine(customerFound[i].FavoriteStore);

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
                                    logger.Warn($"Invalid Input:  {confirmCheck}");
                                }
                            } while (confirmCheck);

                            if (confirm == "y")
                            {
                                PlaceOrder(customerFound[i], oldGuys);
                            }
                        }
                    } else
                    {
                        Console.WriteLine("Sorry! We don't have your record.");
                        Console.WriteLine("Press enter to back to main menu");
                        string back = Console.ReadLine();
                    }
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
                int storeId = (int)customer.FavoriteStore;
                do
                {
                    confirm = Console.ReadLine();
                    confirmCheck = confirm != "n" && confirm != "y";
                    if (confirmCheck)
                    {
                        Console.WriteLine("The input must be y or n");
                        Console.WriteLine("Plese type again(y/n):  ");
                        logger.Warn($"Invalid Input:  {confirmCheck}");
                    }
                } while (confirmCheck);

                if (confirm == "y")
                {
                    Order newOrder = new Order(customer, ns, xb, ps, DateTime.Today, totalPrice, storeId);
                    string success = repo.OrderPlaced(newOrder);
                    Console.WriteLine(success);
                    Console.WriteLine("Press Enter to continue");
                    string back = Console.ReadLine();
                }
            }
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
                logger.Info($"There no record for Order ID: {orderId}");
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
                logger.Info($"There no record for Store ID: {storeId}");
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
                logger.Info($"There no record for Customer ID: {customerId}");
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
                        logger.Warn("Input value is Invalid.");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Input must be between 0 to {menuMaxOption}");
                    logger.Error($"Input format is invalid.  {ex}");
                }

            } while ( finalInput < 0 || finalInput > menuMaxOption );

            return finalInput;
        }

        public string PhoneCheck()
        {
            string phoneOp;
            do
            {
                string phoneIn = Console.ReadLine();
                phoneOp = Regex.Replace(phoneIn, @"[^0-9]+", "");
                if (phoneOp.Length != 10)
                {
                    Console.WriteLine("The input must be 10 digit number");
                    logger.Warn("The input of phone number is wrong.");
                    Console.WriteLine("Please type again:  ");
                }

            } while (phoneOp.Length != 10);

            phoneOp = "(" + phoneOp.Substring(0, 3) + ")" + phoneOp.Substring(3, 3)
                    + "-" + phoneOp.Substring(6, 4);
            return phoneOp;
        }
    }
}
