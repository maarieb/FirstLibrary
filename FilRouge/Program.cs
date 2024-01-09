using Library.Configs;
using Library.Enums;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net;

int DisplayMenu()
{
    Console.WriteLine("GESTION DE BIBLIOTHEQUE");
    Console.WriteLine("MENU");
    Console.WriteLine("1 - ENREGISTRER UN NOUVEAU LIVRE");
    Console.WriteLine("2 - CHERCHER UN LIVRE");
    Console.WriteLine("3 - ENREGISTRER UN EMPRUNT");
    Console.WriteLine("4 - ENREGISTRER UN RETOUR");
    Console.WriteLine();
    Console.WriteLine("ENTRER VOTRE CHOIX");
    return int.Parse(Console.ReadLine()); 
}

void ManageUserSelection()
{
    var choix = DisplayMenu();
    switch (choix)
    {
        case 1:
            SaveBook();
            Console.WriteLine();
            break;
        case 2:
            FindBook();
            Console.WriteLine();
            break;
        case 3:
            SaveLoan();
            Console.WriteLine();
            break;
        case 4:
            EndLoan();
            Console.WriteLine();
            break;
        default:
            Console.WriteLine("Vous n'avez pas entré un numéro valide");
            Console.WriteLine();
            break;
    }
    ManageUserSelection();
}

void SaveBook()
{
    Book book = new Book();
    Console.WriteLine();
    Console.WriteLine("ENREGISTRER UN NOUVEAU LIVRE");

    Console.WriteLine("Entrer un Titre :");
    book.Title = Console.ReadLine();

    if (!BookExists(book.Title))
    {
        Console.WriteLine("Entrer une Description :");
        book.Description = Console.ReadLine();

        Console.WriteLine("Entrer un nombre de page :");
        book.PageNb = int.Parse(Console.ReadLine());

        using (ApplicationContext context = new ApplicationContext())
        {
            context.Books.Add(book);
            book.Writer = SaveWriter(book);
            if(book.Writer == null)
            {
                book.Writer = SaveWriter(book);
            }
            book.Domain = SaveDomain(book);
        }
        Console.WriteLine("Le livre a bien été enregistré !");
    } else
    {
        Console.WriteLine("Le livre existe déjà !");
    }
}

bool BookExists(string title)
{
    using (ApplicationContext context = new ApplicationContext())
    {
        Book? book = context.Books
            .FirstOrDefault(b => b.Title.Equals(title));

        if (book != null)
        {
            return true;

        }
        return false;
    }
}

Writer SaveWriter(Book book)
{
    using (ApplicationContext context = new ApplicationContext())
    {
        context.Persons
            .OfType<Writer>().ToList().ForEach(Console.WriteLine);
        Console.WriteLine();
        Console.WriteLine("1 - Sélectionner l'identifiant de l'auteur");
        Console.WriteLine("2 - Enregistrer un nouvel auteur");
        Console.WriteLine();
        Console.WriteLine("ENTRER VOTRE CHOIX");
        var choix = int.Parse(Console.ReadLine());
        switch (choix)
        {
            case 1:
                Console.WriteLine("Saisir l'identifiant :");
                int id = int.Parse(Console.ReadLine());
                return SelectWriter(id, book);
            case 2:
                Console.WriteLine("Entrer le nom de l'auteur :");
                string name = Console.ReadLine();
                Console.WriteLine("Entrer le prénom de l'auteur :");
                string firstName = Console.ReadLine();
                return CreateWriter(name, firstName, book);
            default:
                Console.WriteLine("Vous n'avez pas entré un numéro valide");
                return null;
        }
    }
}

Writer SelectWriter(int id, Book book)
{
    using (ApplicationContext context = new ApplicationContext())
    {
        Writer? writer = context.Persons
            .OfType<Writer>().ToList().Find(w => w.Id == id);
        writer.Books.Add(book);
        context.SaveChanges();
        return writer;
    }
}

Writer CreateWriter(string name, string firstName, Book book)
{
    using (ApplicationContext context = new ApplicationContext())
    {
        Writer newWriter = new Writer();
        newWriter.FirstName = firstName;
        newWriter.LastName = name;
        newWriter.Books.Add(book);
        context.Persons.Add(newWriter);
        context.SaveChanges();
        return newWriter;
    }   
}

Domain SaveDomain(Book book)
{
    using (ApplicationContext context = new ApplicationContext())
    {
        Console.WriteLine("Entrer le nom du domaine");
        var name = Console.ReadLine();

        Domain domain = context.Domains.FirstOrDefault(w => w.Name.Equals(name));
        if (domain != null)
        {
            if (domain.Books.IsNullOrEmpty())
            {
                domain.Books = new List<Book>() { book };
            } else
            {
                domain.Books.Add(book);
            }
            context.SaveChanges();
            return domain;
        }
        else
        {
            Domain newDomain = CreateDomain(name);
            context.Domains.Add(newDomain);
            context.SaveChanges();
            return newDomain;
        }
    }
}

