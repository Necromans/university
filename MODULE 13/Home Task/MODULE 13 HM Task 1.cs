using System;
using System.Collections.Generic;

namespace HiringProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Начало процесса найма сотрудников.");

            // Этап 1: Подготовка заявки
            if (!SubmitJobRequest())
            {
                Console.WriteLine("Заявка отклонена. Завершение процесса.");
                return;
            }

            // Этап 2: Отбор кандидатов
            List<Candidate> candidates = PublishJobAndCollectApplications();
            if (candidates.Count == 0)
            {
                Console.WriteLine("Нет подходящих кандидатов. Завершение процесса.");
                return;
            }

            // Этап 3: Проведение собеседований
            Candidate selectedCandidate = ConductInterviews(candidates);
            if (selectedCandidate == null)
            {
                Console.WriteLine("Все кандидаты отклонены. Завершение процесса.");
                return;
            }

            // Этап 4: Предложение оффера
            if (OfferJob(selectedCandidate))
            {
                Console.WriteLine($"Кандидат {selectedCandidate.Name} принят на работу.");
                NotifyITDepartment();
            }
            else
            {
                Console.WriteLine($"Кандидат {selectedCandidate.Name} отклонил оффер.");
            }

            Console.WriteLine("Процесс найма завершен.");
        }

        // Подготовка заявки на вакансию
        static bool SubmitJobRequest()
        {
            Console.WriteLine("Руководитель отдела создает заявку на вакансию.");
            Console.WriteLine("HR проверяет заявку...");
            bool isApproved = new Random().Next(0, 2) == 1;
            if (isApproved)
            {
                Console.WriteLine("Заявка утверждена HR.");
                return true;
            }
            else
            {
                Console.WriteLine("Заявка отклонена HR.");
                return false;
            }
        }

        // Публикация вакансии и сбор заявок
        static List<Candidate> PublishJobAndCollectApplications()
        {
            Console.WriteLine("Вакансия опубликована на сайте компании.");
            Console.WriteLine("Кандидаты подают заявки...");
            List<Candidate> candidates = new List<Candidate>
            {
                new Candidate("Иван Иванов", "ivan@example.com"),
                new Candidate("Мария Смирнова", "maria@example.com")
            };
            Console.WriteLine("Собрано " + candidates.Count + " заявок.");
            return candidates;
        }

        // Проведение собеседований
        static Candidate ConductInterviews(List<Candidate> candidates)
        {
            foreach (var candidate in candidates)
            {
                Console.WriteLine($"Проведение собеседования с кандидатом {candidate.Name}...");
                bool passedInterview = new Random().Next(0, 2) == 1;

                if (passedInterview)
                {
                    Console.WriteLine($"{candidate.Name} прошел собеседование.");
                    return candidate;
                }
                else
                {
                    Console.WriteLine($"{candidate.Name} не прошел собеседование.");
                }
            }
            return null;
        }

        // Предложение оффера кандидату
        static bool OfferJob(Candidate candidate)
        {
            Console.WriteLine($"Предложение оффера кандидату {candidate.Name}.");
            bool isAccepted = new Random().Next(0, 2) == 1;
            return isAccepted;
        }

        // Уведомление IT-отдела о настройке рабочего места
        static void NotifyITDepartment()
        {
            Console.WriteLine("HR уведомляет IT-отдел о настройке рабочего места.");
        }
    }

    class Candidate
    {
        public string Name { get; }
        public string Email { get; }

        public Candidate(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
