using System;

namespace InvoiceSystem
{
    public class OrderInfo
    {
        public DateTime OrderDate { get; }
        public string PaymentMethod { get; }
        public string Currency { get; }
        public decimal SubTotal { get; }
        public decimal DiscountAmount { get; }
        public decimal TaxAmount { get; }
        public decimal TotalAmount { get; }

        public OrderInfo(DateTime orderDate, string paymentMethod, string currency,
                         decimal subTotal, decimal discountAmount, decimal taxAmount)
        {
            if (orderDate == default)
                throw new ArgumentException("Order date is required.", nameof(orderDate));
            if (string.IsNullOrWhiteSpace(paymentMethod))
                throw new ArgumentException("Payment method is required.", nameof(paymentMethod));
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency is required.", nameof(currency));
            if (subTotal < 0)
                throw new ArgumentOutOfRangeException(nameof(subTotal), "SubTotal cannot be negative.");
            if (discountAmount < 0 || discountAmount > subTotal)
                throw new ArgumentOutOfRangeException(nameof(discountAmount), "Discount must be between 0 and SubTotal.");
            if (taxAmount < 0)
                throw new ArgumentOutOfRangeException(nameof(taxAmount), "Tax cannot be negative.");

            OrderDate = orderDate;
            PaymentMethod = paymentMethod;
            Currency = currency;
            SubTotal = subTotal;
            DiscountAmount = discountAmount;
            TaxAmount = taxAmount;
            TotalAmount = subTotal - discountAmount + taxAmount;   
        }
    }
}