using System;

namespace LogisticsSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Система управления логистикой интернет-магазина ===\n");

            var orderProcessing = new OrderProcessing();
            if (!orderProcessing.ProcessOrder())
            {
                Console.WriteLine("Обработка заказа не завершена. Завершение работы.");
                return;
            }

            var paymentProcessing = new PaymentProcessing();
            if (!paymentProcessing.ProcessPayment())
            {
                Console.WriteLine("Оплата не удалась. Завершение работы.");
                return;
            }

            var orderFulfillment = new OrderFulfillment();
            orderFulfillment.FulfillOrder();

            Console.WriteLine("Процесс заказа завершен.");
        }
    }

    class OrderProcessing
    {
        public bool ProcessOrder()
        {
            Console.WriteLine("[1] Клиент создает заказ.");
            Console.WriteLine("Система проверяет наличие товаров на складе...");

            bool allItemsAvailable = new Random().Next(0, 2) == 1;
            if (allItemsAvailable)
            {
                Console.WriteLine("Все товары в наличии. Заказ подтвержден.");
                return true;
            }
            else
            {
                Console.WriteLine("Некоторые товары недоступны. Клиент получает уведомление.");
                return false;
            }
        }
    }

    class PaymentProcessing
    {
        public bool ProcessPayment()
        {
            Console.WriteLine("\n[2] Клиент выбирает способ оплаты.");
            Console.WriteLine("Система проверяет платеж...");

            bool paymentSuccessful = new Random().Next(0, 2) == 1;
            if (paymentSuccessful)
            {
                Console.WriteLine("Оплата успешна. Заказ переходит к следующему этапу.");
                return true;
            }
            else
            {
                Console.WriteLine("Оплата отклонена. Клиент получает уведомление.");
                return false;
            }
        }
    }

    class OrderFulfillment
    {
        public void FulfillOrder()
        {
            Console.WriteLine("\n[3] Склад начинает сборку заказа.");

            string[] tasks = { "Сборка товаров", "Упаковка заказа" };

            foreach (var task in tasks)
            {
                Console.WriteLine($"- Выполняется: {task}.");
            }

            Console.WriteLine("Сборка заказа завершена.");
            Console.WriteLine("Система уведомляет клиента о готовности заказа.");
        }
    }
}
