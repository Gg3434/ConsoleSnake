using Snake.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Bomb : IDrawable
    {
        public ConsoleColor Color { get; set; } = ConsoleColor.Red;
        public Position Position { get; private set; }
        public char Pixel { get; set ; } = 'x';

        public Bomb(Position position) 
        {
            Position = position;
        }
        public void ReLocate(Position position)
        {
            Position = position;
        }
        public List<Position> GetPositions()
        {
            return new List<Position>() { Position };
        } 
    }
}
