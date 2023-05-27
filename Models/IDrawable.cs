using System;
using System.Collections.Generic; // импортирует пространство имен  которое содержит обобщенные типы и интерфейсы, такие как List<T>

namespace Snake.Model
{
    public interface IDrawable
    {
        public ConsoleColor Color { get; set; } // свойство цвет который определен в интерфейсе
        public List<Position> GetPositions(); // метод получения позиций  который определен в интерфейсе
        public char Pixel { get; set; } // свойство пиксель который определен в интерфейсе
       
    }
}
