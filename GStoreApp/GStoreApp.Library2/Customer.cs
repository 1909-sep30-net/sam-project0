using System;
using System.Collections.Generic;

namespace GStoreApp.Library
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int? FavoriteStore { get; set; }

    }
}
