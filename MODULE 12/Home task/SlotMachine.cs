using System;

namespace TicketMachineApp
{
    // Интерфейс состояния
    public interface IState
    {
        void SelectTicket(TicketMachine machine);
        void InsertMoney(TicketMachine machine, decimal amount);
        void DispenseTicket(TicketMachine machine);
        void CancelTransaction(TicketMachine machine);
    }

    // Состояние ожидания действия пользователя (Idle)
    public class IdleState : IState
    {
        public void SelectTicket(TicketMachine machine)
        {
            Console.WriteLine("Билет выбран. Внесите деньги.");
            machine.SetState(new WaitingForMoneyState());
        }

        public void InsertMoney(TicketMachine machine, decimal amount)
        {
            Console.WriteLine("Выберите билет перед внесением денег.");
        }

        public void DispenseTicket(TicketMachine machine)
        {
            Console.WriteLine("Сначала выберите билет и внесите деньги.");
        }

        public void CancelTransaction(TicketMachine machine)
        {
            Console.WriteLine("Транзакция не началась.");
        }
    }

    // Состояние ожидания внесения денег
    public class WaitingForMoneyState : IState
    {
        public void SelectTicket(TicketMachine machine)
        {
            Console.WriteLine("Билет уже выбран. Внесите деньги.");
        }

        public void InsertMoney(TicketMachine machine, decimal amount)
        {
            if (amount >= machine.TicketPrice)
            {
                Console.WriteLine($"Деньги внесены: {amount:C}. Выдача билета...");
                machine.SetState(new MoneyReceivedState());
            }
            else
            {
                Console.WriteLine($"Недостаточно средств. Внесите еще {machine.TicketPrice - amount:C}.");
            }
        }

        public void DispenseTicket(TicketMachine machine)
        {
            Console.WriteLine("Сначала внесите необходимую сумму.");
        }

        public void CancelTransaction(TicketMachine machine)
        {
            Console.WriteLine("Транзакция отменена.");
            machine.SetState(new TransactionCanceledState());
        }
    }

    // Состояние "Деньги внесены"
    public class MoneyReceivedState : IState
    {
        public void SelectTicket(TicketMachine machine)
        {
            Console.WriteLine("Вы уже выбрали билет.");
        }

        public void InsertMoney(TicketMachine machine, decimal amount)
        {
            Console.WriteLine("Деньги уже внесены.");
        }

        public void DispenseTicket(TicketMachine machine)
        {
            Console.WriteLine("Билет выдан. Спасибо за покупку!");
            machine.SetState(new TicketDispensedState());
        }

        public void CancelTransaction(TicketMachine machine)
        {
            Console.WriteLine("Транзакция не может быть отменена после внесения денег.");
        }
    }

    // Состояние "Билет выдан"
    public class TicketDispensedState : IState
    {
        public void SelectTicket(TicketMachine machine)
        {
            Console.WriteLine("Вы уже получили билет.");
        }

        public void InsertMoney(TicketMachine machine, decimal amount)
        {
            Console.WriteLine("Вы уже получили билет.");
        }

        public void DispenseTicket(TicketMachine machine)
        {
            Console.WriteLine("Билет уже выдан.");
        }

        public void CancelTransaction(TicketMachine machine)
        {
            Console.WriteLine("Транзакция завершена.");
        }
    }

    // Состояние "Транзакция отменена"
    public class TransactionCanceledState : IState
    {
        public void SelectTicket(TicketMachine machine)
        {
            Console.WriteLine("Транзакция отменена. Выберите новый билет.");
            machine.SetState(new IdleState());
        }

        public void InsertMoney(TicketMachine machine, decimal amount)
        {
            Console.WriteLine("Транзакция отменена. Выберите билет заново.");
        }

        public void DispenseTicket(TicketMachine machine)
        {
            Console.WriteLine("Транзакция отменена.");
        }

        public void CancelTransaction(TicketMachine machine)
        {
            Console.WriteLine("Транзакция уже отменена.");
        }
    }

    // Класс автомата по продаже билетов
    public class TicketMachine
    {
        private IState _currentState;
        public decimal TicketPrice { get; private set; }

        public TicketMachine(decimal ticketPrice)
        {
            TicketPrice = ticketPrice;
            _currentState = new IdleState();
        }

        public void SetState(IState state)
        {
            _currentState = state;
        }

        public void SelectTicket()
        {
            _currentState.SelectTicket(this);
        }

        public void InsertMoney(decimal amount)
        {
            _currentState.InsertMoney(this, amount);
        }

        public void DispenseTicket()
        {
            _currentState.DispenseTicket(this);
        }

        public void CancelTransaction()
        {
            _currentState.CancelTransaction(this);
        }
    }

    // Основной класс для демонстрации работы автомата
    class Program
    {
        static void Main(string[] args)
        {
            TicketMachine machine = new TicketMachine(100);

            // Демонстрация работы автомата
            machine.SelectTicket();
            machine.InsertMoney(50);
            machine.InsertMoney(100);
            machine.DispenseTicket();
            machine.CancelTransaction();
        }
    }
}
