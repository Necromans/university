public abstract class Vehicle
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }

    public Vehicle(string brand, string model, int year)
    {
        Brand = brand;
        Model = model;
        Year = year;
    }

    public virtual void StartEngine()
    {
        Console.WriteLine($"{Brand} {Model} двигатель запущен.");
    }

    public virtual void StopEngine()
    {
        Console.WriteLine($"{Brand} {Model} двигатель остановлен.");
    }

    public override string ToString()
    {
        return $"{Brand} {Model}, {Year}";
    }
}


public class Car : Vehicle
{
    public int NumberOfDoors { get; set; }
    public string TransmissionType { get; set; }

    public Car(string brand, string model, int year, int numberOfDoors, string transmissionType)
        : base(brand, model, year)
    {
        NumberOfDoors = numberOfDoors;
        TransmissionType = transmissionType;
    }

    public override string ToString()
    {
        return base.ToString() + $", {NumberOfDoors} двери, {TransmissionType} трансмиссия";
    }
}


public class Motorcycle : Vehicle
{
    public string BodyType { get; set; }
    public bool HasBox { get; set; }

    public Motorcycle(string brand, string model, int year, string bodyType, bool hasBox)
        : base(brand, model, year)
    {
        BodyType = bodyType;
        HasBox = hasBox;
    }

    public override string ToString()
    {
        return base.ToString() + $", {BodyType} кузов, {(HasBox ? "с бокосом" : "без бокса")}";
    }
}


public class Garage
{
    public string Name { get; set; }
    public List<Vehicle> Vehicles { get; private set; }

    public Garage(string name)
    {
        Name = name;
        Vehicles = new List<Vehicle>();
    }

    public void AddVehicle(Vehicle vehicle)
    {
        Vehicles.Add(vehicle);
        Console.WriteLine($"{vehicle} добавлен в {Name} гараж.");
    }

    public void RemoveVehicle(Vehicle vehicle)
    {
        if (Vehicles.Remove(vehicle))
        {
            Console.WriteLine($"{vehicle} удален из {Name} гаража.");
        }
        else
        {
            Console.WriteLine($"{vehicle} не найдено в {Name} гараже.");
        }
    }

    public void ListVehicles()
    {
        Console.WriteLine($"Транспортные средства в {Name} гараже:");
        foreach (var vehicle in Vehicles)
        {
            Console.WriteLine(vehicle);
        }
    }
}

public class Fleet
{
    public List<Garage> Garages { get; private set; }

    public Fleet()
    {
        Garages = new List<Garage>();
    }

    public void AddGarage(Garage garage)
    {
        Garages.Add(garage);
        Console.WriteLine($"{garage.Name} гараж добавлен в автопарк.");
    }

    public void RemoveGarage(Garage garage)
    {
        if (Garages.Remove(garage))
        {
            Console.WriteLine($"{garage.Name} гараж удален из автопарка.");
        }
        else
        {
            Console.WriteLine($"{garage.Name} гараж не найден в автопарке.");
        }
    }

    public Vehicle FindVehicle(string brand, string model)
    {
        foreach (var garage in Garages)
        {
            var vehicle = garage.Vehicles.FirstOrDefault(v => v.Brand == brand && v.Model == model);
            if (vehicle != null)
            {
                return vehicle;
            }
        }
        return null;
    }

    public void ListAllVehicles()
    {
        foreach (var garage in Garages)
        {
            garage.ListVehicles();
        }
    }
}


class Program
{
    static void Main(string[] args)
    {
        // Создание автомобилей и мотоциклов
        Car car1 = new Car("Toyota", "Camry", 2020, 4, "Автоматическая");
        Car car2 = new Car("Ford", "Focus", 2018, 5, "Ручная");
        Motorcycle motorcycle1 = new Motorcycle("Yamaha", "MT-07", 2022, "Спортивный", false);

        // Создание гаражей
        Garage garage1 = new Garage("Первый");
        Garage garage2 = new Garage("Второй");

        // Добавление транспортных средств в гаражи
        garage1.AddVehicle(car1);
        garage1.AddVehicle(motorcycle1);
        garage2.AddVehicle(car2);

        // Создание автопарка и добавление гаражей
        Fleet fleet = new Fleet();
        fleet.AddGarage(garage1);
        fleet.AddGarage(garage2);

        // Тестирование методов
        fleet.ListAllVehicles();

        // Поиск транспортного средства
        var foundVehicle = fleet.FindVehicle("Yamaha", "MT-07");
        if (foundVehicle != null)
        {
            Console.WriteLine($"Транспорт найден: {foundVehicle}");
        }

        // Удаление транспортных средств и гаражей
        garage1.RemoveVehicle(car1);
        fleet.RemoveGarage(garage2);

        // Итоговый отчет
        Console.WriteLine("Финальный отчет автопарка:");
        fleet.ListAllVehicles();
    }
}


