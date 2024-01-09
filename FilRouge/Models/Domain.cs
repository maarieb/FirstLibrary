using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    internal class Domain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Book> Books { get; set; }

        public Domain() 
        {
            Books = new List<Book>();
        }

        public Domain(string name, string description)
        {
            Name = name;
            Description = description;
            Books = new List<Book>();
        }

        public override string? ToString()
        {
            return $"{Id} - {Name} : {Description}";
        }
    }
}
