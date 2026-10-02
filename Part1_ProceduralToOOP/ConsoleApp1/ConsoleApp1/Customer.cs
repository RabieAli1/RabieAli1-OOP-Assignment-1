using System;

namespace ConsoleApp1;

public class Customer
{
    public int Id { get; }
    public string Name { get; }
    public string Email { get; }
    public string City { get; }
    public bool IsVip { get; }

    public Customer(int id, string name, string email, string city, bool isVip)
    {
        if (id < 0)
            throw new ArgumentException("Id cannot be less than zero.", nameof(id));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.", nameof(email));

        Id = id;
        Name = name;
        Email = email;
        City = city ?? string.Empty;
        IsVip = isVip;
    }

    public override string ToString()
    {
        return $"#{Id}  {Name}  <{Email}>  {City}  vip={(IsVip ? "yes" : "no")}";
    }
}