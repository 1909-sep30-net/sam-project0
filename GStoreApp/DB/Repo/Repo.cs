using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using d = DB.Entities;
using l = GStoreApp.Library;

namespace DB.Repo
{   
    //Connect UI with database, but not business logic
    public class Repo
    {   
        private static d.GCStoreContext dbcontext;

        public Repo()
        {
            var optionsBuilder = new DbContextOptionsBuilder<d.GCStoreContext>();
            optionsBuilder.UseSqlServer(Config.connectionString);
            dbcontext = new d.GCStoreContext(optionsBuilder.Options);
        }
        public void AddCustomer(l.Customer customer)
        {
            //Add Customer Data into database

            d.Customer entity = Mapper.MapCustomer(customer);
            dbcontext.Add(entity);
            dbcontext.SaveChanges();
        }

        public l.Customer SearchCustomer( l.Customer customer )
        {
            //Search Customer from database
            d.Customer entity = Mapper.MapCustomer(customer);

            entity = dbcontext.Customer.Where(c => c.LastName.Contains(entity.LastName)
                                                && c.FirstName.Contains(entity.FirstName))
                                       .First();

            l.Customer customerFound = Mapper.MapCustomer(entity);
            
            return customerFound;
        }

        public void OrderPlaced( l.Order order )
        {
            d.OrderOverView orderOverView = Mapper.MapOrderOverView(order);
            dbcontext.Add(orderOverView);
            dbcontext.SaveChanges();

            int orderId = orderOverView.OrderId;

            if (order.NSAmount != 0)
            {
                int amount1 = order.NSAmount;
                string name1 = "NSwitch";
                d.OrderItem itemN = Mapper.MapOrderItem(amount1, orderId, name1);
                dbcontext.Add(itemN);
            }
            if (order.XBAmount != 0)
            {
                int amount2 = order.XBAmount;
                string name2 = "Xbox One";
                d.OrderItem itemX = Mapper.MapOrderItem(amount2, orderId, name2);
                dbcontext.Add(itemX);
            }
            if (order.PSAmount != 0)
            {
                int amount3 = order.PSAmount;
                string name3 = "Playstation 4 Pro";
                d.OrderItem itemP = Mapper.MapOrderItem(amount3, orderId, name3);
                dbcontext.Add(itemP);
            }

            dbcontext.SaveChanges();


        }

        public void SearchPastOrder( int orderid )
        {
            //search past order in the database

        }

        public void DisplayOrderByStore ( int storeId )
        {
            //search order by store id
        }

        public void DisplayOrderByCustomer( int customerId )
        {
            //search order by customer id
        }


    }
}
