using System;
using System.Collections.Generic;

namespace LibrarySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем библиотеку
            var library = new Library();

            // Создаем книги и авторов
            var author1 = new Author { Name = "John Smith" };
            var book1 = new Book { Title = "Программирование на C#", ISBN = "123-456", AvailabilityStatus = true };
            var book2 = new Book { Title = "Изучаем .NET", ISBN = "789-012", AvailabilityStatus = true };

            // Добавляем книги в библиотеку
            library.AddBook(book1);
            library.AddBook(book2);

            // Регистрация пользователей
            var reader = new Reader { Id = 1, Name = "Иван Иванов", Email = "ivan.ivanov@example.com" };
            var librarian = new Librarian { Id = 2, Name = "Анна Смирнова", Email = "anna.smirnova@example.com" };

            library.RegisterUser(reader);
            library.RegisterUser(librarian);

            // Чтение информации о книгах
            Console.WriteLine("Книги в библиотеке:");
            foreach (var book in library.Books)
            {
                Console.WriteLine(book.GetBookInfo());
            }

            // Читатель берет книгу
            Console.WriteLine("\nИван Иванов берет книгу 'Программирование на C#'...");
            reader.BorrowBook(book1);

            // Проверим доступность книги после выдачи
            Console.WriteLine("\nДоступность книги после выдачи:");
            Console.WriteLine(book1.GetBookInfo());

            // Читатель возвращает книгу
            Console.WriteLine("\nИван Иванов возвращает книгу 'Программирование на C#'...");
            reader.ReturnBook(book1);

            // Проверим доступность книги после возврата
            Console.WriteLine("\nДоступность книги после возврата:");
            Console.WriteLine(book1.GetBookInfo());

            // Генерация отчета
            Console.WriteLine("\nГенерация отчета по библиотеке...");
            var report = new Report();
            report.GenerateBookPopularityReport();
            report.GenerateUserActivityReport();

            Console.WriteLine("\nОперации с библиотечной системой завершены.");
        }
    }

    // Определение классов

    public class Book
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public bool AvailabilityStatus { get; set; }

        public string GetBookInfo()
        {
            return $"{Title} (ISBN: {ISBN}) - Доступна: {AvailabilityStatus}";
        }

        public void ChangeAvailabilityStatus(bool status)
        {
            AvailabilityStatus = status;
        }
    }

    public class Author
    {
        public string Name { get; set; }

        public string GetAuthorInfo()
        {
            return Name;
        }
    }

    public enum UserType
    {
        Reader,
        Librarian
    }

    public abstract class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }

        public void Register() => Console.WriteLine($"{Name} зарегистрирован.");
        public void Login() => Console.WriteLine($"{Name} вошел в систему.");
    }

    public class Reader : User
    {
        public Reader()
        {
            UserType = UserType.Reader;
        }

        public void BorrowBook(Book book)
        {
            if (book.AvailabilityStatus)
            {
                book.ChangeAvailabilityStatus(false);
                Console.WriteLine($"{Name} взял книгу: {book.Title}");
            }
            else
            {
                Console.WriteLine($"{book.Title} в данный момент недоступна.");
            }
        }

        public void ReturnBook(Book book)
        {
            book.ChangeAvailabilityStatus(true);
            Console.WriteLine($"{Name} вернул книгу: {book.Title}");
        }
    }

    public class Librarian : User
    {
        public Librarian()
        {
            UserType = UserType.Librarian;
        }

        public void AddBook(Book book, Library library)
        {
            library.AddBook(book);
            Console.WriteLine($"Книга добавлена библиотекарем {Name}: {book.Title}");
        }

        public void RemoveBook(Book book, Library library)
        {
            library.RemoveBook(book);
            Console.WriteLine($"Книга удалена библиотекарем {Name}: {book.Title}");
        }
    }

    public class Loan
    {
        public Book Book { get; set; }
        public Reader Reader { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }

        public void IssueLoan()
        {
            LoanDate = DateTime.Now;
            Console.WriteLine($"Выдача книги {Book.Title} читателю {Reader.Name}.");
        }

        public void ReturnBook()
        {
            ReturnDate = DateTime.Now;
            Console.WriteLine($"Книга {Book.Title} возвращена читателем {Reader.Name}.");
        }
    }

    public class Library
    {
        public List<Book> Books { get; private set; } = new List<Book>();
        public List<User> Users { get; private set; } = new List<User>();

        public void AddBook(Book book)
        {
            Books.Add(book);
        }

        public void RemoveBook(Book book)
        {
            Books.Remove(book);
        }

        public void RegisterUser(User user)
        {
            Users.Add(user);
        }

        public void GenerateReport()
        {
            Console.WriteLine("Отчет по библиотеке сгенерирован.");
        }
    }

    public class Report
    {
        public void GenerateBookPopularityReport()
        {
            Console.WriteLine("Генерация отчета о популярности книг...");
        }

        public void GenerateUserActivityReport()
        {
            Console.WriteLine("Генерация отчета об активности пользователей...");
        }
    }
}
