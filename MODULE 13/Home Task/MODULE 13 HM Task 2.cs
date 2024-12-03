using System;

namespace EventBookingMonolith
{
    

    class BookingRequest
    {
        public bool RequestVenue()
        {
            Console.WriteLine("[1] Клиент запрашивает доступность площадки.");
            Console.WriteLine("Система проверяет доступность...");

            bool isAvailable = new Random().Next(0, 2) == 1;
            if (isAvailable)
            {
                Console.WriteLine("Площадка доступна. Клиент получает информацию о стоимости.");
                return true;
            }
            else
            {
                Console.WriteLine("Площадка недоступна. Клиенту предлагается выбрать другую дату.");
                return false;
            }
        }
    }

    class PaymentProcessor
    {
        public bool ProcessPayment()
        {
            Console.WriteLine("\n[2] Клиент подтверждает бронирование.");
            Console.WriteLine("Система запрашивает предоплату...");

            bool paymentSuccess = new Random().Next(0, 2) == 1;
            if (paymentSuccess)
            {
                Console.WriteLine("Оплата успешна. Бронирование подтверждено.");
                Console.WriteLine("Система уведомляет администратора площадки.");
                return true;
            }
            else
            {
                Console.WriteLine("Оплата отклонена. Клиенту предлагается повторить оплату.");
                return false;
            }
        }
    }

    class EventOrganizer
    {
        public void Organize()
        {
            Console.WriteLine("\n[3] Администратор площадки подготавливает задачи для подрядчиков.");

            string[] tasks = { "Декорации", "Кейтеринг", "Аренда оборудования" };

            foreach (var task in tasks)
            {
                Console.WriteLine($"- Уведомление подрядчику о задаче: {task}.");
                Console.WriteLine($"- Подрядчик подтверждает выполнение задачи: {task}.");
            }

            Console.WriteLine("Все задачи выполнены.");
        }
    }

    class FeedbackCollector
    {
        public void CollectFeedback()
        {
            Console.WriteLine("\n[4] Мероприятие завершено.");
            Console.WriteLine("Система отправляет запрос на отзыв клиенту...");

            string feedback = "Все прошло отлично! Спасибо!";
            Console.WriteLine($"Отзыв клиента: {feedback}");

            Console.WriteLine("Система отправляет отчет менеджеру.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Система управления бронированием мероприятий ===\n");

            var bookingRequest = new BookingRequest();
            if (!bookingRequest.RequestVenue())
            {
                Console.WriteLine("Бронирование отклонено. Завершение работы.");
                return;
            }

            var paymentProcessor = new PaymentProcessor();
            if (!paymentProcessor.ProcessPayment())
            {
                Console.WriteLine("Оплата не удалась. Завершение работы.");
                return;
            }

            var eventOrganizer = new EventOrganizer();
            eventOrganizer.Organize();

            var feedbackCollector = new FeedbackCollector();
            feedbackCollector.CollectFeedback();

            Console.WriteLine("Процесс бронирования завершен.");
        }
    }
}
