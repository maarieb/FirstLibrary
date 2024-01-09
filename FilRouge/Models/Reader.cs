using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    internal class Reader : Person
    {
        public string Password { get; set; }
        public Address? Address { get; set; }
        public List<Loan> Loans { get; set; }

        public Reader() 
        {
            Loans = new List<Loan>();
        }

        public Reader(string lastName, string firstName, string email, string phone, string password) : base(lastName, firstName, email, phone)
        {
            Password = password;
            Loans = new List<Loan>();
        }

    }
}
