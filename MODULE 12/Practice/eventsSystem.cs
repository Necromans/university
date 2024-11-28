using System;
using System.Collections.Generic;
using System.Linq;

namespace EventBookingSystem
{
    // Класс Мероприятия
    class Event
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }

        public override string ToString()
        {
            return $"ID: {ID}, Название: {Title}, Дата: {Date.ToShortDateString()}, Место: {Location}";
        }
    }

    // Класс Пользователь
    class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Role { get; set; } // Guest, User, Admin

        public override string ToString()
        {
            return $"ID: {ID}, Имя: {Name}, Роль: {Role}";
        }
    }

    // Класс Бронирование
    class Booking
    {
        public int ID { get; set; }
        public User User { get; set; }
        public Event Event { get; set; }
        public string Status { get; set; } = "Активно";

        public override string ToString()
        {
            return $"ID: {ID}, Пользователь: {User.Name}, Мероприятие: {Event.Title}, Статус: {Status}";
        }
    }

    class Program
    {
        static List<Event> events = new List<Event>();
        static List<User> users = new List<User>();
        static List<Booking> bookings = new List<Booking>();

        static void Main(string[] args)
        {
            SeedData(); // Создание тестовых данных
            MainMenu();
        }

        static void SeedData()
        {
            // Добавление тестовых мероприятий
            events.Add(new Event { ID = 1, Title = "Концерт", Date = DateTime.Now.AddDays(5), Location = "Зал 1" });
            events.Add(new Event { ID = 2, Title = "Конференция", Date = DateTime.Now.AddDays(10), Location = "Зал 2" });

            // Добавление тестовых пользователей
            users.Add(new User { ID = 1, Name = "Гость", Role = "Guest" });
            users.Add(new User { ID = 2, Name = "Иван", Role = "User" });
            users.Add(new User { ID = 3, Name = "Админ", Role = "Admin" });
        }

        static void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("\nСистема бронирования мероприятий");
                Console.WriteLine("1. Войти как Гость");
                Console.WriteLine("2. Войти как Зарегистрированный пользователь");
                Console.WriteLine("3. Войти как Администратор");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите роль: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        GuestMenu();
                        break;
                    case "2":
                        UserMenu();
                        break;
                    case "3":
                        AdminMenu();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный ввод.");
                        break;
                }
            }
        }

        static void GuestMenu()
        {
            Console.WriteLine("\nПросмотр мероприятий:");
            foreach (var ev in events)
            {
                Console.WriteLine(ev);
            }
        }

        static void UserMenu()
        {
            Console.Write("\nВведите свое имя: ");
            string name = Console.ReadLine();
            var user = users.FirstOrDefault(u => u.Name == name && u.Role == "User");

            if (user == null)
            {
                Console.WriteLine("Пользователь не найден.");
                return;
            }

            while (true)
            {
                Console.WriteLine("\nМеню пользователя:");
                Console.WriteLine("1. Просмотреть мероприятия");
                Console.WriteLine("2. Забронировать мероприятие");
                Console.WriteLine("3. Отменить бронирование");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        foreach (var ev in events)
                        {
                            Console.WriteLine(ev);
                        }
                        break;
                    case "2":
                        BookEvent(user);
                        break;
                    case "3":
                        CancelBooking(user);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный ввод.");
                        break;
                }
            }
        }

        static void AdminMenu()
        {
            Console.Write("\nВведите свое имя: ");
            string name = Console.ReadLine();
            var admin = users.FirstOrDefault(u => u.Name == name && u.Role == "Admin");

            if (admin == null)
            {
                Console.WriteLine("Администратор не найден.");
                return;
            }

            while (true)
            {
                Console.WriteLine("\nМеню администратора:");
                Console.WriteLine("1. Просмотреть мероприятия");
                Console.WriteLine("2. Добавить мероприятие");
                Console.WriteLine("3. Редактировать мероприятие");
                Console.WriteLine("4. Удалить мероприятие");
                Console.WriteLine("5. Просмотреть все бронирования");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        foreach (var ev in events)
                        {
                            Console.WriteLine(ev);
                        }
                        break;
                    case "2":
                        AddEvent();
                        break;
                    case "3":
                        EditEvent();
                        break;
                    case "4":
                        DeleteEvent();
                        break;
                    case "5":
                        foreach (var booking in bookings)
                        {
                            Console.WriteLine(booking);
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный ввод.");
                        break;
                }
            }
        }

        static void BookEvent(User user)
        {
            Console.Write("\nВведите ID мероприятия: ");
            int eventId = int.Parse(Console.ReadLine());

            var ev = events.FirstOrDefault(e => e.ID == eventId);
            if (ev == null)
            {
                Console.WriteLine("Мероприятие не найдено.");
                return;
            }

            bookings.Add(new Booking { ID = bookings.Count + 1, User = user, Event = ev });
            Console.WriteLine("Бронирование успешно.");
        }

        static void CancelBooking(User user)
        {
            var userBookings = bookings.Where(b => b.User.ID == user.ID).ToList();

            if (!userBookings.Any())
            {
                Console.WriteLine("У вас нет бронирований.");
                return;
            }

            Console.WriteLine("Ваши бронирования:");
            foreach (var booking in userBookings)
            {
                Console.WriteLine(booking);
            }

            Console.Write("\nВведите ID бронирования для отмены: ");
            int bookingId = int.Parse(Console.ReadLine());

            var bookingToCancel = bookings.FirstOrDefault(b => b.ID == bookingId);
            if (bookingToCancel != null)
            {
                bookingToCancel.Status = "Отменено";
                Console.WriteLine("Бронирование отменено.");
            }
            else
            {
                Console.WriteLine("Бронирование не найдено.");
            }
        }

        static void AddEvent()
        {
            Console.Write("\nВведите название мероприятия: ");
            string title = Console.ReadLine();

            Console.Write("Введите дату мероприятия (гггг-мм-дд): ");
            DateTime date = DateTime.Parse(Console.ReadLine());

            Console.Write("Введите место проведения: ");
            string location = Console.ReadLine();

            events.Add(new Event { ID = events.Count + 1, Title = title, Date = date, Location = location });
            Console.WriteLine("Мероприятие добавлено.");
        }

        static void EditEvent()
        {
            Console.Write("\nВведите ID мероприятия для редактирования: ");
            int eventId = int.Parse(Console.ReadLine());

            var ev = events.FirstOrDefault(e => e.ID == eventId);
            if (ev == null)
            {
                Console.WriteLine("Мероприятие не найдено.");
                return;
            }

            Console.Write("Введите новое название мероприятия: ");
            ev.Title = Console.ReadLine();

            Console.Write("Введите новую дату мероприятия (гггг-мм-дд): ");
            ev.Date = DateTime.Parse(Console.ReadLine());

            Console.Write("Введите новое место проведения: ");
            ev.Location = Console.ReadLine();

            Console.WriteLine("Мероприятие обновлено.");
        }

        static void DeleteEvent()
        {
            Console.Write("\nВведите ID мероприятия для удаления: ");
            int eventId = int.Parse(Console.ReadLine());

            var ev = events.FirstOrDefault(e => e.ID == eventId);
            if (ev != null)
            {
                events.Remove(ev);
                Console.WriteLine("Мероприятие удалено.");
            }
            else
            {
                Console.WriteLine("Мероприятие не найдено.");
            }
        }
    }
}
