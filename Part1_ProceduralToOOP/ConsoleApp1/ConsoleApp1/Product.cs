using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class Product
    {
        public Product(int iD, string name, decimal productPrices, int productStock)
        {
            if (iD <= 0)
                throw new ArgumentOutOfRangeException("Out Off The Reng");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Cannot be Empity");
            if (productPrices < 0)
                throw new ArgumentOutOfRangeException("Cannot be less than Zerooo");
            if (productStock < 0)
                throw new ArgumentOutOfRangeException("Cannot be less than Zerooo");
            ID = iD;
            Name = name;
            ProductPrices = productPrices;
            ProductStock = productStock;



        }


        public int ID { get; }
        public string Name { get; }
        public decimal ProductPrices { get; }
        public int ProductStock { get; private set; }

        public void DecreaseStock(int quantity)
        {
            if (ProductStock < quantity)
                throw new InvalidOperationException("Not enough stock.");
            ProductStock -= quantity;
        }

        public override string ToString()
        {
            return $"#{ID}  {Name}  price={ProductPrices:F2}  stock={ProductStock}";
        }
    }
}
