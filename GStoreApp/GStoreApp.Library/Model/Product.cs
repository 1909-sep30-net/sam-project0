using System;
using System.Collections.Generic;
using System.Text;

namespace GStoreApp.Library.Model
{
    public class Product
    {
        public string Name { get; set; }
        public int Amount { get; set; }
        public double Cost { get; set; }

        public Product( string name, int amount, double cost)
        {
            Name = name;
            Amount = amount;
            Cost = cost;
        }
    }
}
