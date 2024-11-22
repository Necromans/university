using System;

class Program
{
    // Класс TV
    class TV
    {
        public void TurnOn()
        {
            Console.WriteLine("Телевизор включен.");
        }

        public void TurnOff()
        {
            Console.WriteLine("Телевизор выключен.");
        }

        public void SetChannel(int channel)
        {
            Console.WriteLine($"Канал телевизора установлен на {channel}.");
        }
    }

    // Класс AudioSystem
    class AudioSystem
    {
        public void TurnOn()
        {
            Console.WriteLine("Аудиосистема включена.");
        }

        public void TurnOff()
        {
            Console.WriteLine("Аудиосистема выключена.");
        }

        public void SetVolume(int level)
        {
            Console.WriteLine($"Громкость аудиосистемы установлена на уровень {level}.");
        }
    }

    // Класс DVDPlayer
    class DVDPlayer
    {
        public void Play()
        {
            Console.WriteLine("DVD проигрыватель: воспроизведение начато.");
        }

        public void Pause()
        {
            Console.WriteLine("DVD проигрыватель: воспроизведение приостановлено.");
        }

        public void Stop()
        {
            Console.WriteLine("DVD проигрыватель: воспроизведение остановлено.");
        }
    }

    // Класс GameConsole
    class GameConsole
    {
        public void TurnOn()
        {
            Console.WriteLine("Игровая консоль включена.");
        }

        public void StartGame(string game)
        {
            Console.WriteLine($"Запущена игра: {game}.");
        }

        public void TurnOff()
        {
            Console.WriteLine("Игровая консоль выключена.");
        }
    }

    // Фасад HomeTheaterFacade
    class HomeTheaterFacade
    {
        private TV _tv;
        private AudioSystem _audioSystem;
        private DVDPlayer _dvdPlayer;
        private GameConsole _gameConsole;

        public HomeTheaterFacade()
        {
            _tv = new TV();
            _audioSystem = new AudioSystem();
            _dvdPlayer = new DVDPlayer();
            _gameConsole = new GameConsole();
        }

        public void WatchMovie()
        {
            Console.WriteLine("\nПодготовка системы для просмотра фильма...");
            _tv.TurnOn();
            _audioSystem.TurnOn();
            _audioSystem.SetVolume(5);
            _dvdPlayer.Play();
            Console.WriteLine("Система готова к просмотру фильма.\n");
        }

        public void PlayGame(string game)
        {
            Console.WriteLine("\nПодготовка системы для игры...");
            _tv.TurnOn();
            _audioSystem.TurnOn();
            _audioSystem.SetVolume(7);
            _gameConsole.TurnOn();
            _gameConsole.StartGame(game);
            Console.WriteLine("Система готова к игре.\n");
        }

        public void ListenToMusic()
        {
            Console.WriteLine("\nПодготовка системы для прослушивания музыки...");
            _tv.TurnOn();
            _audioSystem.TurnOn();
            _audioSystem.SetVolume(6);
            Console.WriteLine("Система готова для воспроизведения музыки.\n");
        }

        public void TurnOffSystem()
        {
            Console.WriteLine("\nВыключение всей системы...");
            _dvdPlayer.Stop();
            _audioSystem.TurnOff();
            _tv.TurnOff();
            _gameConsole.TurnOff();
            Console.WriteLine("Система выключена.\n");
        }
    }

    static void Main(string[] args)
    {
        // Создание фасада
        HomeTheaterFacade homeTheater = new HomeTheaterFacade();

        // Использование фасада
        homeTheater.WatchMovie();
        homeTheater.PlayGame("Super Mario");
        homeTheater.ListenToMusic();
        homeTheater.TurnOffSystem();
    }
}
