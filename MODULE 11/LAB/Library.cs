using System;

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

    public Librarian(int id, string name, string position)
    {
        Id = id;
        Name = name;
        Position = position;
    }

    public void AddBook(Book book)
    {
        Console.WriteLine($"Книга '{book.Title}' добавлена в библиотеку.");
    }

    public void RemoveBook(Book book)
    {
        Console.WriteLine($"Книга '{book.Title}' удалена из библиотеки.");
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

        Reader reader = new Reader(1, "Иван Иванов", "ivan@example.com");
        Librarian librarian = new Librarian(1, "Мария Петрова", "Главный библиотекарь");

        // Библиотекарь добавляет книги
        librarian.AddBook(book1);
        librarian.AddBook(book2);

        // Читатель берет книгу
        reader.BorrowBook(book1);

        // Создание записи о выдаче
        Loan loan = new Loan();
        loan.IssueLoan(book1, reader);

        // Читатель возвращает книгу
        reader.ReturnBook(book1);

        // Завершение записи о выдаче
        loan.CompleteLoan();

        // Читатель пытается взять книгу, которая уже возвращена
        reader.BorrowBook(book1);
    }
}
