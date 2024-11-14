using System;
using System.Collections.Generic;

namespace HotelBookingSystem
{
    // Интерфейсы
    public interface IUserManagementService
    {
        bool Register(User user);
        bool Login(User user);
    }

    public interface IHotelService
    {
        List<Hotel> SearchHotel(string location, float rating);
    }

    public interface IBookingService
    {
        Booking CreateBooking(User user, Hotel hotel, Room room, DateTime checkInDate, DateTime checkOutDate);
        bool CancelBooking(Booking booking);
    }

    public interface IPaymentService
    {
        bool ProcessPayment(Payment payment);
    }

    public interface INotificationService
    {
        void SendNotification(User user, string message);
    }

    // Классы сущностей
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public float Rating { get; set; }
    }

    public class Room
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string RoomType { get; set; }
        public decimal Price { get; set; }
        public bool Availability { get; set; }
    }

    public class Booking
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Hotel Hotel { get; set; }
        public Room Room { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }

    public class Payment
    {
        public int Id { get; set; }
        public Booking Booking { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }

    // Реализация сервисов
    public class UserManagementService : IUserManagementService
    {
        private List<User> _users = new List<User>();

        public bool Register(User user)
        {
            _users.Add(user);
            Console.WriteLine($"Пользователь {user.Name} зарегистрирован.");
            return true;
        }

        public bool Login(User user)
        {
            Console.WriteLine($"Пользователь {user.Name} авторизован.");
            return true;
        }
    }

    public class HotelService : IHotelService
    {
        private List<Hotel> _hotels = new List<Hotel>
        {
            new Hotel { Id = 1, Name = "Hotel A", Location = "New York", Rating = 4.5f },
            new Hotel { Id = 2, Name = "Hotel B", Location = "Los Angeles", Rating = 4.2f }
        };

        public List<Hotel> SearchHotel(string location, float rating)
        {
            return _hotels.FindAll(h => h.Location.Contains(location) && h.Rating >= rating);
        }
    }

    public class BookingService : IBookingService
    {
        private List<Booking> _bookings = new List<Booking>();

        public Booking CreateBooking(User user, Hotel hotel, Room room, DateTime checkInDate, DateTime checkOutDate)
        {
            var booking = new Booking
            {
                Id = _bookings.Count + 1,
                User = user,
                Hotel = hotel,
                Room = room,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate
            };

            _bookings.Add(booking);
            Console.WriteLine($"Бронирование успешно для пользователя {user.Name} в отеле {hotel.Name}.");
            return booking;
        }

        public bool CancelBooking(Booking booking)
        {
            _bookings.Remove(booking);
            Console.WriteLine($"Бронирование отменено для пользователя {booking.User.Name}.");
            return true;
        }
    }

    public class PaymentService : IPaymentService
    {
        public bool ProcessPayment(Payment payment)
        {
            payment.Status = "Успешно";
            Console.WriteLine($"Платеж на сумму {payment.Amount} успешно обработан.");
            return true;
        }
    }

    public class NotificationService : INotificationService
    {
        public void SendNotification(User user, string message)
        {
            Console.WriteLine($"Уведомление для {user.Name}: {message}");
        }
    }

    // Главная программа
    class Program
    {
        static void Main(string[] args)
        {
            // Инициализация сервисов
            IUserManagementService userManagementService = new UserManagementService();
            IHotelService hotelService = new HotelService();
            IBookingService bookingService = new BookingService();
            IPaymentService paymentService = new PaymentService();
            INotificationService notificationService = new NotificationService();

            // Регистрация и авторизация
            var user = new User { Id = 1, Name = "Иван", Email = "ivan@example.com" };
            userManagementService.Register(user);
            userManagementService.Login(user);

            // Поиск отелей
            var hotels = hotelService.SearchHotel("New York", 4.0f);
            foreach (var hotel in hotels)
            {
                Console.WriteLine($"Найден отель: {hotel.Name} в {hotel.Location} с рейтингом {hotel.Rating}");
            }

            // Создание бронирования
            var room = new Room { Id = 1, HotelId = 1, RoomType = "Standard", Price = 150, Availability = true };
            var booking = bookingService.CreateBooking(user, hotels[0], room, DateTime.Now, DateTime.Now.AddDays(2));

            // Платеж
            var payment = new Payment { Id = 1, Booking = booking, Amount = room.Price, Status = "" };
            paymentService.ProcessPayment(payment);

            // Отправка уведомления
            notificationService.SendNotification(user, "Ваше бронирование подтверждено.");
        }
    }
}
