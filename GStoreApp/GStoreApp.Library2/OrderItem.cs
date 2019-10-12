using System;
using System.Collections.Generic;

namespace GStoreApp.Library
{
    public partial class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public string ProducutName { get; set; }
        public int Amount { get; set; }

    }
}
