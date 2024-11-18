using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportSystem
{
    // Основной класс для отчетов
    public class SalesReport
    {
        public List<string> Sales { get; set; }

        public SalesReport()
        {
            Sales = new List<string>
            {
                "Продажа 1: 1000",
                "Продажа 2: 1500",
                "Продажа 3: 2000",
                "Продажа 4: 500",
                "Продажа 5: 1200"
            };
        }

        public string Generate()
        {
            return string.Join("\n", Sales);
        }
    }

    public class UserReport
    {
        public List<string> Users { get; set; }

        public UserReport()
        {
            Users = new List<string>
            {
                "Пользователь 1: Иван",
                "Пользователь 2: Мария",
                "Пользователь 3: Алексей",
                "Пользователь 4: Оля"
            };
        }

        public string Generate()
        {
            return string.Join("\n", Users);
        }
    }

    // Декоратор фильтрации по сумме продаж
    public class SalesAmountFilterDecorator
    {
        private readonly SalesReport _report;
        private readonly int _minAmount;
        private readonly int _maxAmount;

        public SalesAmountFilterDecorator(SalesReport report, int minAmount, int maxAmount)
        {
            _report = report;
            _minAmount = minAmount;
            _maxAmount = maxAmount;
        }

        public string Generate()
        {
            var filteredSales = _report.Sales
                .Where(s => int.Parse(s.Split(':')[1]) >= _minAmount && int.Parse(s.Split(':')[1]) <= _maxAmount)
                .ToList();
            return string.Join("\n", filteredSales);
        }
    }

    // Декоратор фильтрации по пользователям
    public class UserFilterDecorator
    {
        private readonly UserReport _report;
        private readonly string _filter;

        public UserFilterDecorator(UserReport report, string filter)
        {
            _report = report;
            _filter = filter;
        }

        public string Generate()
        {
            var filteredUsers = _report.Users.Where(u => u.Contains(_filter)).ToList();
            return string.Join("\n", filteredUsers);
        }
    }

    // Декоратор фильтрации по датам
    public class DateFilterDecorator
    {
        private readonly string _report;
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;

        public DateFilterDecorator(string report, DateTime startDate, DateTime endDate)
        {
            _report = report;
            _startDate = startDate;
            _endDate = endDate;
        }

        public string Generate()
        {
            return _report + $"\nФильтрация по датам: с {_startDate.ToShortDateString()} по {_endDate.ToShortDateString()}";
        }
    }

    // Декоратор сортировки
    public class SortingDecorator
    {
        private readonly string _report;
        private readonly string _sortBy;

        public SortingDecorator(string report, string sortBy)
        {
            _report = report;
            _sortBy = sortBy;
        }

        public string Generate()
        {
            return _report + $"\nСортировка по: {_sortBy}";
        }
    }

    // Декоратор экспорта в CSV
    public class CsvExportDecorator
    {
        private readonly string _report;

        public CsvExportDecorator(string report)
        {
            _report = report;
        }

        public string Generate()
        {
            return _report + "\nЭкспорт в CSV формат.";
        }
    }

    // Декоратор экспорта в PDF
    public class PdfExportDecorator
    {
        private readonly string _report;

        public PdfExportDecorator(string report)
        {
            _report = report;
        }

        public string Generate()
        {
            return _report + "\nЭкспорт в PDF формат.";
        }
    }

    // Механизм динамического выбора декораторов
    public class ReportBuilder
    {
        private readonly List<string> _decorators = new List<string>();

        public void AddDecorator(string decoratorType)
        {
            _decorators.Add(decoratorType);
        }

        public void GenerateReport()
        {
            SalesReport salesReport = new SalesReport();
            string report = salesReport.Generate();

            foreach (var decorator in _decorators)
            {
                if (decorator == "DateFilter")
                {
                    report = new DateFilterDecorator(report, new DateTime(2023, 1, 1), new DateTime(2023, 12, 31)).Generate();
                }
                else if (decorator == "SortByAmount")
                {
                    report = new SortingDecorator(report, "сумма продажи").Generate();
                }
                else if (decorator == "CsvExport")
                {
                    report = new CsvExportDecorator(report).Generate();
                }
                else if (decorator == "PdfExport")
                {
                    report = new PdfExportDecorator(report).Generate();
                }
                else if (decorator == "SalesAmountFilter")
                {
                    report = new SalesAmountFilterDecorator(salesReport, 1000, 2000).Generate();
                }
            }

            Console.WriteLine(report);
        }
    }

    // Класс Program для запуска приложения
    public class Program
    {
        public static void Main(string[] args)
        {
            ReportBuilder builder = new ReportBuilder();

            // Симуляция выбора декораторов пользователем
            builder.AddDecorator("DateFilter");
            builder.AddDecorator("SortByAmount");
            builder.AddDecorator("CsvExport");

            // Генерация отчета с выбранными декораторами
            builder.GenerateReport();
        }
    }
}
