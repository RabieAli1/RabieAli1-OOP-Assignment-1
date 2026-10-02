

namespace ConsoleApp1;

public class Store
{
    private const int MaxCustomers = 50;
    private const int MaxProducts = 50;
    private const int MaxOrders = 100;

    private readonly List<Customer> _customers = new();
    private readonly List<Product> _products = new();
    private readonly List<Order> _orders = new();

    public IReadOnlyList<Customer> Customers => _customers;
    public IReadOnlyList<Product> Products => _products;
    public IReadOnlyList<Order> Orders => _orders;

    // Customers
    public Customer? FindCustomer(int id) =>
        _customers.FirstOrDefault(c => c.Id == id);

    public void AddCustomer(Customer customer)
    {
        if (_customers.Count >= MaxCustomers)
            throw new InvalidOperationException("Customer list is full.");
        if (FindCustomer(customer.Id) != null)
            throw new InvalidOperationException($"Customer id {customer.Id} already exists.");

        _customers.Add(customer);
    }

    // Products
    public Product? FindProduct(int id) =>
        _products.FirstOrDefault(p => p.ID == id);

    public void AddProduct(Product product)
    {
        if (_products.Count >= MaxProducts)
            throw new InvalidOperationException("Product list is full.");
        if (FindProduct(product.ID) != null)
            throw new InvalidOperationException($"Product id {product.ID} already exists.");

        _products.Add(product);
    }

    // Orders 
    public Order? FindOrder(int id) =>
        _orders.FirstOrDefault(o => o.Id == id);

    public Order CreateOrder(int orderId, int customerId, string date)
    {
        if (_orders.Count >= MaxOrders)
            throw new InvalidOperationException("Order list is full.");
        if (FindOrder(orderId) != null)
            throw new InvalidOperationException($"Order id {orderId} already exists.");

        var customer = FindCustomer(customerId)
            ?? throw new InvalidOperationException($"Customer id {customerId} not found.");

        var order = new Order(orderId, customer, date);
        _orders.Add(order);
        return order;
    }

    public void AddLineToOrder(int orderId, int productId, int quantity)
    {
        var order = FindOrder(orderId)
            ?? throw new InvalidOperationException($"Order id {orderId} not found.");
        var product = FindProduct(productId)
            ?? throw new InvalidOperationException($"Product id {productId} not found.");

        var line = new OrderLine(product, quantity);  

        if (!order.CanAddLine)
            throw new InvalidOperationException("Order cannot accept more lines.");

        product.DecreaseStock(quantity);               
        order.AddLine(line);
    }

    public void MarkOrderPaid(int orderId)
    {
        var order = FindOrder(orderId)
            ?? throw new InvalidOperationException($"Order id {orderId} not found.");
        order.MarkPaid();
    }

    public decimal TotalSalesPaidOnly() =>
        _orders.Where(o => o.IsPaid).Sum(o => o.CalculateTotal());
}