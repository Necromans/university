//Было (Использование методов для устранения дублирования кода)

//public class OrderService
//{
//    public void CreateOrder(string productName, int quantity, double price)
//    {
//        double totalPrice = quantity * price;
//        Console.WriteLine($"Order for {productName} created. Total: {totalPrice}");
//    }
//    public void UpdateOrder(string productName, int quantity, double price)
//    {
//        double totalPrice = quantity * price;
//        Console.WriteLine($"Order for {productName} updated. New total: {totalPrice}");
//    }
//}

//Стало
public class OrderService
{
    public double CalculateTotalPrice(int quantity, double price)
    {
        return quantity * price;
    }

    public void CreateOrder(string productName, int quantity, double price)
    {
        double totalPrice = CalculateTotalPrice(quantity, price);
        Console.WriteLine($"Order for {productName} created. Total: {totalPrice}");
    }
    public void UpdateOrder(string productName, int quantity, double price)
    {
        double totalPrice = CalculateTotalPrice(quantity, price);
        Console.WriteLine($"Order for {productName} updated. New total: {totalPrice}");
    }
}



//Было (Использование общих базовых классов)

//public class Car
//{
//    public void Start()
//    {
//        Console.WriteLine("Car is starting");
//    }
//    public void Stop()
//    {
//        Console.WriteLine("Car is stopping");
//    }
//}
//public class Truck
//{
//    public void Start()
//    {
//        Console.WriteLine("Truck is starting");
//    }
//    public void Stop()
//    {
//        Console.WriteLine("Truck is stopping");
//    }
//}



//Стало


public class Vehicle
{
    public void Start()
    {
        Console.WriteLine($"{this.GetType().Name} is starting");
    }
    public void Stop()
    {
        Console.WriteLine($"{this.GetType().Name} is stopping");
    }
}
public class Car : Vehicle
{
    
}
public class Truck : Vehicle
{
    
}




//Было (Избегание чрезмерного использования абстракци) Принцип KISS (Keep It Simple, Stupid) — Упрощение кода

//public interface IOperation
//{
//    void Execute();
//}
//public class AdditionOperation : IOperation
//{
//    private int _a;
//    private int _b;
//    public AdditionOperation(int a, int b)
//    {
//        _a = a;
//        _b = b;
//    }
//    public void Execute()
//    {
//        Console.WriteLine(_a + _b);
//    }
//}
//public class Calculator
//{
//    public void PerformOperation(IOperation operation)
//    {
//        operation.Execute();
//    }
//}



//Стало

public class Calculator
{
    public void Add(int a, int b)
    {
        Console.WriteLine(a + b);
    }
}





//Было (Избегание избыточного использования шаблонов проектирования)

//public class Singleton
//{
//    private static Singleton _instance;
//    private Singleton() { }
//    public static Singleton Instance
//    {
//        get
//        {
//            if (_instance == null)
//            {
//                _instance = new Singleton();
//            }
//            return _instance;
//        }
//    }
//    public void DoSomething()
//    {
//        Console.WriteLine("Doing something...");
//    }
//}
//public class Client
//{
//    public void Execute()
//    {
//        Singleton.Instance.DoSomething();
//    }
//}



//Стало

public class Client
{
    public void Execute()
    {
        Console.WriteLine("Doing something...");
    }
}



//Было (Избыточное создание абстракций) YAGNI (You Ain't Gonna Need It) — Избегание ненужной функциональности

//public interface IShape
//{
//    double CalculateArea();
//}

//public class Circle : IShape
//{
//    private double _radius;

//    public Circle(double radius)
//    {
//        _radius = radius;
//    }

//    public double CalculateArea()
//    {
//        return Math.PI * _radius * _radius;
//    }
//}

//public class Square : IShape
//{
//    private double _side;

//    public Square(double side)
//    {
//        _side = side;
//    }

//    public double CalculateArea()
//    {
//        return _side * _side;
//    }
//}



//Стало

public class Circle
{
    private double _radius;

    public Circle(double radius)
    {
        _radius = radius;
    }

    public double CalculateArea()
    {
        return Math.PI * _radius * _radius;
    }
}

public class Square
{
    private double _side;

    public Square(double side)
    {
        _side = side;
    }

    public double CalculateArea()
    {
        return _side * _side;
    }
}



//Было (Излишняя параметризация методов)

//public class MathOperations
//{
//    public int Add(int a, int b, bool shouldLog = false)
//    {
//        int result = a + b;
//        if (shouldLog)
//        {
//            Console.WriteLine($"Result: {result}");
//        }
//        return result;
//    }
//}



//Стало

public class MathOperations
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}




