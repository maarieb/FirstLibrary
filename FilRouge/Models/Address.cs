using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    internal class Address
    {
        public int Id { get; set; }
        public string Apartment { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public List<Reader> Readers { get; set; }

        public Address() {}

        public Address(string apartment, string street, string city, string zipCode, string country)
        {
            Apartment = apartment;
            Street = street;
            City = city;
            ZipCode = zipCode;
            Country = country;
            Readers = new List<Reader>();
        }

        public override string? ToString()
        {
            return $"{Apartment} {Street} {City} ({ZipCode}), {Country}";
        }
    }
}
