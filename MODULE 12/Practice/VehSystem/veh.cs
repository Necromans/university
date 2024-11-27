public enum OrderState
{
    Idle,
    CarSelected,
    OrderConfirmed,
    CarArrived,
    InTrip,
    TripCompleted,
    WaitingPay,
    TripCancelled
}

public class OrderManager
{
    private OrderState _currentState;
    public OrderState CurrentState
    {
        get { return _currentState; }
        private set { _currentState = value; }
    }

    public OrderManager()
    {
        _currentState = OrderState.Idle;
    }

    public void SelectCar()
    {
        if (_currentState == OrderState.Idle)
        {
            _currentState = OrderState.CarSelected;
            Console.WriteLine("Автомобиль выбран.");
        }
        else
        {
            Console.WriteLine("Невозможно выбрать автомобиль на данном этапе.");
        }
    }

    public void ConfirmOrder()
    {
        if (_currentState == OrderState.CarSelected)
        {
            _currentState = OrderState.OrderConfirmed;
            Console.WriteLine("Заказ подтвержден.");
        }
        else
        {
            Console.WriteLine("Невозможно подтвердить заказ на данном этапе.");
        }
    }

    public void CarArrives()
    {
        if (_currentState == OrderState.OrderConfirmed)
        {
            _currentState = OrderState.CarArrived;
            Console.WriteLine("Автомобиль прибыл.");
        }
        else
        {
            Console.WriteLine("Невозможно, чтобы автомобиль прибыл на данном этапе.");
        }
    }

    public void StartTrip()
    {
        if (_currentState == OrderState.CarArrived)
        {
            _currentState = OrderState.InTrip;
            Console.WriteLine("Поездка началась.");
        }
        else
        {
            Console.WriteLine("Невозможно начать поездку на данном этапе.");
        }
    }

    public void CompleteTrip()
    {
        if (_currentState == OrderState.InTrip)
        {
            _currentState = OrderState.TripCompleted;
            Console.WriteLine("Поездка завершена.");
        }
        else
        {
            Console.WriteLine("Невозможно завершить поездку на данном этапе.");
        }
    }

    public void WaitPay()
    {
        if (_currentState == OrderState.TripCompleted)
        {
            _currentState = OrderState.WaitingPay;
            Console.WriteLine("Поездка оплачена");
        }
    }

    public void CancelTrip()
    {
        if (_currentState == OrderState.WaitingPay)
        {
            _currentState = OrderState.TripCancelled;
            Console.WriteLine("Поездка отменена.");
            SetIdle();
        }
    }

    public void SetIdle()
    {
        _currentState = OrderState.Idle;
        Console.WriteLine("Idle");
    }
}

class Program
{
    static void Main()
    {
        var orderManager = new OrderManager();

        // Состояние Idle
        orderManager.SelectCar();  // Переход в состояние CarSelected
        orderManager.ConfirmOrder();  // Переход в состояние OrderConfirmed
        orderManager.CarArrives();  // Переход в состояние CarArrived
        orderManager.StartTrip();  // Переход в состояние InTrip
        orderManager.CompleteTrip();  // Переход в состояние TripCompleted
        orderManager.WaitPay(); //Переход в состояние WaitingPay
        // Отмена заказа
        orderManager.CancelTrip();  // Переход в состояние TripCancelled
    }
}
