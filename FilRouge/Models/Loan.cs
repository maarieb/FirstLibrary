using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    internal class Loan
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Book? Book { get; set; }
        public Reader? Reader { get; set; }

        public Loan(){}

        public Loan(Book book, Reader reader)
        {
            StartDate = DateTime.Now;
            Book = book;
            Reader = reader;
        }
        public override string? ToString()
        {
            return $"{StartDate} - {EndDate} - {Book} - {Reader}";
        }
    }
}
