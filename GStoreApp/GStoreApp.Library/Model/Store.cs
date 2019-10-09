using System;
using System.Collections.Generic;
using System.Text;

namespace GStoreApp.Library.Model
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PostalCode { get; set; }

        public List<Product> Products { get; set; }

        public Store( int id, string name, int postalCode, List<Product> products)
        {
            Id = id;
            Name = name;
            PostalCode = postalCode;
            Products = products;
        }
    }
}
