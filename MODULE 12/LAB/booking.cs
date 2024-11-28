using System;

namespace HotelBookingApp
{
    // Интерфейс состояния
    public interface IState
    {
        void SelectRoom(BookingContext context, string room);
        void ConfirmBooking(BookingContext context);
        void Pay(BookingContext context, decimal amount);
        void CancelBooking(BookingContext context);
    }

    // Состояние ожидания (Idle)
    public class IdleState : IState
    {
        public void SelectRoom(BookingContext context, string room)
        {
            Console.WriteLine($"Номер {room} выбран.");
            context.SetRoom(room);
            context.SetState(new RoomSelectedState());
        }

        public void ConfirmBooking(BookingContext context)
        {
            Console.WriteLine("Сначала выберите номер.");
        }

        public void Pay(BookingContext context, decimal amount)
        {
            Console.WriteLine("Сначала выберите номер и подтвердите бронирование.");
        }

        public void CancelBooking(BookingContext context)
        {
            Console.WriteLine("Нет активного бронирования для отмены.");
        }
    }

    // Состояние выбора номера (RoomSelected)
    public class RoomSelectedState : IState
    {
        public void SelectRoom(BookingContext context, string room)
        {
            Console.WriteLine($"Номер изменен на {room}.");
            context.SetRoom(room);
        }

        public void ConfirmBooking(BookingContext context)
        {
            Console.WriteLine($"Бронирование номера {context.Room} подтверждено.");
            context.SetState(new BookingConfirmedState());
        }

        public void Pay(BookingContext context, decimal amount)
        {
            Console.WriteLine("Сначала подтвердите бронирование.");
        }

        public void CancelBooking(BookingContext context)
        {
            Console.WriteLine("Бронирование отменено.");
            context.SetState(new BookingCancelledState());
        }
    }

    // Состояние подтвержденного бронирования (BookingConfirmed)
    public class BookingConfirmedState : IState
    {
        public void SelectRoom(BookingContext context, string room)
        {
            Console.WriteLine("Нельзя изменить номер после подтверждения бронирования.");
        }

        public void ConfirmBooking(BookingContext context)
        {
            Console.WriteLine("Бронирование уже подтверждено.");
        }

        public void Pay(BookingContext context, decimal amount)
        {
            if (amount >= context.RoomPrice)
            {
                Console.WriteLine($"Оплата {amount} принята. Бронирование завершено.");
                context.SetState(new PaidState());
            }
            else
            {
                Console.WriteLine($"Недостаточно средств. Внесите ещё {context.RoomPrice - amount}.");
            }
        }

        public void CancelBooking(BookingContext context)
        {
            Console.WriteLine("Бронирование отменено.");
            context.SetState(new BookingCancelledState());
        }
    }

    // Состояние оплаченного бронирования (Paid)
    public class PaidState : IState
    {
        public void SelectRoom(BookingContext context, string room)
        {
            Console.WriteLine("Нельзя изменить номер после оплаты.");
        }

        public void ConfirmBooking(BookingContext context)
        {
            Console.WriteLine("Бронирование уже завершено.");
        }

        public void Pay(BookingContext context, decimal amount)
        {
            Console.WriteLine("Бронирование уже оплачено.");
        }

        public void CancelBooking(BookingContext context)
        {
            Console.WriteLine("Нельзя отменить бронирование после оплаты.");
        }
    }

    // Состояние отмененного бронирования (BookingCancelled)
    public class BookingCancelledState : IState
    {
        public void SelectRoom(BookingContext context, string room)
        {
            Console.WriteLine("Бронирование отменено. Выберите новый номер.");
            context.SetState(new IdleState());
        }

        public void ConfirmBooking(BookingContext context)
        {
            Console.WriteLine("Бронирование отменено. Выберите новый номер.");
        }

        public void Pay(BookingContext context, decimal amount)
        {
            Console.WriteLine("Бронирование отменено. Выберите новый номер.");
        }

        public void CancelBooking(BookingContext context)
        {
            Console.WriteLine("Бронирование уже отменено.");
        }
    }

    // Контекст бронирования
    public class BookingContext
    {
        private IState _currentState;
        public string Room { get; private set; }
        public decimal RoomPrice { get; private set; } = 100m; // Цена за номер

        public BookingContext()
        {
            _currentState = new IdleState();
        }

        public void SetState(IState state)
        {
            _currentState = state;
        }

        public void SetRoom(string room)
        {
            Room = room;
        }

        public void SelectRoom(string room)
        {
            _currentState.SelectRoom(this, room);
        }

        public void ConfirmBooking()
        {
            _currentState.ConfirmBooking(this);
        }

        public void Pay(decimal amount)
        {
            _currentState.Pay(this, amount);
        }

        public void CancelBooking()
        {
            _currentState.CancelBooking(this);
        }
    }

    // Основной класс для демонстрации работы системы бронирования
    class Program
    {
        static void Main(string[] args)
        {
            BookingContext booking = new BookingContext();

            // Демонстрация процесса бронирования
            booking.SelectRoom("101");
            booking.ConfirmBooking();
            booking.Pay(50);  // Недостаточно средств
            booking.Pay(100); // Успешная оплата
            booking.CancelBooking(); // Попытка отменить после оплаты
        }
    }
}
