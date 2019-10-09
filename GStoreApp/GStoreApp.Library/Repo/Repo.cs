using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GStoreApp.Library.Model;

namespace GStoreApp.Library.Repo
{
    public class Repo
    {
        
        public void SearchCustomer( string fName, string lName )
        {
            //search customer from database
        }

        public void SearchPastOrder( int orderNum )
        {
            //search past order in the database
        }

        public void OrderPlaced(Customer customer, string order, string store)
        {
            int productNum;
            string name;
            int cost;
            List<Product> consoleOrder = new List<Product>();
            DateTime time = DateTime.Now;
            // Store currentStore = new Store(1, store, 76010, );

            for (int i = 0 ; i < 3 ; i++ )
            {
                productNum = (int)order[i];
                if ( i == 0 )
                {
                    name = "Nintento Switch";
                    cost = 199;

                } 
                else if ( i == 1 )
                {
                    name = "Xbox ONE";
                    cost = 299;
                } 
                else
                {
                    name = "Playstation 4 Pro";
                    cost = 399;
                }
                Product product = new Product(name, productNum, cost);
                consoleOrder.Add(product);
            }

            Order yourOrder = new Order(customer, consoleOrder, time, store);

        }
    }
}
