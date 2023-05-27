using Snake.Model;
using System;  // импортирует пространство имен System, которое содержит основные типы и классы .NET Framework.

namespace Snake
{
    public class InputControl
    {
        public Direction? GetCurrentDirection(ConsoleKey key) // обьявление перечисления для установки клавиш управления змейкой
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
