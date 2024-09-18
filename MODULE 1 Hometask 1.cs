using System;
using System.Collections.Generic;

// Класс Книга
class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int CopiesAvailable { get; set; }

    public Book(string title, string author, string isbn, int copiesAvailable)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        CopiesAvailable = copiesAvailable;
    }

    public override string ToString()
    {
        return $"Название: {Title}, Автор: {Author}, ISBN: {ISBN}, Доступно: {CopiesAvailable}";
    }
}

// Класс Читатель
class Reader
{
    public string Name { get; set; }
    public int ID { get; set; }

    public Reader(string name, int id)
    {
        Name = name;
        ID = id;
    }

    public override string ToString()
    {
        return $"Читатель: {Name}, ID: {ID}";
    }
}

// Класс Библиотека
class Library
{
    private List<Book> books = new List<Book>();
    private List<Reader> readers = new List<Reader>();

    // Добавление книги
    public void AddBook(Book book)
    {
        books.Add(book);
        Console.WriteLine($"Книга '{book.Title}' добавлена в библиотеку.");
    }

    // Удаление книги
    public void RemoveBook(Book book)
    {
        books.Remove(book);
        Console.WriteLine($"Книга '{book.Title}' удалена из библиотеки.");
    }

    // Регистрация читателя
    public void RegisterReader(Reader reader)
    {
        readers.Add(reader);
        Console.WriteLine($"Читатель {reader.Name} зарегистрирован.");
    }

    // Удаление читателя
    public void RemoveReader(Reader reader)
    {
        readers.Remove(reader);
        Console.WriteLine($"Читатель {reader.Name} удалён.");
    }

    // Выдача книги
    public void CheckoutBook(string isbn, Reader reader)
    {
        Book book = books.Find(b => b.ISBN == isbn);
        if (book != null && book.CopiesAvailable > 0)
        {
            book.CopiesAvailable--;
            Console.WriteLine($"Читатель {reader.Name} взял книгу '{book.Title}'.");
        }
        else
        {
            Console.WriteLine($"Книга с ISBN: {isbn} недоступна.");
        }
    }

    // Возврат книги
    public void ReturnBook(string isbn, Reader reader)
    {
        Book book = books.Find(b => b.ISBN == isbn);
        if (book != null)
        {
            book.CopiesAvailable++;
            Console.WriteLine($"Читатель {reader.Name} вернул книгу '{book.Title}'.");
        }
        else
        {
            Console.WriteLine($"Книга с ISBN: {isbn} не найдена.");
        }
    }

    // Отображение списка книг
    public void ShowBooks()
    {
        Console.WriteLine("\nСписок книг в библиотеке:");
        foreach (var book in books)
        {
            Console.WriteLine(book);
        }
    }
}


class Program
{
    static void Main(string[] args)
    {
        // Создание библиотеки
        Library library = new Library();

        // Создание книг
        Book book1 = new Book("1984", "Джордж Оруэлл", "1234567890", 2);
        Book book2 = new Book("Мастер и Маргарита", "Михаил Булгаков", "0987654321", 2);

        // Добавление книг в библиотеку
        library.AddBook(book1);
        library.AddBook(book2);

        // Создание читателей
        Reader reader1 = new Reader("Иван Иванов", 1);
        Reader reader2 = new Reader("Мария Петрова", 2);

        // Регистрация читателей
        library.RegisterReader(reader1);
        library.RegisterReader(reader2);

        // Отображение книг
        library.ShowBooks();

        // Выдача книги читателю
        library.CheckoutBook("1234567890", reader1);
        library.CheckoutBook("1234567890", reader2); // Второй экземпляр

        // Попытка взять книгу, когда экземпляры закончились
        library.CheckoutBook("1234567890", reader2); // Нет доступных экземпляров

        // Возврат книги
        library.ReturnBook("1234567890", reader1);

        // Отображение списка книг после операций
        library.ShowBooks();
    }
}
