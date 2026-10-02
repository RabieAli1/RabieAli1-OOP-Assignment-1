using System;

namespace InvoiceSystem
{
    public class Invoice
    {
        public int InvoiceId { get; }
        public string CustomerName { get; }
        public string CustomerEmail { get; }
        public string? CustomerPhone { get; }    
        public Address BillingAddress { get; }
        public Address ShippingAddress { get; }
        public OrderInfo Order { get; }

        internal Invoice(int invoiceId, string customerName, string customerEmail, string? customerPhone,
                         Address billing, Address shipping, OrderInfo order)
        {
            if (invoiceId <= 0)
                throw new ArgumentOutOfRangeException(nameof(invoiceId), "Invoice id must be positive.");
            if (string.IsNullOrWhiteSpace(customerName))
                throw new ArgumentException("Customer name is required.", nameof(customerName));
            if (string.IsNullOrWhiteSpace(customerEmail))
                throw new ArgumentException("Customer email is required.", nameof(customerEmail));
            if (billing == null) throw new ArgumentNullException(nameof(billing));
            if (shipping == null) throw new ArgumentNullException(nameof(shipping));
            if (order == null) throw new ArgumentNullException(nameof(order));

            InvoiceId = invoiceId;
            CustomerName = customerName;
            CustomerEmail = customerEmail;
            CustomerPhone = customerPhone;
            BillingAddress = billing;
            ShippingAddress = shipping;
            Order = order;
        }
    }
}