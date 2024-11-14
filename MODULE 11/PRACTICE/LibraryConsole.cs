using System;
using System.Collections.Generic;

namespace LibrarySystem
{
    // Класс для книги
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string ISBN { get; set; }

        public Book(string title, string author, string genre, string isbn)
        {
            Title = title;
            Author = author;
            Genre = genre;
            ISBN = isbn;
        }

        public override string ToString()
        {
            return $"Название: {Title}, Автор: {Author}, Жанр: {Genre}, ISBN: {ISBN}";
        }
    }

    // Класс для читателя
    public class Reader
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TicketNumber { get; set; }

        public Reader(string firstName, string lastName, int ticketNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            TicketNumber = ticketNumber;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, Номер билета: {TicketNumber}";
        }
    }

    // Класс для библиотекаря
    public class Librarian
    {
        public string Name { get; set; }

        public Librarian(string name)
        {
            Name = name;
        }

        public void IssueBook(Reader reader, Book book, AccountSystem accountSystem)
        {
            accountSystem.RecordIssue(reader, book);
            Console.WriteLine($"Библиотекарь {Name} выдал книгу '{book.Title}' читателю {reader.FirstName}.");
        }

        public void ReturnBook(Reader reader, Book book, AccountSystem accountSystem)
        {
            accountSystem.RecordReturn(reader, book);
            Console.WriteLine($"Библиотекарь {Name} принял книгу '{book.Title}' от читателя {reader.FirstName}.");
        }
    }

    // Класс для каталога
    public class Catalog
    {
        private List<Book> books = new List<Book>();

        public void AddBook(Book book)
        {
            books.Add(book);
            Console.WriteLine($"Книга '{book.Title}' добавлена в каталог.");
        }

        public Book SearchBookByTitle(string title)
        {
            return books.Find(book => book.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        public List<Book> SearchBooksByAuthor(string author)
        {
            return books.FindAll(book => book.Author.Equals(author, StringComparison.OrdinalIgnoreCase));
        }

        public Book FindBookByISBN(string isbn)
        {
            return books.Find(book => book.ISBN.Equals(isbn, StringComparison.OrdinalIgnoreCase));
        }
    }

    // Интерфейс для учетной системы
    public interface IAccountSystem
    {
        void RecordIssue(Reader reader, Book book);
        void RecordReturn(Reader reader, Book book);
        void ShowIssuedBooks();
    }

    // Учетная система
    public class AccountSystem : IAccountSystem
    {
        private Dictionary<Reader, List<Book>> issuedBooks = new Dictionary<Reader, List<Book>>();

        public void RecordIssue(Reader reader, Book book)
        {
            if (!issuedBooks.ContainsKey(reader))
            {
                issuedBooks[reader] = new List<Book>();
            }

            issuedBooks[reader].Add(book);
        }

        public void RecordReturn(Reader reader, Book book)
        {
            if (issuedBooks.ContainsKey(reader))
            {
                issuedBooks[reader].Remove(book);
            }
        }

        public void ShowIssuedBooks()
        {
            Console.WriteLine("Список выданных книг:");
            foreach (var entry in issuedBooks)
            {
                Console.WriteLine($"{entry.Key.FirstName} {entry.Key.LastName}:");
                foreach (var book in entry.Value)
                {
                    Console.WriteLine($"  - {book.Title}");
                }
            }
        }
    }

    // Основной класс для тестирования
    public class Program
    {
        public static void Main(string[] args)
        {
            // Создание экземпляров
            var catalog = new Catalog();
            var accountSystem = new AccountSystem();
            var librarian = new Librarian("Иван Иванов");

            // Добавление книг в каталог
            var book1 = new Book("Война и мир", "Лев Толстой", "Роман", "123456789");
            var book2 = new Book("Преступление и наказание", "Федор Достоевский", "Роман", "987654321");

            catalog.AddBook(book1);
            catalog.AddBook(book2);

            // Создание читателя
            var reader = new Reader("Петр", "Петров", 123);

            // Библиотекарь выдает книгу
            librarian.IssueBook(reader, book1, accountSystem);

            // Показать выданные книги
            accountSystem.ShowIssuedBooks();

            // Библиотекарь возвращает книгу
            librarian.ReturnBook(reader, book1, accountSystem);

            // Показать выданные книги после возврата
            accountSystem.ShowIssuedBooks();
        }
    }
}
