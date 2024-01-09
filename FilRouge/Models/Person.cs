using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    internal abstract class Person
    {
        public int Id { get; set; }
        public string LastName { get; set;}
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Person(){}

        protected Person(string lastName, string firstName)
        {
            LastName = lastName;
            FirstName = firstName;
        }

        public Person(string lastName, string firstName, string email, string phone)
        {
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            Phone = phone;
        }

        public override string? ToString()
        {
            return $"{Id} : {LastName} {FirstName} ({Email}, {Phone})";
        }
    }
}
