using Snake.Model; // импортирует пространство имен Snake.Model, чтобы использовать классы из этого пространства имен в текущем файле.
using System; // импортирует пространство имен System, которое содержит основные типы и классы .NET Framework.
using System.Collections.Generic;
using System.Linq; // импортируется пространство имен System.Linq, которое содержит расширения LINQ  для работы с коллекциями и запросами.
using System.Text;
using System.Threading.Tasks; // импортируется пространство имен System.Threading.Tasks, которое содержит классы для работы с асинхронными операциями и параллельным выполнением кода.

namespace Snake
{
    public class Bomb : IDrawable 
    {
        public ConsoleColor Color { get; set; } = ConsoleColor.Red; // установка красного цвета свойству 
        public Position Position { get; private set; } // обьявление свойства позиции типа Position
        public char Pixel { get; set ; } = 'x'; 

        public Bomb(Position position) // конструктор класса Bomb принимающий позицию
        {
            Position = position;
        }
        public void ReLocate(Position position) // метод принимающий позицию 
        {
            Position = position;
        }
        public List<Position> GetPositions() // метод типа List<Position> для получения новой позиции
        {
            return new List<Position>() { Position };
        } 
    }
}
