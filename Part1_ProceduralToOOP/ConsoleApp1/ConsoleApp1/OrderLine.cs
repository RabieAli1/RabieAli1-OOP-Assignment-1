using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class OrderLine
    {
        public Product Product { get;  }
        public int Quantity {  get;  }


        public OrderLine(Product product, int quantity)
        {
            if (product == null)
                throw new ArgumentNullException("cannot be Null");
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException("cannot be Zero or less than zero");

            Product = product;
            Quantity = quantity;
            
        }
        public decimal LineTotal => Product.ProductPrices * Quantity;
    }
}
