using System;  // импортирует пространство имен System, которое содержит основные типы и классы .NET Framework.

namespace Snake
{
    class Program
    {
        static void Main()
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight); // устанавливается размер окна консоли равным наибольшему доступному размеру по ширине и высоте.
            Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight); // устанавливается размер буфера консоли равным наибольшему доступному размеру по ширине и высоте. 
            var game = new Game();
            game.Run();
        }
    }
}
