using System;

public class AudioSystem
{
    public void TurnOn()
    {
        Console.WriteLine("Аудиосистема включена.");
    }

    public void SetVolume(int level)
    {
        Console.WriteLine($"Громкость аудио установлена на {level}.");
    }

    public void TurnOff()
    {
        Console.WriteLine("Аудиосистема выключена.");
    }
}

public class VideoProjector
{
    public void TurnOn()
    {
        Console.WriteLine("Видеопроектор включён.");
    }

    public void SetResolution(string resolution)
    {
        Console.WriteLine($"Разрешение видео установлено на {resolution}.");
    }

    public void TurnOff()
    {
        Console.WriteLine("Видеопроектор выключен.");
    }
}

public class LightingSystem
{
    public void TurnOn()
    {
        Console.WriteLine("Освещение включено.");
    }

    public void SetBrightness(int level)
    {
        Console.WriteLine($"Яркость освещения установлена на {level}.");
    }

    public void TurnOff()
    {
        Console.WriteLine("Освещение выключено.");
    }
}

public class HomeTheaterFacade
{
    private AudioSystem _audioSystem;
    private VideoProjector _videoProjector;
    private LightingSystem _lightingSystem;

    public HomeTheaterFacade(AudioSystem audioSystem, VideoProjector videoProjector, LightingSystem lightingSystem)
    {
        _audioSystem = audioSystem;
        _videoProjector = videoProjector;
        _lightingSystem = lightingSystem;
    }

    public void StartMovie()
    {
        Console.WriteLine("Подготовка к просмотру фильма...");
        _lightingSystem.TurnOn();
        _lightingSystem.SetBrightness(5);
        _audioSystem.TurnOn();
        _audioSystem.SetVolume(8);
        _videoProjector.TurnOn();
        _videoProjector.SetResolution("HD");
        Console.WriteLine("Фильм начат.");
    }

    public void EndMovie()
    {
        Console.WriteLine("Завершение просмотра фильма...");
        _videoProjector.TurnOff();
        _audioSystem.TurnOff();
        _lightingSystem.TurnOff();
        Console.WriteLine("Просмотр фильма завершён.");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Создание подсистем
        AudioSystem audio = new AudioSystem();
        VideoProjector video = new VideoProjector();
        LightingSystem lights = new LightingSystem();

        // Создание фасада
        HomeTheaterFacade homeTheater = new HomeTheaterFacade(audio, video, lights);

        // Начало фильма
        homeTheater.StartMovie();
        Console.WriteLine();

        // Завершение фильма
        homeTheater.EndMovie();
    }
}
