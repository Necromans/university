using System;
using System.Collections.Generic;
using System.Linq;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public string Status { get; set; }

    public Book(string title, string author, string isbn)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        Status = "Available";  // Книга доступна по умолчанию
    }

    public void ChangeStatus(string newStatus)
    {
        Status = newStatus;
    }
}

public class Reader
{
    public string Name { get; set; }
    public List<Book> RentedBooks { get; set; } = new List<Book>();
    public int MaxRentableBooks { get; set; } = 3; // Максимум 3 книги

    public Reader(string name)
    {
        Name = name;
    }

    public bool CanRentBook()
    {
        return RentedBooks.Count < MaxRentableBooks;
    }

    public void RentBook(Book book)
    {
        if (CanRentBook() && book.Status == "Available")
        {
            RentedBooks.Add(book);
            book.ChangeStatus("Rented");
            Console.WriteLine($"{Name} арендовал книгу: {book.Title}");
        }
        else if (!CanRentBook())
        {
            Console.WriteLine($"{Name} не может арендовать больше {MaxRentableBooks} книг.");
        }
        else
        {
            Console.WriteLine($"{book.Title} уже арендована.");
        }
    }

    public void ReturnBook(Book book)
    {
        if (RentedBooks.Contains(book))
        {
            RentedBooks.Remove(book);
            book.ChangeStatus("Available");
            Console.WriteLine($"{Name} вернул книгу: {book.Title}");
        }
        else
        {
            Console.WriteLine($"{Name} не арендовал эту книгу: {book.Title}");
        }
    }
}

public class Librarian
{
    public string Name { get; set; }

    public Librarian(string name)
    {
        Name = name;
    }

    public void ManageBooks(Library library)
    {
        // Например, добавить книгу в библиотеку
        var newBook = new Book("Основы программирования", "Иванов И.И.", "1234567890");
        library.AddBook(newBook);
        Console.WriteLine($"Библиотекарь {Name} добавил книгу: {newBook.Title}");
    }

    public List<Book> SearchBooks(Library library, string searchTerm)
    {
        return library.Books.Where(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm)).ToList();
    }
}

public class Library
{
    public List<Book> Books { get; set; } = new List<Book>();

    public void DisplayAvailableBooks()
    {
        Console.WriteLine("Доступные книги:");
        foreach (var book in Books.Where(b => b.Status == "Available"))
        {
            Console.WriteLine($"{book.Title} - {book.Author}");
        }
    }

    public void AddBook(Book book)
    {
        Books.Add(book);
        Console.WriteLine($"Книга {book.Title} добавлена в библиотеку.");
    }

    public void RemoveBook(Book book)
    {
        Books.Remove(book);
        Console.WriteLine($"Книга {book.Title} удалена из библиотеки.");
    }
}

// Пример использования
class Program
{
    static void Main(string[] args)
    {
        var library = new Library();

        // Библиотекарь управляет библиотекой
        var librarian = new Librarian("Ольга");
        librarian.ManageBooks(library);

        // Читатель арендует книги
        var reader = new Reader("Алексей");
        var book1 = library.Books[0];
        reader.RentBook(book1);

        // Пытаемся арендовать еще книги
        var book2 = new Book("Математика", "Петров П.П.", "0987654321");
        var book3 = new Book("Физика", "Сидоров С.С.", "1122334455");
        var book4 = new Book("Химия", "Кузнецова К.К.", "2233445566");
        library.AddBook(book2);
        library.AddBook(book3);
        library.AddBook(book4);

        reader.RentBook(book2);  // Успешно арендует
        reader.RentBook(book3);  // Успешно арендует
        reader.RentBook(book4);  // Не сможет арендовать, превышено количество книг

        // Отображение доступных книг
        library.DisplayAvailableBooks();

        // Читатель возвращает книгу
        reader.ReturnBook(book1);

        // Отображение доступных книг
        library.DisplayAvailableBooks();
    }
}
