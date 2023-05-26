using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public  static class GameState
    {
        public static GameSettings Settings { get; set; }
        public static void Reset()
        {
            Settings = null;
        }

    }
}
