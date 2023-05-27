using System;
using System.Collections.Generic; // импортирует пространство имен  которое содержит обобщенные типы и интерфейсы, такие как List<T>

namespace Snake.Model
{
    public class Food : IDrawable
    {
        public Position Position { get; private set; } // свойство позиции типа Position
        public ConsoleColor Color { get; set; } = ConsoleColor.Yellow; // установка желтого цвета
        public char Pixel { get; set; } = '$';
              
        public Food(Position position) // конструктор принимает и устанавливает позицию  еды
        {
            Position = position;
        }

        public void ReLocate(Position position) // метод перемещения позиции
        {
            Position = position;
        }

        public List<Position> GetPositions() // метод получения позиции
        {
            return new List<Position> { Position };
        }
    }
}
