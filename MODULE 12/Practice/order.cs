using System;
using System.Collections.Generic;

namespace OnlineStore
{
    // Класс Товар
    class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"ID: {ID}, Название: {Name}, Цена: {Price:C}";
        }
    }

    // Класс Корзина
    class Cart
    {
        public List<Product> Products { get; private set; } = new List<Product>();

        public void AddProduct(Product product)
        {
            Products.Add(product);
            Console.WriteLine($"Товар {product.Name} добавлен в корзину.");
        }

        public void ShowCart()
        {
            Console.WriteLine("\nСодержимое корзины:");
            if (Products.Count == 0)
            {
                Console.WriteLine("Корзина пуста.");
                return;
            }

            decimal total = 0;
            foreach (var product in Products)
            {
                Console.WriteLine(product);
                total += product.Price;
            }
            Console.WriteLine($"Итого к оплате: {total:C}");
        }
    }

    // Класс Заказ
    class Order
    {
        public int OrderID { get; set; }
        public Cart Cart { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public bool IsPaid { get; set; } = false;

        public override string ToString()
        {
            return $"Номер заказа: {OrderID}, Покупатель: {CustomerName}, Адрес доставки: {Address}, Статус оплаты: {(IsPaid ? "Оплачен" : "Не оплачен")}";
        }
    }

    // Класс для обработки заказа
    class OrderProcessor
    {
        public void ProcessOrder(Order order)
        {
            Console.WriteLine("\nОбработка заказа...");
            Console.WriteLine("Заказ обрабатывается на складе...");
            Console.WriteLine("Заказ передан в службу доставки.");
        }
    }

    // Основной класс программы
    class Program
    {
        static List<Product> products = new List<Product>();
        static Cart cart = new Cart();
        static int orderCounter = 1;

        static void Main(string[] args)
        {
            SeedProducts(); // Создание тестовых товаров
            MainMenu();     // Главное меню
        }

        static void SeedProducts()
        {
            products.Add(new Product { ID = 1, Name = "Смартфон", Price = 30000 });
            products.Add(new Product { ID = 2, Name = "Ноутбук", Price = 60000 });
            products.Add(new Product { ID = 3, Name = "Наушники", Price = 5000 });
        }

        static void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("\nДобро пожаловать в интернет-магазин!");
                Console.WriteLine("1. Просмотреть товары");
                Console.WriteLine("2. Добавить товар в корзину");
                Console.WriteLine("3. Просмотреть корзину");
                Console.WriteLine("4. Оформить заказ");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowProducts();
                        break;
                    case "2":
                        AddToCart();
                        break;
                    case "3":
                        cart.ShowCart();
                        break;
                    case "4":
                        CreateOrder();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный ввод.");
                        break;
                }
            }
        }

        static void ShowProducts()
        {
            Console.WriteLine("\nСписок доступных товаров:");
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }

        static void AddToCart()
        {
            Console.Write("\nВведите ID товара для добавления в корзину: ");
            if (int.TryParse(Console.ReadLine(), out int productId))
            {
                var product = products.Find(p => p.ID == productId);
                if (product != null)
                {
                    cart.AddProduct(product);
                }
                else
                {
                    Console.WriteLine("Товар не найден.");
                }
            }
            else
            {
                Console.WriteLine("Неверный ввод.");
            }
        }

        static void CreateOrder()
        {
            if (cart.Products.Count == 0)
            {
                Console.WriteLine("Корзина пуста. Сначала добавьте товары.");
                return;
            }

            Console.Write("\nВведите ваше имя: ");
            string customerName = Console.ReadLine();
            Console.Write("Введите адрес доставки: ");
            string address = Console.ReadLine();

            Order order = new Order
            {
                OrderID = orderCounter++,
                Cart = cart,
                CustomerName = customerName,
                Address = address
            };

            cart.ShowCart();
            Console.Write("Перейти к оплате? (да/нет): ");
            if (Console.ReadLine().ToLower() == "да")
            {
                Pay(order);
            }
            else
            {
                Console.WriteLine("Заказ отменен.");
            }
        }

        static void Pay(Order order)
        {
            Console.Write("\nВведите сумму для оплаты: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                decimal total = order.Cart.Products.Sum(p => p.Price);
                if (amount >= total)
                {
                    order.IsPaid = true;
                    Console.WriteLine("Оплата прошла успешно!");
                    var processor = new OrderProcessor();
                    processor.ProcessOrder(order);
                }
                else
                {
                    Console.WriteLine("Недостаточно средств. Заказ не оформлен.");
                }
            }
            else
            {
                Console.WriteLine("Неверный ввод.");
            }
        }
    }
}
