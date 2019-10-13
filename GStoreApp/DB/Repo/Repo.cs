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

        public IEnumerable<l.Customer> SearchCustomer( l.Customer customer )
        {
            //Search Customer from database
            IQueryable<d.Customer> cusotmerFound
                = dbcontext.Customer.Where(c => c.LastName == customer.LastName
                                           && c.FirstName == customer.FirstName);

            // IEnumerable<l.Customer> customerFound = Mapper.MapCustomer(entity);
            
            return cusotmerFound.Select(Mapper.MapCustomer);
        }

        public string OrderPlaced( l.Order order )
        {
            IQueryable<d.Inventory> CurrentInventoryQ
                = dbcontext.Inventory.Where(i => i.StoreId == order.StoreId)
                                     .AsNoTracking();
            IEnumerable<l.Inventory> CurrentInventoryE = CurrentInventoryQ.Select(Mapper.MapInventory);
            List < l.Inventory > invent = CurrentInventoryE.ToList();
            for ( int j = 0; j < CurrentInventoryE.Count(); j++)
            {
                if ( j == 0 )
                {
                    if ( invent[j].Amount - order.NSAmount < 0 )
                    {
                        return "Order Failed, not enough Inventory";
                    }
                    else
                    {
                        var nsN = dbcontext.Inventory.Where(i => i.ProductId == j + 1)
                                                     .First();
                        nsN.Amount = invent[j].Amount - order.NSAmount;
                    }
                } 
                else if ( j == 1 )
                {
                    if (invent[j].Amount - order.XBAmount < 0)
                    {
                        return "Order Failed, not enough Inventory";
                    }
                    else
                    {
                        var xbN = dbcontext.Inventory.Where(i => i.ProductId == j + 1)
                                                     .First();
                        xbN.Amount = invent[j].Amount - order.XBAmount;
                    }
                }
                else
                {
                    if (invent[j].Amount - order.PSAmount < 0)
                    {
                        return "Order Failed, not enough Inventory";
                    }
                    else
                    {
                        var psN = dbcontext.Inventory.Where(i => i.ProductId == j + 1)
                                                     .First();
                        psN.Amount = invent[j].Amount - order.PSAmount;
                        dbcontext.SaveChanges();
                    }
                }
            }

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
            return "Order Success!!";

        }

        public l.OrderOverView SearchPastOrder(int orderid)
        {
            d.OrderOverView overView = dbcontext.OrderOverView.Find(orderid);
            if ( overView == null)
            {
                return null;
            } else
            {
                l.OrderOverView overView1 = Mapper.MapOrderOverView(overView);
                return overView1;
            }
            
        }

        public IEnumerable<l.OrderItem> SearchPastOrderItem(int orderid)
        {
            IQueryable<d.OrderItem> orderItems
                = dbcontext.OrderItem.Where( o => o.OrderId == orderid )
                                     .AsNoTracking();
            return orderItems.Select(Mapper.MapOrderItem);
        }


        public IEnumerable<l.OrderOverView> DisplayOrderByStore ( int storeId )
        {
            //search order by store id
            IQueryable<d.OrderOverView> orderHistory
                = dbcontext.OrderOverView.Where(o => o.StoreId == storeId)
                                         .AsNoTracking();
            return orderHistory.Select(Mapper.MapOrderOverView);
        }

        public IEnumerable<l.OrderOverView> DisplayOrderByCustomer( int customerId )
        {
            //search order by customer id
            IQueryable<d.OrderOverView> orderHistory
                = dbcontext.OrderOverView.Where(o => o.CustomerId == customerId)
                                         .AsNoTracking();
            return orderHistory.Select(Mapper.MapOrderOverView);
        }

        public l.Store CheckIfStoreExists( int storeId)
        {
            d.Store store = dbcontext.Store.Find(storeId);
            if (store == null)
            {
                return null;
            }
            else
            {
                l.Store storeFind = Mapper.MapStore(store);
                return storeFind;
            }
        }
    }
}
