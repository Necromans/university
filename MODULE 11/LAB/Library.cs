using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public bool IsAvailable { get; private set; } = true;

    public Book(string title, string author, string isbn)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
    }

    public void MarkAsLoaned()
    {
        IsAvailable = false;
        Console.WriteLine($"Книга '{Title}' помечена как выданная.");
    }

    public void MarkAsAvailable()
    {
        IsAvailable = true;
        Console.WriteLine($"Книга '{Title}' помечена как доступная.");
    }
}

public class Reader
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public Reader(int id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public void BorrowBook(Book book)
    {
        if (book.IsAvailable)
        {
            book.MarkAsLoaned();
            Console.WriteLine($"{Name} взял(а) книгу '{book.Title}'.");
        }
        else
        {
            Console.WriteLine($"Книга '{book.Title}' в данный момент недоступна.");
        }
    }

    public void ReturnBook(Book book)
    {
        book.MarkAsAvailable();
        Console.WriteLine($"{Name} вернул(а) книгу '{book.Title}'.");
    }
}

public class Librarian
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }

    public List<Book> Books { get; set; } = new List<Book>();
    public List<Reader> Readers { get; set; } = new List<Reader>();

    public Librarian(int id, string name, string position)
    {
        Id = id;
        Name = name;
        Position = position;
    }

    // Добавление книги
    public void AddBook(Book book)
    {
        Books.Add(book);
        Console.WriteLine($"Книга '{book.Title}' добавлена в библиотеку.");
    }

    // Удаление книги
    public void RemoveBook(Book book)
    {
        Books.Remove(book);
        Console.WriteLine($"Книга '{book.Title}' удалена из библиотеки.");
    }

    // Добавление читателя
    public void AddReader(Reader reader)
    {
        Readers.Add(reader);
        Console.WriteLine($"Читатель '{reader.Name}' добавлен в библиотеку.");
    }

    // Удаление читателя
    public void RemoveReader(Reader reader)
    {
        Readers.Remove(reader);
        Console.WriteLine($"Читатель '{reader.Name}' удален из библиотеки.");
    }

    // Поиск книги по автору или названию
    public List<Book> SearchBooks(string query)
    {
        return Books.Where(b => b.Title.Contains(query, StringComparison.OrdinalIgnoreCase) || b.Author.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    // Получение отчета о доступных книгах
    public void ReportAvailableBooks()
    {
        Console.WriteLine("Доступные книги:");
        foreach (var book in Books.Where(b => b.IsAvailable))
        {
            Console.WriteLine($"- {book.Title} ({book.Author})");
        }
    }

    // Получение отчета о занятых книгах
    public void ReportLoanedBooks()
    {
        Console.WriteLine("Занятые книги:");
        foreach (var book in Books.Where(b => !b.IsAvailable))
        {
            Console.WriteLine($"- {book.Title} ({book.Author})");
        }
    }

    // Сохранение данных о книгах и читателях в файл
    public void SaveDataToFile()
    {
        using (StreamWriter sw = new StreamWriter("library_data.txt"))
        {
            sw.WriteLine("Книги:");
            foreach (var book in Books)
            {
                sw.WriteLine($"{book.Title}, {book.Author}, {book.ISBN}, {(book.IsAvailable ? "Доступна" : "Занята")}");
            }

            sw.WriteLine("\nЧитатели:");
            foreach (var reader in Readers)
            {
                sw.WriteLine($"{reader.Name}, {reader.Email}");
            }
        }
        Console.WriteLine("Данные о книгах и читателях сохранены в файл 'library_data.txt'.");
    }
}

public class Loan
{
    public Book Book { get; set; }
    public Reader Reader { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public void IssueLoan(Book book, Reader reader)
    {
        Book = book;
        Reader = reader;
        LoanDate = DateTime.Now;
        book.MarkAsLoaned();
        Console.WriteLine($"{Reader.Name} оформил(а) выдачу книги '{Book.Title}' {LoanDate}.");
    }

    public void CompleteLoan()
    {
        ReturnDate = DateTime.Now;
        Book.MarkAsAvailable();
        Console.WriteLine($"Выдача книги '{Book.Title}' завершена {ReturnDate}.");
    }
}

// Пример работы программы
class Program
{
    static void Main()
    {
        // Создание объектов
        Book book1 = new Book("Война и мир", "Лев Толстой", "123-456-789");
        Book book2 = new Book("Преступление и наказание", "Федор Достоевский", "987-654-321");
        Book book3 = new Book("Анна Каренина", "Лев Толстой", "555-444-333");

        Reader reader = new Reader(1, "Иван Иванов", "ivan@example.com");
        Reader reader2 = new Reader(2, "Мария Смирнова", "maria@example.com");

        Librarian librarian = new Librarian(1, "Мария Петрова", "Главный библиотекарь");

        // Библиотекарь добавляет книги
        librarian.AddBook(book1);
        librarian.AddBook(book2);
        librarian.AddBook(book3);

        // Библиотекарь добавляет читателей
        librarian.AddReader(reader);
        librarian.AddReader(reader2);

        // Читатель берет книгу
        reader.BorrowBook(book1);

        // Создание записи о выдаче
        Loan loan = new Loan();
        loan.IssueLoan(book1, reader);

        // Библиотекарь получает отчеты
        librarian.ReportAvailableBooks();
        librarian.ReportLoanedBooks();

        // Библиотекарь ищет книгу по автору
        var foundBooks = librarian.SearchBooks("Толстой");
        Console.WriteLine("Найденные книги:");
        foreach (var book in foundBooks)
        {
            Console.WriteLine($"- {book.Title} ({book.Author})");
        }

        // Читатель возвращает книгу
        reader.ReturnBook(book1);

        // Завершение записи о выдаче
        loan.CompleteLoan();

        // Сохранение данных
        librarian.SaveDataToFile();
    }
}
