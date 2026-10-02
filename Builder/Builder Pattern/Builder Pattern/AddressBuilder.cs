

using InvoiceSystem;

namespace Builder_Pattern
{
    internal class AddressBuilder
    {
        private string? _street;
        private string? _city;
        private string? _state;
        private string? _zipCode;
        private string? _country;

        public AddressBuilder WithStreet(string street) { _street = street; return this; }
        public AddressBuilder WithCity(string city) { _city = city; return this; }
        public AddressBuilder WithState(string state) { _state = state; return this; }
        public AddressBuilder WithZipCode(string zipCode) { _zipCode = zipCode; return this; }
        public AddressBuilder WithCountry(string country) { _country = country; return this; }

        public Address Build() =>
          new Address(_street!, _city!, _state!, _zipCode!, _country!);
    }
}
