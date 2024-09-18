using System;
using System.Collections.Generic;

class Employee
{
    public string Name { get; set; }
    public int ID { get; set; }
    public string Position { get; set; }

    public Employee(string name, int id, string position)
    {
        Name = name;
        ID = id;
        Position = position;
    }

    // Метод для расчета зарплаты, будет переопределяться в производных классах
    public virtual decimal CalculateSalary()
    {
        return 0;
    }

    public override string ToString()
    {
        return $"Имя: {Name}, ID: {ID}, Должность: {Position}";
    }
}

class Worker : Employee
{
    public decimal HourlyRate { get; set; }
    public int HoursWorked { get; set; }

    public Worker(string name, int id, string position, decimal hourlyRate, int hoursWorked)
        : base(name, id, position)
    {
        HourlyRate = hourlyRate;
        HoursWorked = hoursWorked;
    }

    // Переопределение метода расчета зарплаты для рабочего
    public override decimal CalculateSalary()
    {
        return HourlyRate * HoursWorked;
    }
}

class Manager : Employee
{
    public decimal FixedSalary { get; set; }
    public decimal Bonus { get; set; }

    public Manager(string name, int id, string position, decimal fixedSalary, decimal bonus)
        : base(name, id, position)
    {
        FixedSalary = fixedSalary;
        Bonus = bonus;
    }

    // Переопределение метода расчета зарплаты для менеджера
    public override decimal CalculateSalary()
    {
        return FixedSalary + Bonus;
    }
}


class EmployeeSystem
{
    private List<Employee> employees = new List<Employee>();

    // Метод для добавления сотрудника в систему
    public void AddEmployee(Employee employee)
    {
        employees.Add(employee);
    }

    // Метод для расчета и отображения зарплаты всех сотрудников
    public void ShowSalaries()
    {
        foreach (var employee in employees)
        {
            Console.WriteLine($"{employee}: Зарплата = {employee.CalculateSalary()} $.");
        }
    }
}


class Program
{
    static void Main(string[] args)
    {
        EmployeeSystem employeeSystem = new EmployeeSystem();

        // Создание рабочих
        Worker worker1 = new Worker("Алексей", 1, "Рабочий", 500, 160);
        Worker worker2 = new Worker("Иван", 2, "Рабочий", 600, 150);

        // Создание менеджеров
        Manager manager1 = new Manager("Ольга", 3, "Менеджер", 50000, 10000);
        Manager manager2 = new Manager("Мария", 4, "Менеджер", 60000, 15000);

        // Добавление сотрудников в систему
        employeeSystem.AddEmployee(worker1);
        employeeSystem.AddEmployee(worker2);
        employeeSystem.AddEmployee(manager1);
        employeeSystem.AddEmployee(manager2);

        // Отображение зарплат сотрудников
        employeeSystem.ShowSalaries();
    }
}
