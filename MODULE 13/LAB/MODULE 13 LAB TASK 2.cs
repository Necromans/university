using System;

namespace CourseManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Система управления онлайн-курсами ===\n");

            var courseRegistration = new CourseRegistration();
            if (!courseRegistration.RegisterForCourse())
            {
                Console.WriteLine("Регистрация на курс не завершена. Завершение работы.");
                return;
            }

            var paymentProcessing = new PaymentProcessing();
            if (!paymentProcessing.ProcessPayment())
            {
                Console.WriteLine("Оплата курса не удалась. Завершение работы.");
                return;
            }

            var courseCompletion = new CourseCompletion();
            if (!courseCompletion.CompleteCourse())
            {
                Console.WriteLine("Курс не завершен. Завершение работы.");
                return;
            }

            Console.WriteLine("Процесс обучения завершен. Пользователь получает сертификат.");
        }
    }

    class CourseRegistration
    {
        public bool RegisterForCourse()
        {
            Console.WriteLine("[1] Пользователь выбирает курс.");
            Console.WriteLine("Система проверяет доступность курса...");

            bool isCourseAvailable = new Random().Next(0, 2) == 1;
            if (isCourseAvailable)
            {
                Console.WriteLine("Курс доступен. Пользователь добавлен в список участников.");
                return true;
            }
            else
            {
                Console.WriteLine("Курс недоступен. Пользователь получает уведомление.");
                return false;
            }
        }
    }

    class PaymentProcessing
    {
        public bool ProcessPayment()
        {
            Console.WriteLine("\n[2] Пользователь выбирает способ оплаты.");
            Console.WriteLine("Система проверяет платеж...");

            bool paymentSuccessful = new Random().Next(0, 2) == 1;
            if (paymentSuccessful)
            {
                Console.WriteLine("Оплата успешна. Доступ к курсу активирован.");
                return true;
            }
            else
            {
                Console.WriteLine("Оплата отклонена. Пользователь получает уведомление.");
                return false;
            }
        }
    }

    class CourseCompletion
    {
        public bool CompleteCourse()
        {
            Console.WriteLine("\n[3] Пользователь начинает обучение.");
            Console.WriteLine("Система отслеживает выполнение заданий...");

            bool allTasksCompleted = new Random().Next(0, 2) == 1;
            if (allTasksCompleted)
            {
                Console.WriteLine("Все задания выполнены. Пользователь получает сертификат.");
                return true;
            }
            else
            {
                Console.WriteLine("Некоторые задания не выполнены. Пользователь получает уведомление.");
                return false;
            }
        }
    }
}
