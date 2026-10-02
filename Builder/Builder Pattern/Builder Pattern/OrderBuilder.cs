using System;

namespace InvoiceSystem
{
    public class OrderBuilder
    {
        private DateTime _orderDate;
        private string? _paymentMethod;
        private string? _currency;
        private decimal _subTotal;
        private decimal _discountAmount;   
        private decimal _taxAmount;       

        public OrderBuilder WithOrderDate(DateTime date) { _orderDate = date; return this; }
        public OrderBuilder WithPaymentMethod(string method) { _paymentMethod = method; return this; }
        public OrderBuilder WithCurrency(string currency) { _currency = currency; return this; }
        public OrderBuilder WithSubTotal(decimal subTotal) { _subTotal = subTotal; return this; }
        public OrderBuilder WithDiscount(decimal discount) { _discountAmount = discount; return this; }
        public OrderBuilder WithTax(decimal tax) { _taxAmount = tax; return this; }

        public OrderInfo Build() =>
            new OrderInfo(_orderDate, _paymentMethod!, _currency!, _subTotal, _discountAmount, _taxAmount);
    }
}