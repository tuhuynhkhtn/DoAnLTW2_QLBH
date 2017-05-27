using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCQLBH.Models
{
    public class Cart
    {
        public IList<CartItem> Items { get; }
        public Cart()
        {
            Items = new List<CartItem>();
        }

        public int TotalQuantity()
        {
            return Items.Sum(i => i.Quantity);
        }
    }

    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}