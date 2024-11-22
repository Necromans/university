using System;
using System.Collections.Generic;

// Абстрактный класс для компонентов организации
public abstract class OrganizationComponent
{
    public string Name { get; set; }
    
    public OrganizationComponent(string name)
    {
        Name = name;
    }

    public virtual void Add(OrganizationComponent component)
    {
        throw new InvalidOperationException("Не поддерживается.");
    }

    public virtual void Remove(OrganizationComponent component)
    {
        throw new InvalidOperationException("Не поддерживается.");
    }

    public virtual double GetBudget()
    {
        throw new InvalidOperationException("Не поддерживается.");
    }

    public virtual int GetEmployeeCount()
    {
        throw new InvalidOperationException("Не поддерживается.");
    }

    public virtual void Display(int depth)
    {
        throw new InvalidOperationException("Не поддерживается.");
    }
}

// Класс для представления сотрудника
public class Employee : OrganizationComponent
{
    public string Position { get; set; }
    public double Salary { get; set; }

    public Employee(string name, string position, double salary) : base(name)
    {
        Position = position;
        Salary = salary;
    }

    public override double GetBudget()
    {
        return Salary;
    }

    public override int GetEmployeeCount()
    {
        return 1;
    }

    public override void Display(int depth)
    {
        Console.WriteLine(new String('-', depth) + "Сотрудник: " + Name + ", Должность: " + Position + ", Зарплата: " + Salary);
    }
}

// Класс для представления отдела
public class Department : OrganizationComponent
{
    private List<OrganizationComponent> _components = new List<OrganizationComponent>();

    public Department(string name) : base(name) { }

    public override void Add(OrganizationComponent component)
    {
        _components.Add(component);
    }

    public override void Remove(OrganizationComponent component)
    {
        _components.Remove(component);
    }

    public override double GetBudget()
    {
        double totalBudget = 0;
        foreach (var component in _components)
        {
            totalBudget += component.GetBudget();
        }
        return totalBudget;
    }

    public override int GetEmployeeCount()
    {
        int totalEmployees = 0;
        foreach (var component in _components)
        {
            totalEmployees += component.GetEmployeeCount();
        }
        return totalEmployees;
    }

    public override void Display(int depth)
    {
        Console.WriteLine(new String('-', depth) + "Отдел: " + Name);
        foreach (var component in _components)
        {
            component.Display(depth + 2);
        }
    }
}

// Пример использования системы
class Program
{
    static void Main(string[] args)
    {
        // Создаем сотрудников
        Employee emp1 = new Employee("Иванов Иван", "Менеджер", 50000);
        Employee emp2 = new Employee("Петров Петр", "Разработчик", 60000);
        Employee emp3 = new Employee("Сидоров Сидор", "Тестировщик", 40000);

        // Создаем отделы
        Department devDepartment = new Department("Отдел разработки");
        Department hrDepartment = new Department("Отдел кадров");

        // Добавляем сотрудников в отделы
        devDepartment.Add(emp2);
        devDepartment.Add(emp3);

        // Создаем главный отдел компании
        Department companyDepartment = new Department("Компания");

        // Добавляем подразделения в компанию
        companyDepartment.Add(devDepartment);
        companyDepartment.Add(hrDepartment);
        companyDepartment.Add(emp1);

        // Отображаем структуру
        Console.WriteLine("Структура организации:");
        companyDepartment.Display(1);

        // Расчет бюджета и числа сотрудников
        Console.WriteLine("\nОбщий бюджет компании: " + companyDepartment.GetBudget());
        Console.WriteLine("Общее количество сотрудников: " + companyDepartment.GetEmployeeCount());
    }
}
