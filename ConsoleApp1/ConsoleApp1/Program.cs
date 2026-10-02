using System;
using System.Linq;

namespace ConsoleApp1;

public class Program
{
    static void Main()
    {
        Console.WriteLine("OOP Order System (C#)");

        var store = new Store();
        SeedSampleData(store);
        RunDemoScenario(store);

        PrintCustomers(store);
        PrintProducts(store);
        PrintAllOrders(store);
        Console.WriteLine($"\nPaid sales total after demo: {store.TotalSalesPaidOnly():F2}");

        RunInteractiveMenu(store);
    }

    // ===== Seed + Demo =====
    static void SeedSampleData(Store store)
    {
        store.AddCustomer(new Customer(1, "Mona Ali", "mona@example.com", "Cairo", true));
        store.AddCustomer(new Customer(2, "Omar Hassan", "omar@example.com", "Alexandria", false));
        store.AddCustomer(new Customer(3, "Sara Nabil", "sara@example.com", "Giza", false));

        store.AddProduct(new Product(101, "USB Cable", 50m, 100));
        store.AddProduct(new Product(102, "Wireless Mouse", 250m, 40));
        store.AddProduct(new Product(103, "Mechanical Keyboard", 1200m, 15));
        store.AddProduct(new Product(104, "Laptop Stand", 400m, 25));
    }

    static void RunDemoScenario(Store store)
    {
        store.CreateOrder(1001, 1, "2026-09-15");
        store.AddLineToOrder(1001, 101, 2);
        store.AddLineToOrder(1001, 102, 1);
        store.MarkOrderPaid(1001);

        store.CreateOrder(1002, 2, "2026-09-15");
        store.AddLineToOrder(1002, 103, 1);
        store.AddLineToOrder(1002, 104, 1);

        store.CreateOrder(1003, 3, "2026-09-16");
        store.AddLineToOrder(1003, 101, 5);
        store.MarkOrderPaid(1003);
    }

    // ===== Printing =====
    static void PrintCustomers(Store store)
    {
        Console.WriteLine($"\n=== CUSTOMERS ({store.Customers.Count}) ===");
        foreach (var c in store.Customers)
            Console.WriteLine(c);
    }

    static void PrintProducts(Store store)
    {
        Console.WriteLine($"\n=== PRODUCTS ({store.Products.Count}) ===");
        foreach (var p in store.Products)
            Console.WriteLine(p);
    }

    static void PrintOrder(Order order)
    {
        Console.WriteLine($"\n=== ORDER #{order.Id} ===");
        Console.WriteLine($"Date: {order.Date}");
        Console.WriteLine($"Customer: {order.Customer.Name} (#{order.Customer.Id})");
        Console.WriteLine($"Paid: {(order.IsPaid ? "yes" : "no")}");
        Console.WriteLine("Lines:");

        foreach (var line in order.Lines)
        {
            Console.WriteLine($"  - {line.Product.Name}  x{line.Quantity}  @{line.Product.ProductPrices:F2}  = {line.LineTotal:F2}");
        }

        Console.WriteLine($"TOTAL: {order.CalculateTotal():F2}");
    }

    static void PrintAllOrders(Store store)
    {
        Console.WriteLine($"\n=== ALL ORDERS ({store.Orders.Count}) ===");
        foreach (var o in store.Orders)
            PrintOrder(o);
    }

    // ===== Menu =====
    static void PrintMenu()
    {
        Console.WriteLine("\n---------- MENU ----------");
        Console.WriteLine("1) Print customers");
        Console.WriteLine("2) Print products");
        Console.WriteLine("3) Print all orders");
        Console.WriteLine("4) Print one order by id");
        Console.WriteLine("5) Create order");
        Console.WriteLine("6) Add line to order");
        Console.WriteLine("7) Mark order paid");
        Console.WriteLine("8) Show paid sales total");
        Console.WriteLine("0) Exit");
        Console.Write("Choice: ");
    }

    static int ReadInt(string prompt)
    {
        int value;
        Console.Write(prompt);
        while (!int.TryParse(Console.ReadLine(), out value))
            Console.Write("Invalid number, try again: ");
        return value;
    }

    static string ReadString(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    static void RunInteractiveMenu(Store store)
    {
        int choice = -1;
        while (choice != 0)
        {
            PrintMenu();
            if (!int.TryParse(Console.ReadLine(), out choice))
                choice = -1;

            try
            {
                switch (choice)
                {
                    case 1:
                        PrintCustomers(store);
                        break;
                    case 2:
                        PrintProducts(store);
                        break;
                    case 3:
                        PrintAllOrders(store);
                        break;
                    case 4:
                        int id = ReadInt("Order id: ");
                        var order = store.FindOrder(id);
                        if (order == null)
                            Console.WriteLine($"ERROR: order id {id} not found.");
                        else
                            PrintOrder(order);
                        break;
                    case 5:
                        store.CreateOrder(
                            ReadInt("Order id: "),
                            ReadInt("Customer id: "),
                            ReadString("Date (YYYY-MM-DD): "));
                        break;
                    case 6:
                        store.AddLineToOrder(
                            ReadInt("Order id: "),
                            ReadInt("Product id: "),
                            ReadInt("Quantity: "));
                        break;
                    case 7:
                        store.MarkOrderPaid(ReadInt("Order id: "));
                        break;
                    case 8:
                        Console.WriteLine($"Paid sales total: {store.TotalSalesPaidOnly():F2}");
                        break;
                    case 0:
                        Console.WriteLine("Bye.");
                        break;
                    default:
                        Console.WriteLine("Unknown choice.");
                        break;
                }
            }
            catch (Exception ex) when (ex is InvalidOperationException
                                    || ex is ArgumentException)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }
    }
}