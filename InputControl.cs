using Snake.Model;
using System;

namespace Snake
{
    public class InputControl
    {
        public Direction? GetCurrentDirection(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.W:
                    return Direction.Up;
                case ConsoleKey.A:
                    return Direction.Left;
                case ConsoleKey.S:
                    return Direction.Down;
                case ConsoleKey.D:
                    return Direction.Right;
                    //
                case ConsoleKey.UpArrow:
                    return Direction.Up;

                case ConsoleKey.DownArrow:
                    return Direction.Down;

                case ConsoleKey.RightArrow:
                    return Direction.Right;

                case ConsoleKey.LeftArrow:
                    return Direction.Left;

                default:
                    return null;
            }
        }
    }
}
