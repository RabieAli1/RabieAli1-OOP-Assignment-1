using Builder_Pattern;
using InvoiceSystem;

class Program
{
    static void Main()
    {
        var billing = new AddressBuilder()
            .WithStreet("12 Nile St").WithCity("Cairo").WithState("C")
            .WithZipCode("11511").WithCountry("EG")
            .Build();

        var shipping = new AddressBuilder()
            .WithStreet("5 Pyramids Rd").WithCity("Giza").WithState("G")
            .WithZipCode("12611").WithCountry("EG")
            .Build();

        var order = new OrderBuilder()
            .WithOrderDate(new DateTime(2026, 10, 2))
            .WithPaymentMethod("Card").WithCurrency("EGP")
            .WithSubTotal(100m).WithDiscount(10m).WithTax(14m)
            .Build();

        var invoice = new InvoiceBuilder()
            .WithId(1)
            .WithCustomer("Mona Ali", "mona@example.com")
            .WithBillingAddress(billing)
            .WithShippingAddress(shipping)
            .WithOrder(order)
            .Build();

        Console.WriteLine($"{invoice.CustomerName} - total {invoice.Order.TotalAmount}");   // 104

        try { new InvoiceBuilder().WithId(2).Build(); }
        catch (InvalidOperationException ex) { Console.WriteLine($"OK: {ex.Message}"); }
    }
}