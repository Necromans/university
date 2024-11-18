using System;

//Интерфейс для всех процессоров платежей
public interface IPaymentProcessor
{
    void ProcessPayment(double amount);
}

//Реализация процессора для PayPal \
public class PayPalPaymentProcessor : IPaymentProcessor
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Оплата через PayPal на сумму {amount}.");
    }
}

//Сторонний класс для Stripe
public class StripePaymentService
{
    public void MakeTransaction(double totalAmount)
    {
        Console.WriteLine($"Оплата через Stripe на сумму {totalAmount}.");
    }
}

//Адаптер для Stripe, который адаптирует его к интерфейсу IPaymentProcessor
public class StripePaymentAdapter : IPaymentProcessor
{
    private readonly StripePaymentService _stripePaymentService;

    public StripePaymentAdapter(StripePaymentService stripePaymentService)
    {
        _stripePaymentService = stripePaymentService;
    }

    public void ProcessPayment(double amount)
    {
        _stripePaymentService.MakeTransaction(amount);
    }
}

//Сторонний класс для Google Pay
public class GooglePayPaymentService
{
    public void ExecutePayment(double totalAmount)
    {
        Console.WriteLine($"Оплата через Google Pay на сумму {totalAmount}.");
    }
}

//Адаптер для Google Pay, который адаптирует его к интерфейсу IPaymentProcessor
public class GooglePayPaymentAdapter : IPaymentProcessor
{
    private readonly GooglePayPaymentService _googlePayPaymentService;

    public GooglePayPaymentAdapter(GooglePayPaymentService googlePayPaymentService)
    {
        _googlePayPaymentService = googlePayPaymentService;
    }

    public void ProcessPayment(double amount)
    {
        _googlePayPaymentService.ExecutePayment(amount);
    }
}

class Program
{
    static void Main(string[] args)
    {
        //Создание объектов процессоров
        IPaymentProcessor paypalProcessor = new PayPalPaymentProcessor();
        IPaymentProcessor stripeProcessor = new StripePaymentAdapter(new StripePaymentService());
        IPaymentProcessor googlePayProcessor = new GooglePayPaymentAdapter(new GooglePayPaymentService());

        //Симуляция различных платежей
        ProcessPayment(paypalProcessor, 100);
        ProcessPayment(stripeProcessor, 200);
        ProcessPayment(googlePayProcessor, 300);
    }

    //Метод для обработки платежей через интерфейс IPaymentProcessor
    static void ProcessPayment(IPaymentProcessor paymentProcessor, double amount)
    {
        paymentProcessor.ProcessPayment(amount);
    }
}
