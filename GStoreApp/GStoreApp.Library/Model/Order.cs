using System;
using System.Collections.Generic;
using System.Text;

namespace GStoreApp.Library.Model
{
    public class Order
    {
        public Customer Customer { get; set; }
        public List<Product> Products { get; set; }
        public DateTime Time { get; set; }
        public string Store { get; set; }
        
        public Order( Customer customer, List<Product> products, DateTime time, string store)
        {
            Customer = customer;
            Products = products;
            Time = time;
            Store = store;
        }
    }
}
