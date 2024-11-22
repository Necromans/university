using System;

namespace HotelManagement
{
    // Подсистема для бронирования номеров
    public class RoomBookingSystem
    {
        public void BookRoom(string roomType)
        {
            Console.WriteLine($"Номер типа '{roomType}' успешно забронирован.");
        }

        public void CancelRoomBooking(string roomType)
        {
            Console.WriteLine($"Бронирование для номера типа '{roomType}' отменено.");
        }

        public bool IsRoomAvailable(string roomType)
        {
            Console.WriteLine($"Проверка доступности номера типа '{roomType}'...");
            return true; // Упрощено для примера
        }
    }

    // Подсистема для ресторана
    public class RestaurantSystem
    {
        public void BookTable(int numberOfGuests)
        {
            Console.WriteLine($"Столик на {numberOfGuests} человек успешно забронирован в ресторане.");
        }

        public void OrderFood(string foodItem)
        {
            Console.WriteLine($"Заказано блюдо: {foodItem}.");
        }
    }

    // Подсистема для мероприятий
    public class EventManagementSystem
    {
        public void BookConferenceRoom(string eventType)
        {
            Console.WriteLine($"Конференц-зал забронирован для мероприятия: {eventType}.");
        }

        public void OrderEquipment(string equipment)
        {
            Console.WriteLine($"Оборудование для мероприятия заказано: {equipment}.");
        }
    }

    // Подсистема для службы уборки
    public class CleaningService
    {
        public void ScheduleCleaning(string roomType)
        {
            Console.WriteLine($"Уборка для номера типа '{roomType}' запланирована.");
        }

        public void PerformCleaning(string roomType)
        {
            Console.WriteLine($"Уборка для номера типа '{roomType}' выполнена.");
        }
    }

    // Фасад, который объединяет все подсистемы
    public class HotelFacade
    {
        private readonly RoomBookingSystem _roomBookingSystem;
        private readonly RestaurantSystem _restaurantSystem;
        private readonly EventManagementSystem _eventManagementSystem;
        private readonly CleaningService _cleaningService;

        public HotelFacade()
        {
            _roomBookingSystem = new RoomBookingSystem();
            _restaurantSystem = new RestaurantSystem();
            _eventManagementSystem = new EventManagementSystem();
            _cleaningService = new CleaningService();
        }

        // Бронирование номера с услугами ресторана и уборки
        public void BookRoomWithServices(string roomType, int numberOfGuests, string foodItem)
        {
            if (_roomBookingSystem.IsRoomAvailable(roomType))
            {
                _roomBookingSystem.BookRoom(roomType);
                _restaurantSystem.BookTable(numberOfGuests);
                _restaurantSystem.OrderFood(foodItem);
                _cleaningService.ScheduleCleaning(roomType);
            }
            else
            {
                Console.WriteLine($"Номер типа '{roomType}' недоступен для бронирования.");
            }
        }

        // Организация мероприятия с бронированием номеров и оборудования
        public void OrganizeEvent(string eventType, string roomType, string equipment)
        {
            _eventManagementSystem.BookConferenceRoom(eventType);
            _roomBookingSystem.BookRoom(roomType);
            _eventManagementSystem.OrderEquipment(equipment);
        }

        // Бронирование стола с вызовом такси (упрощено)
        public void BookTableWithTaxi(int numberOfGuests)
        {
            _restaurantSystem.BookTable(numberOfGuests);
            Console.WriteLine("Такси заказано для гостей.");
        }

        // Метод для отмены бронирования номера
        public void CancelRoomBooking(string roomType)
        {
            _roomBookingSystem.CancelRoomBooking(roomType);
        }

        // Метод для организации уборки по запросу
        public void RequestCleaning(string roomType)
        {
            _cleaningService.PerformCleaning(roomType);
        }
    }

    // Основной класс с методом Main для демонстрации
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем фасад
            HotelFacade hotelFacade = new HotelFacade();

            // Бронирование номера с услугами ресторана и уборки
            Console.WriteLine("Бронирование номера с услугами ресторана и уборки:");
            hotelFacade.BookRoomWithServices("Стандартный", 2, "Паста с соусом Альфредо");

            Console.WriteLine("\n----------------------------------\n");

            // Организация мероприятия с бронированием номеров и оборудования
            Console.WriteLine("Организация мероприятия с бронированием номеров и оборудования:");
            hotelFacade.OrganizeEvent("Конференция", "Люкс", "Проекционное оборудование");

            Console.WriteLine("\n----------------------------------\n");

            // Бронирование стола в ресторане с вызовом такси
            Console.WriteLine("Бронирование стола в ресторане с вызовом такси:");
            hotelFacade.BookTableWithTaxi(4);

            Console.WriteLine("\n----------------------------------\n");

            // Отмена бронирования номера
            Console.WriteLine("Отмена бронирования номера:");
            hotelFacade.CancelRoomBooking("Стандартный");

            Console.WriteLine("\n----------------------------------\n");

            // Организация уборки по запросу
            Console.WriteLine("Организация уборки по запросу:");
            hotelFacade.RequestCleaning("Люкс");

            Console.ReadLine();
        }
    }
}
