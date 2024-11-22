using System;
using System.Collections.Generic;

// Абстрактный компонент файловой системы
public abstract class FileSystemComponent
{
    protected string _name;

    public FileSystemComponent(string name)
    {
        _name = name;
    }

    // Метод для отображения структуры
    public abstract void Display(int depth);

    // Методы для работы с дочерними элементами
    public virtual void Add(FileSystemComponent component)
    {
        throw new NotImplementedException();
    }

    public virtual void Remove(FileSystemComponent component)
    {
        throw new NotImplementedException();
    }

    public virtual FileSystemComponent GetChild(int index)
    {
        throw new NotImplementedException();
    }
}

// Класс File, представляющий файл (лист)
public class File : FileSystemComponent
{
    public File(string name) : base(name)
    {
    }

    public override void Display(int depth)
    {
        Console.WriteLine(new string('-', depth) + " Файл: " + _name);
    }
}

// Класс Directory, представляющий директорию (контейнер)
public class Directory : FileSystemComponent
{
    private List<FileSystemComponent> _children = new List<FileSystemComponent>();

    public Directory(string name) : base(name)
    {
    }

    public override void Add(FileSystemComponent component)
    {
        _children.Add(component);
    }

    public override void Remove(FileSystemComponent component)
    {
        _children.Remove(component);
    }

    public override FileSystemComponent GetChild(int index)
    {
        return _children[index];
    }

    public override void Display(int depth)
    {
        Console.WriteLine(new string('-', depth) + " Директория: " + _name);
        foreach (var component in _children)
        {
            component.Display(depth + 2);
        }
    }
}

// Клиентский код
public class Program
{
    public static void Main(string[] args)
    {
        // Создание файлов и директорий
        Directory root = new Directory("Корневая папка");
        File file1 = new File("Файл1.txt");
        File file2 = new File("Файл2.txt");
        Directory subDir = new Directory("Подпапка");
        File subFile1 = new File("Файл_в_подпапке.txt");

        // Формирование структуры компоновщика
        root.Add(file1);
        root.Add(file2);
        subDir.Add(subFile1);
        root.Add(subDir);

        // Отображение структуры файловой системы
        root.Display(1);
    }
}
