using System;

namespace Snake
{
    class Program
    {
        static void Main()
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            var game = new Game();
            game.Run();
        }
    }
}
