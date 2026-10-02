using System;

namespace InvoiceSystem
{
    public class InvoiceBuilder
    {
        private int _invoiceId;
        private string? _customerName;
        private string? _customerEmail;
        private string? _customerPhone;
        private Address? _billing;
        private Address? _shipping;
        private OrderInfo? _order;

        public InvoiceBuilder WithId(int id) { _invoiceId = id; return this; }

        public InvoiceBuilder WithCustomer(string name, string email, string? phone = null)
        {
            _customerName = name;
            _customerEmail = email;
            _customerPhone = phone;
            return this;
        }

        public InvoiceBuilder WithBillingAddress(Address address) { _billing = address; return this; }
        public InvoiceBuilder WithShippingAddress(Address address) { _shipping = address; return this; }

        public InvoiceBuilder WithSameShippingAsBilling() { _shipping = _billing; return this; }

        public InvoiceBuilder WithOrder(OrderInfo order) { _order = order; return this; }

        public Invoice Build()
        {
            if (_billing == null)
                throw new InvalidOperationException("Billing address is required.");
            if (_shipping == null)
                throw new InvalidOperationException("Shipping address is required.");
            if (_order == null)
                throw new InvalidOperationException("Order info is required.");

            return new Invoice(_invoiceId, _customerName!, _customerEmail!, _customerPhone,
                               _billing, _shipping, _order);
        }
    }
}