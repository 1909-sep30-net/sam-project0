using System;
using System.Collections.Generic;
using System.Text;

namespace GStoreApp.Library.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }

        public Product( int id, string name, double cost)
        {
            Id = id;
            Name = name;
            Cost = cost;
        }
    }
}
