using System;

namespace GStoreApp.Library.Model
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DefaultStore { get; set; }

        public Customer( string fName, string lName, string defaultStore)
        {
            FirstName = fName;
            LastName = lName;
            DefaultStore = defaultStore;
        }
        
        public void AddCustomer( string fName, string lName, string defaultstore )
        {
            // Add new Customer into database
        }

        public void SearchCustomer(string fName, string lName)
        {
            //search customer from database
        }
    }
}
