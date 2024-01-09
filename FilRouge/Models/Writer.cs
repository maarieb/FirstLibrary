using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    internal class Writer : Person
    {
        public string? Grade {  get; set; }
        public List<Book> Books { get; set; }

        public Writer() 
        {
            Books = new List<Book>();
            Email = "";
            Phone = "";
        }


        public Writer(string lastName, string firstName) : base(lastName, firstName)
        {
            Email = "";
            Phone = "";
            Books = new List<Book>();
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
