using System;
using System.Collections.Generic;

class Program
{
    // Абстрактный класс FileSystemComponent
    abstract class FileSystemComponent
    {
        public string Name;

        public FileSystemComponent(string name)
        {
            Name = name;
        }

        public abstract void Display(string indent = "");
        public abstract long GetSize();
    }

    // Класс File
    class File : FileSystemComponent
    {
        private long Size;

        public File(string name, long size) : base(name)
        {
            Size = size;
        }

        public override void Display(string indent = "")
        {
            Console.WriteLine($"{indent}File: {Name} ({Size} bytes)");
        }

        public override long GetSize()
        {
            return Size;
        }
    }

    // Класс Directory
    class Directory : FileSystemComponent
    {
        private List<FileSystemComponent> Components = new List<FileSystemComponent>();

        public Directory(string name) : base(name) { }

        public void Add(FileSystemComponent component)
        {
            if (!Components.Contains(component))
            {
                Components.Add(component);
            }
            else
            {
                Console.WriteLine($"Компонент {component.Name} уже существует в папке {Name}.");
            }
        }

        public void Remove(FileSystemComponent component)
        {
            if (Components.Contains(component))
            {
                Components.Remove(component);
            }
            else
            {
                Console.WriteLine($"Компонент {component.Name} не найден в папке {Name}.");
            }
        }

        public override void Display(string indent = "")
        {
            Console.WriteLine($"{indent}Directory: {Name}");
            foreach (var component in Components)
            {
                component.Display(indent + "  ");
            }
        }

        public override long GetSize()
        {
            long totalSize = 0;
            foreach (var component in Components)
            {
                totalSize += component.GetSize();
            }
            return totalSize;
        }
    }

    static void Main(string[] args)
    {
        // Создание файлов и папок
        var file1 = new File("File1.txt", 1200);
        var file2 = new File("File2.txt", 800);
        var file3 = new File("File3.txt", 1500);

        var subDirectory = new Directory("SubDirectory");
        subDirectory.Add(file2);
        subDirectory.Add(file3);

        var rootDirectory = new Directory("Root");
        rootDirectory.Add(file1);
        rootDirectory.Add(subDirectory);

        // Вывод структуры файловой системы
        Console.WriteLine("Содержимое файловой системы:");
        rootDirectory.Display();

        // Расчет общего размера
        Console.WriteLine($"\nОбщий размер: {rootDirectory.GetSize()} bytes");

        // Пример удаления файла
        Console.WriteLine("\nУдаление файла File2.txt...");
        subDirectory.Remove(file2);

        // Вывод обновленной структуры
        Console.WriteLine("\nОбновленное содержимое:");
        rootDirectory.Display();

        Console.WriteLine($"\nОбновленный общий размер: {rootDirectory.GetSize()} bytes");
    }
}
