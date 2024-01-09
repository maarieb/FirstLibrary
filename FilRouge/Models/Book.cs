using Library.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    internal class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PageNb { get; set; }
        public Domain? Domain { get; set; }
        public Writer? Writer { get; set; }
        public List<Loan> Loans { get; set; }
        public EBookState State { get; set; }

        public Book()
        {
            Loans = new List<Loan>();
            State = EBookState.DISPONIBLE;
        }

        public Book(string title, string description, int pageNb, Domain domain, Writer writer)
        {
            Title = title;
            Description = description;
            PageNb = pageNb;
            Domain = domain;
            Writer = writer;
            Loans = new List<Loan>();
            State = EBookState.DISPONIBLE;
        }

        public override string? ToString()
        {
            return $"{Title} : {Description} - {State}";
        }
    }
}
