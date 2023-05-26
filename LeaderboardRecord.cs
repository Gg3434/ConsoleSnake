using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class LeaderboardRecord
    {
        public string NamePlayer { get; private set; }
        public int Score { get; private set; }
        public string Difficulty { get; private set; }
        public int Size { get; private set; }
        public LeaderboardRecord(string namePlayer, int score, string difficulty, int size)
        {
            NamePlayer = namePlayer;
            Score = score;
            Difficulty = difficulty;
            Size = size;
        }
    }
}
