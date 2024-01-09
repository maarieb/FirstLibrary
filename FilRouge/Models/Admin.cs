using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    internal class Admin : Person
    {
        public string Password { get; set; }
        public Admin() { }
        public Admin(string lastName, string firstName, string email, string phone, string password) : base(lastName, firstName, email, phone)
        {
            Password = password;
        }
    }
}
