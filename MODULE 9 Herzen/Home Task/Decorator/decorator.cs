using System;

public interface IBeverage
{
    string GetDescription();
    double Cost();
}

public class Espresso : IBeverage
{
    public string GetDescription() => "Эспрессо";
    public double Cost() => 2.00;
}

public class Tea : IBeverage
{
    public string GetDescription() => "Чай";
    public double Cost() => 1.50;
}

public abstract class BeverageDecorator : IBeverage
{
    protected IBeverage _beverage;

    public BeverageDecorator(IBeverage beverage)
    {
        _beverage = beverage;
    }

    public virtual string GetDescription()
    {
        return _beverage.GetDescription();
    }

    public virtual double Cost()
    {
        return _beverage.Cost();
    }
}

public class Milk : BeverageDecorator
{
    public Milk(IBeverage beverage) : base(beverage) { }

    public override string GetDescription()
    {
        return _beverage.GetDescription() + ", Молоко";
    }

    public override double Cost()
    {
        return _beverage.Cost() + 0.50;
    }
}

public class Sugar : BeverageDecorator
{
    public Sugar(IBeverage beverage) : base(beverage) { }

    public override string GetDescription()
    {
        return _beverage.GetDescription() + ", Сахар";
    }

    public override double Cost()
    {
        return _beverage.Cost() + 0.20;
    }
}

public class WhippedCream : BeverageDecorator
{
    public WhippedCream(IBeverage beverage) : base(beverage) { }

    public override string GetDescription()
    {
        return _beverage.GetDescription() + ", Взбитые сливки";
    }

    public override double Cost()
    {
        return _beverage.Cost() + 1.00;
    }
}

public class Latte : IBeverage
{
    public string GetDescription() => "Латте";
    public double Cost() => 3.00;
}

public class Mocha : IBeverage
{
    public string GetDescription() => "Мокко";
    public double Cost() => 3.50;
}

public class ChocolateSyrup : BeverageDecorator
{
    public ChocolateSyrup(IBeverage beverage) : base(beverage) { }

    public override string GetDescription()
    {
        return _beverage.GetDescription() + ", Шоколадный сироп";
    }

    public override double Cost()
    {
        return _beverage.Cost() + 0.75;
    }
}

class Program
{
    static void Main(string[] args)
    {
        //Пример 1: Эспрессо с добавками
        IBeverage beverage = new Espresso();
        Console.WriteLine($"{beverage.GetDescription()} стоит {beverage.Cost()}$");

        beverage = new Milk(beverage);
        Console.WriteLine($"{beverage.GetDescription()} стоит {beverage.Cost()}$");

        beverage = new Sugar(beverage);
        Console.WriteLine($"{beverage.GetDescription()} стоит {beverage.Cost()}$");

        beverage = new WhippedCream(beverage);
        Console.WriteLine($"{beverage.GetDescription()} стоит {beverage.Cost()}$");

        //Пример 2: Латте с добавками
        IBeverage anotherBeverage = new Latte();
        anotherBeverage = new Milk(anotherBeverage);
        anotherBeverage = new ChocolateSyrup(anotherBeverage);
        Console.WriteLine($"{anotherBeverage.GetDescription()} стоит {anotherBeverage.Cost()}$");

        //Пример 3: Мокко с добавками
        IBeverage mochaBeverage = new Mocha();
        mochaBeverage = new Sugar(mochaBeverage);
        mochaBeverage = new WhippedCream(mochaBeverage);
        Console.WriteLine($"{mochaBeverage.GetDescription()} стоит {mochaBeverage.Cost()}$");
    }
}
