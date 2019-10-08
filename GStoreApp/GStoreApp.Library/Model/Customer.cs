using System;

namespace GStoreApp.Library.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Customer( int id, string fName, string lName)
        {
            Id = id;
            FirstName = fName;
            LastName = lName;
        }
        
    }
}
