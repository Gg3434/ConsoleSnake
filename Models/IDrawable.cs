using System;
using System.Collections.Generic;

namespace Snake.Model
{
    public interface IDrawable
    {
        public ConsoleColor Color { get; set; }
        public List<Position> GetPositions();
        public char Pixel { get; set; }
       
    }
}
