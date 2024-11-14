using System;
using System.Collections.Generic;
using System.Linq;

// Модели данных
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public List<Product> Products { get; set; }
    public string Status { get; set; }
}

// Интерфейсы
public interface IUserService
{
    User Register(string username, string password);
    User Login(string username, string password);
}

public interface IProductService
{
    List<Product> GetProducts();
    Product AddProduct(Product product);
}

public interface IOrderService
{
    Order CreateOrder(int userId, List<int> productIds);
    Order GetOrderStatus(int orderId);
}

public interface IPaymentService
{
    bool ProcessPayment(int orderId, decimal amount);
}

public interface INotificationService
{
    void SendNotification(int userId, string message);
}

// Реализация сервисов
public class UserService : IUserService
{
    private List<User> users = new List<User>();
    private int userIdCounter = 1;

    public User Register(string username, string password)
    {
        var user = new User { Id = userIdCounter++, Username = username, Password = password };
        users.Add(user);
        Console.WriteLine($"Пользователь {username} зарегистрирован.");
        return user;
    }

    public User Login(string username, string password)
    {
        var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);
        if (user != null)
        {
            Console.WriteLine($"Пользователь {username} авторизован.");
        }
        else
        {
            Console.WriteLine($"Ошибка авторизации для пользователя {username}.");
        }
        return user;
    }
}

public class ProductService : IProductService
{
    private List<Product> products = new List<Product>();

    public List<Product> GetProducts() => products;

    public Product AddProduct(Product product)
    {
        products.Add(product);
        Console.WriteLine($"Товар {product.Name} добавлен.");
        return product;
    }
}

public class OrderService : IOrderService
{
    private List<Order> orders = new List<Order>();
    private int orderIdCounter = 1;
    private IProductService _productService;
    private IPaymentService _paymentService;
    private INotificationService _notificationService;

    public OrderService(IProductService productService, IPaymentService paymentService, INotificationService notificationService)
    {
        _productService = productService;
        _paymentService = paymentService;
        _notificationService = notificationService;
    }

    public Order CreateOrder(int userId, List<int> productIds)
    {
        var products = _productService.GetProducts().Where(p => productIds.Contains(p.Id)).ToList();
        if (!products.Any())
        {
            Console.WriteLine("Ошибка: Продукты не найдены.");
            return null;
        }

        var order = new Order { Id = orderIdCounter++, UserId = userId, Products = products, Status = "Created" };
        decimal totalAmount = products.Sum(p => p.Price);

        if (_paymentService.ProcessPayment(order.Id, totalAmount))
        {
            order.Status = "Paid";
            _notificationService.SendNotification(userId, "Ваш заказ успешно оплачен.");
        }
        else
        {
            order.Status = "Payment Failed";
            _notificationService.SendNotification(userId, "Ошибка оплаты. Попробуйте снова.");
        }

        orders.Add(order);
        return order;
    }

    public Order GetOrderStatus(int orderId)
    {
        return orders.FirstOrDefault(o => o.Id == orderId);
    }
}

public class PaymentService : IPaymentService
{
    public bool ProcessPayment(int orderId, decimal amount)
    {
        // Для простоты всегда возвращаем true (платеж прошел)
        Console.WriteLine($"Платеж на сумму {amount} для заказа {orderId} успешно обработан.");
        return true;
    }
}

public class NotificationService : INotificationService
{
    public void SendNotification(int userId, string message)
    {
        Console.WriteLine($"Уведомление для пользователя {userId}: {message}");
    }
}

// Пример тестирования программы
class Program
{
    static void Main(string[] args)
    {
        // Инициализация сервисов
        IUserService userService = new UserService();
        IProductService productService = new ProductService();
        IPaymentService paymentService = new PaymentService();
        INotificationService notificationService = new NotificationService();
        IOrderService orderService = new OrderService(productService, paymentService, notificationService);

        // Добавление продуктов
        var book = productService.AddProduct(new Product { Id = 1, Name = "C# для начинающих", Price = 500 });
        var laptop = productService.AddProduct(new Product { Id = 2, Name = "Ноутбук", Price = 15000 });

        // Регистрация пользователя
        var user = userService.Register("john_doe", "password123");

        // Авторизация пользователя
        userService.Login("john_doe", "password123");

        // Создание заказа
        var order = orderService.CreateOrder(user.Id, new List<int> { book.Id, laptop.Id });

        // Получение статуса заказа
        var status = orderService.GetOrderStatus(order.Id);
        Console.WriteLine($"Статус заказа {order.Id}: {status.Status}");
    }
}
