using System;
using System.Collections.Generic;

namespace Snake.Model
{
    public class Food : IDrawable
    {
        public Position Position { get; private set; }
        public ConsoleColor Color { get; set; } = ConsoleColor.Yellow;
        public char Pixel { get; set; } = '$';
              
        public Food(Position position)
        {
            Position = position;
        }

        public void ReLocate(Position position)
        {
            Position = position;
        }

        public List<Position> GetPositions()
        {
            return new List<Position> { Position };
        }
    }
}
