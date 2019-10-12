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
        public void AddCustomer(string fName, string lName, string phone, int favStore)
        {
            //Add Customer Data into database
            GStoreApp.Library.Customer customer = new l.Customer();
            customer.FirstName = fName;
            customer.LastName = lName;
            customer.PhoneNumber = phone;
            customer.FavoriteStore = favStore;

            d.Customer entity = Mapper.MapCustomer(customer);
            dbcontext.Add(entity);
            dbcontext.SaveChanges();
        }

        public int SearchCustomer(string fName, string lName )
        {
            int storeId = 0;
            //Search Customer from database
            return storeId;
        }

        public void OrderPlaced( string fName, string lName, int storeId, string order)
        {

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
