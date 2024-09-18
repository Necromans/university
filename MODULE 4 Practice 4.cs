using System;
using System.Collections.Generic;

public interface IPayment
{
    void ProcessPayment(double amount);
}

public interface IDelivery
{
    void DeliverOrder(Order order);
}

public interface INotification
{
    void SendNotification(string message);
}


public class Order
{
    public List<OrderItem> Items { get; private set; }
    public IPayment PaymentMethod { get; set; }
    public IDelivery DeliveryMethod { get; set; }

    public Order()
    {
        Items = new List<OrderItem>();
    }

    public void AddItem(string productName, double price, int quantity)
    {
        Items.Add(new OrderItem(productName, price, quantity));
    }

    public double CalculateTotal(DiscountCalculator discountCalculator)
    {
        double total = 0;
        foreach (var item in Items)
        {
            total += item.Price * item.Quantity;
        }
        return discountCalculator.ApplyDiscount(total);
    }
}

public class OrderItem
{
    public string ProductName { get; }
    public double Price { get; }
    public int Quantity { get; }

    public OrderItem(string productName, double price, int quantity)
    {
        ProductName = productName;
        Price = price;
        Quantity = quantity;
    }
}

public class CreditCardPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Оплата картой на сумму: {amount}");
    }
}

public class PayPalPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Оплата через PayPal на сумму: {amount}");
    }
}

public class BankTransferPayment : IPayment
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Банковский перевод на сумму: {amount}");
    }
}


public class CourierDelivery : IDelivery
{
    public void DeliverOrder(Order order)
    {
        Console.WriteLine("Заказ будет доставлен курьером.");
    }
}

public class PostDelivery : IDelivery
{
    public void DeliverOrder(Order order)
    {
        Console.WriteLine("Заказ будет отправлен почтой.");
    }
}

public class PickUpPointDelivery : IDelivery
{
    public void DeliverOrder(Order order)
    {
        Console.WriteLine("Заказ можно забрать в пункте выдачи.");
    }
}

public class EmailNotification : INotification
{
    public void SendNotification(string message)
    {
        Console.WriteLine($"Отправка уведомления по электронной почте: {message}");
    }
}

public class SmsNotification : INotification
{
    public void SendNotification(string message)
    {
        Console.WriteLine($"Отправка SMS уведомления: {message}");
    }
}

public class DiscountCalculator
{
    public double ApplyDiscount(double total)
    {
        return total * 0.9; // Пример: 10% скидка
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создаем заказ
        Order order = new Order();
        order.AddItem("Товар 1", 100, 1);
        order.AddItem("Товар 2", 50, 2);

        // Устанавливаем способ оплаты
        order.PaymentMethod = new CreditCardPayment();
       
        // Устанавливаем способ доставки
        order.DeliveryMethod = new CourierDelivery();

        // Рассчитываем стоимость заказа
        DiscountCalculator discountCalculator = new DiscountCalculator();
        double totalAmount = order.CalculateTotal(discountCalculator);
        Console.WriteLine($"Итоговая стоимость заказа: {totalAmount}");

        // Обрабатываем платеж
        order.PaymentMethod.ProcessPayment(totalAmount);

        // Делаем доставку
        order.DeliveryMethod.DeliverOrder(order);

        // Отправляем уведомление
        INotification notification = new EmailNotification();
        notification.SendNotification("Ваш заказ был успешно оформлен!");
    }
}

