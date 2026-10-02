using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1;

public class Order
{
    private const int MaxLines = 20;
    private const decimal VipDiscountRate = 0.90m;

    private readonly List<OrderLine> _lines = new();

    public int Id { get; }
    public Customer Customer { get; }
    public string Date { get; }
    public bool IsPaid { get; private set; }
    public IReadOnlyList<OrderLine> Lines => _lines;
    public bool CanAddLine => !IsPaid && _lines.Count < MaxLines;  

    public Order(int id, Customer customer, string date)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "Id must be positive.");
        if (customer == null)
            throw new ArgumentNullException(nameof(customer));
        if (string.IsNullOrWhiteSpace(date))
            throw new ArgumentException("Date is required.", nameof(date));

        Id = id;
        Customer = customer;
        Date = date;
        IsPaid = false;
    }

    public void AddLine(OrderLine line)
    {
        if (IsPaid)
            throw new InvalidOperationException("Cannot change a paid order.");
        if (_lines.Count >= MaxLines)
            throw new InvalidOperationException("Order has too many lines.");

        _lines.Add(line);
    }

    public decimal CalculateTotal()
    {
        decimal total = _lines.Sum(l => l.LineTotal);
        if (Customer.IsVip)
            total *= VipDiscountRate;
        return total;
    }

    public void MarkPaid()
    {
        if (_lines.Count == 0)
            throw new InvalidOperationException("Cannot pay an empty order.");
        IsPaid = true;
    }

    public override string ToString() =>
        $"#{Id}  {Customer.Name}  {Date}  paid={(IsPaid ? "yes" : "no")}  lines={_lines.Count}";
}