using System;

namespace LogisticsSystem
{
    // Интерфейс для внутренней службы доставки
    public interface IInternalDeliveryService
    {
        void DeliverOrder(string orderId);
        string GetDeliveryStatus(string orderId);
        decimal CalculateDeliveryCost(string orderId);
    }

    // Внутренняя служба доставки
    public class InternalDeliveryService : IInternalDeliveryService
    {
        public void DeliverOrder(string orderId)
        {
            Console.WriteLine($"Заказ {orderId} доставляется внутренней службой.");
        }

        public string GetDeliveryStatus(string orderId)
        {
            return $"Статус заказа {orderId}: В процессе доставки.";
        }

        public decimal CalculateDeliveryCost(string orderId)
        {
            return 10.0m; // Примерная стоимость доставки
        }
    }

    // Сторонние логистические службы

    // Сторонняя служба A
    public class ExternalLogisticsServiceA
    {
        public void ShipItem(int itemId)
        {
            Console.WriteLine($"Товар {itemId} отправлен через службу логистики A.");
        }

        public string TrackShipment(int shipmentId)
        {
            return $"Статус отправки {shipmentId}: В пути (служба A).";
        }

        public decimal GetShippingCost(int itemId)
        {
            return 15.0m; // Стоимость доставки
        }
    }

    // Сторонняя служба B
    public class ExternalLogisticsServiceB
    {
        public void SendPackage(string packageInfo)
        {
            Console.WriteLine($"Посылка {packageInfo} отправлена через службу логистики B.");
        }

        public string CheckPackageStatus(string trackingCode)
        {
            return $"Статус посылки {trackingCode}: Доставлена (служба B).";
        }

        public decimal CalculateShippingCost(string packageInfo)
        {
            return 20.0m; // Стоимость доставки
        }
    }

    // Сторонняя служба C
    public class ExternalLogisticsServiceC
    {
        public void DispatchPackage(string orderInfo)
        {
            Console.WriteLine($"Посылка {orderInfo} отправлена через службу логистики C.");
        }

        public string VerifyPackageStatus(string trackingNumber)
        {
            return $"Статус посылки {trackingNumber}: Доставлена (служба C).";
        }

        public decimal EstimateShippingCost(string orderInfo)
        {
            return 18.0m; // Стоимость доставки
        }
    }

    // Адаптеры для сторонних служб

    // Адаптер для службы A
    public class LogisticsAdapterA : IInternalDeliveryService
    {
        private readonly ExternalLogisticsServiceA _externalServiceA;

        public LogisticsAdapterA(ExternalLogisticsServiceA externalServiceA)
        {
            _externalServiceA = externalServiceA;
        }

        public void DeliverOrder(string orderId)
        {
            int itemId = int.Parse(orderId);  // Преобразуем OrderId в ItemId
            _externalServiceA.ShipItem(itemId);
        }

        public string GetDeliveryStatus(string orderId)
        {
            int shipmentId = int.Parse(orderId);
            return _externalServiceA.TrackShipment(shipmentId);
        }

        public decimal CalculateDeliveryCost(string orderId)
        {
            int itemId = int.Parse(orderId);
            return _externalServiceA.GetShippingCost(itemId);
        }
    }

    // Адаптер для службы B
    public class LogisticsAdapterB : IInternalDeliveryService
    {
        private readonly ExternalLogisticsServiceB _externalServiceB;

        public LogisticsAdapterB(ExternalLogisticsServiceB externalServiceB)
        {
            _externalServiceB = externalServiceB;
        }

        public void DeliverOrder(string orderId)
        {
            _externalServiceB.SendPackage(orderId);
        }

        public string GetDeliveryStatus(string orderId)
        {
            return _externalServiceB.CheckPackageStatus(orderId);
        }

        public decimal CalculateDeliveryCost(string orderId)
        {
            return _externalServiceB.CalculateShippingCost(orderId);
        }
    }

    // Адаптер для службы C
    public class LogisticsAdapterC : IInternalDeliveryService
    {
        private readonly ExternalLogisticsServiceC _externalServiceC;

        public LogisticsAdapterC(ExternalLogisticsServiceC externalServiceC)
        {
            _externalServiceC = externalServiceC;
        }

        public void DeliverOrder(string orderId)
        {
            _externalServiceC.DispatchPackage(orderId);
        }

        public string GetDeliveryStatus(string orderId)
        {
            return _externalServiceC.VerifyPackageStatus(orderId);
        }

        public decimal CalculateDeliveryCost(string orderId)
        {
            return _externalServiceC.EstimateShippingCost(orderId);
        }
    }

    // Фабрика для создания службы доставки
    public class DeliveryServiceFactory
    {
        public IInternalDeliveryService GetDeliveryService(string serviceType)
        {
            switch (serviceType)
            {
                case "internal":
                    return new InternalDeliveryService();
                case "externalA":
                    return new LogisticsAdapterA(new ExternalLogisticsServiceA());
                case "externalB":
                    return new LogisticsAdapterB(new ExternalLogisticsServiceB());
                case "externalC":
                    return new LogisticsAdapterC(new ExternalLogisticsServiceC());
                default:
                    throw new ArgumentException("Неизвестный тип службы доставки");
            }
        }
    }

    // Клиентский код
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new DeliveryServiceFactory();

            // Выбираем службу доставки на основе типа
            IInternalDeliveryService deliveryService = factory.GetDeliveryService("externalA");

            // Используем выбранную службу доставки
            string orderId = "123";
            Console.WriteLine($"Используется служба доставки для заказа {orderId}:");

            deliveryService.DeliverOrder(orderId);
            Console.WriteLine(deliveryService.GetDeliveryStatus(orderId));
            Console.WriteLine($"Стоимость доставки: {deliveryService.CalculateDeliveryCost(orderId)}");

            // Пример с обработкой ошибок
            try
            {
                IInternalDeliveryService invalidService = factory.GetDeliveryService("invalidType");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            // Пример с другой службой
            Console.WriteLine("\nИспользуется другая служба доставки:");
            IInternalDeliveryService anotherService = factory.GetDeliveryService("externalB");
            anotherService.DeliverOrder("456");
            Console.WriteLine(anotherService.GetDeliveryStatus("456"));
            Console.WriteLine($"Стоимость доставки: {anotherService.CalculateDeliveryCost("456")}");
        }
    }
}
