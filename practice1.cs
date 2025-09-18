using System;

class Program
{
    static void Main()
    {
        // Устанавливаем цвет текста - желтый
        Console.ForegroundColor = ConsoleColor.Yellow;
        
        // Устанавливаем цвет фона - синий
        Console.BackgroundColor = ConsoleColor.Blue;
        
        // Выводим текст
        Console.WriteLine("Hello World!");
        
        // Сбрасываем цвета обратно к стандартным
        Console.ResetColor();
    }
}