Domain CreateDomain(string name)
{
    Console.WriteLine("Entrer une description du domaine");
    string description = Console.ReadLine();

    Domain newDomain = new Domain();
    newDomain.Name = name;
    newDomain.Description = description;
    newDomain.Books = new List<Book>();
    return newDomain;
}

void FindBook()
{
    Console.WriteLine("RECHERCHER UN LIVRE");
    Console.WriteLine("1 - Par son identifiant");
    Console.WriteLine("2 - Par son titre");
    int choix = int.Parse(Console.ReadLine());

    switch (choix)
    {
        case 1:
            Console.WriteLine("RECHERCHER UN LIVRE PAR SON IDENTIFIANT");
            Console.WriteLine("Entrer l'identifiant");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine(FindBookById(id));
            Console.WriteLine();
            break;
        case 2:
            Console.WriteLine("RECHERCHER UN LIVRE PAR SON TITRE");
            Console.WriteLine("Entrer le titre à rechercher");
            string search = Console.ReadLine();
            FindBookByTitle(search);
            Console.WriteLine();
            break;
        default:
            Console.WriteLine("Vous n'avez pas entré un numéro valide");
            Console.WriteLine();
            FindBook();
            break;
    }
}

Book FindBookById(int id)
{
    using (ApplicationContext context = new ApplicationContext())
    {
        Book? book = context.Books.Find(id);
        if (book != null)
        {
            return book;
        } else
        {
            Console.WriteLine("Aucun livre n'a été trouvé");
            return null;
        }
    }
}

void FindBookByTitle(string search)
{
    using (ApplicationContext context = new ApplicationContext())
    {
        var books = context.Books
            .Where(b => b.Title.Equals(search) || b.Title.Contains(search))
            .ToList();

        if (books != null)
        {
            books.ForEach(Console.WriteLine);
        }
        else
        {
            Console.WriteLine("Aucun livre n'a été trouvé");
        }
    }
}


void SaveLoan()
{
    Console.WriteLine("ENREGISTRER UN EMPRUNT");
    Console.WriteLine("Entrer l'identifiant du livre emprunté");
    int bookId = int.Parse(Console.ReadLine());
    Console.WriteLine("Entrer l'identifiant de l'emprunteur");
    int readerId = int.Parse(Console.ReadLine());

    using (ApplicationContext context = new ApplicationContext())
    {
        Book book = FindBookById(bookId);
        Reader reader = context.Persons.OfType<Reader>().First(r => r.Id == readerId);
        if (book != null && reader != null)
        {
            if (IsAvailable(book))
            {
                book.State = Library.Enums.EBookState.EMPRUNTE;
                Loan loan = new Loan();
                context.Loans.Add(loan);
                CreateLoan(loan, reader, book);
                book.Loans.Add(loan);
                reader.Loans.Add(loan);
                context.SaveChanges();
                Console.WriteLine("L'emprunt a bien été enregistré");
            } else
            {
                Console.WriteLine("Le livre n'est pas disponible à l'emprunt");
            }
        } else
        {
            Console.WriteLine("Impossible de créer l'emprunt");
        }
    }
}

void CreateLoan(Loan loan, Reader reader, Book book)
{
    loan.Book = book;
    loan.Reader = reader;
    loan.StartDate = DateTime.Now;
}

bool IsAvailable(Book book){
    if(book.State == EBookState.DISPONIBLE) 
    {
        return true; 
    }
    return false;
}

void EndLoan()
{
    Console.WriteLine("ENREGISTRER UN RETOUR");
    Console.WriteLine("Entrer l'identifiant de l'emprunt");
    int loanId = int.Parse(Console.ReadLine());

    using (ApplicationContext context = new ApplicationContext())
    {
        Loan? loan = context.Loans.Find(loanId);
        if (loan != null)
        {
            loan.EndDate = DateTime.Now;
            Book book = context.Books.ToList().Find(b => b.Id == loan.Book.Id);
            book.State = Library.Enums.EBookState.DISPONIBLE;
            context.SaveChanges();
            Console.WriteLine("Le retour a bien été enregistré");
        }
        else
        {
            Console.WriteLine("Impossible d'enregistrer le retour");
        }
    }
}

ManageUserSelection();

//using (ApplicationContext context = new ApplicationContext())
//{
//    Reader reader = new Reader("Pamures", "Pomme", "pamures@gmail.com", "0652364789", "pomme3000");
//    Address address = new Address("", "16 Rue des Fruits", "Versailles", "78000", "France");
//    reader.Address = address;
//    address.Readers.Add(reader);
//    context.Persons.Add(reader);
//    context.Addresses.Add(address);
//    context.SaveChanges();
//}