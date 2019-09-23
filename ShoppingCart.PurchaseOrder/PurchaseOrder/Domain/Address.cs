using System;
using Infrastructure.Core.Domain;

namespace ShoppingCart.ApplicationCore.PurchaseOrder.Domain
{
    public class Address  :BaseEntity
    {
        public String Street { get; private set; }

        public String City { get; private set; }

        public String County { get; private set; }

        public String Country { get; private set; }

        public String Postcode { get; private set; }

        private Address() { }

        public Address(string street, string city, string county, string country, string postcode)
        {
            Street = street;
            City = city;
            County = county;
            Country = country;
            Postcode = postcode;
        }
    }
}
