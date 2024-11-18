using System;

public interface IPaymentProcessor
{
    void ProcessPayment(double amount);
    void RefundPayment(double amount);
}

// Внутренняя платежная система
public class InternalPaymentProcessor : IPaymentProcessor
{
    public void ProcessPayment(double amount)
    {
        Console.WriteLine($"Обработка платежа на сумму {amount} через внутреннюю систему.");
    }

    public void RefundPayment(double amount)
    {
        Console.WriteLine($"Возврат средств на сумму {amount} через внутреннюю систему.");
    }
}

// Сторонняя платежная система A
public class ExternalPaymentSystemA
{
    public void MakePayment(double amount)
    {
        Console.WriteLine($"Платеж на сумму {amount} через внешнюю платежную систему A.");
    }

    public void MakeRefund(double amount)
    {
        Console.WriteLine($"Возврат средств на сумму {amount} через внешнюю платежную систему A.");
    }
}

// Сторонняя платежная система B
public class ExternalPaymentSystemB
{
    public void SendPayment(double amount)
    {
        Console.WriteLine($"Платеж на сумму {amount} через внешнюю платежную систему B.");
    }

    public void ProcessRefund(double amount)
    {
        Console.WriteLine($"Возврат средств на сумму {amount} через внешнюю платежную систему B.");
    }
}

// Адаптер для ExternalPaymentSystemA
public class PaymentAdapterA : IPaymentProcessor
{
    private ExternalPaymentSystemA _externalSystemA;

    public PaymentAdapterA(ExternalPaymentSystemA externalSystemA)
    {
        _externalSystemA = externalSystemA;
    }

    public void ProcessPayment(double amount)
    {
        _externalSystemA.MakePayment(amount);
    }

    public void RefundPayment(double amount)
    {
        _externalSystemA.MakeRefund(amount);
    }
}

// Адаптер для ExternalPaymentSystemB
public class PaymentAdapterB : IPaymentProcessor
{
    private ExternalPaymentSystemB _externalSystemB;

    public PaymentAdapterB(ExternalPaymentSystemB externalSystemB)
    {
        _externalSystemB = externalSystemB;
    }

    public void ProcessPayment(double amount)
    {
        _externalSystemB.SendPayment(amount);
    }

    public void RefundPayment(double amount)
    {
        _externalSystemB.ProcessRefund(amount);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Используем внутреннюю платежную систему
        IPaymentProcessor internalProcessor = new InternalPaymentProcessor();
        internalProcessor.ProcessPayment(100.0);
        internalProcessor.RefundPayment(50.0);

        // Используем внешнюю платежную систему A через адаптер
        ExternalPaymentSystemA externalSystemA = new ExternalPaymentSystemA();
        IPaymentProcessor adapterA = new PaymentAdapterA(externalSystemA);
        adapterA.ProcessPayment(200.0);
        adapterA.RefundPayment(100.0);

        // Используем внешнюю платежную систему B через адаптер
        ExternalPaymentSystemB externalSystemB = new ExternalPaymentSystemB();
        IPaymentProcessor adapterB = new PaymentAdapterB(externalSystemB);
        adapterB.ProcessPayment(300.0);
        adapterB.RefundPayment(150.0);
    }
}
