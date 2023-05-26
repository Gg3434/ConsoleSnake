using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class GameSettings
    {
        public int mapSize { get; set; }
        public int speedSnake { get; set; }
        public string Difficulty { get; set; }
        public int Size { get; set; }
        public GameSettings() { }
        public void  ChoiceDifficulty()
        {
            Canvas.DrawCenteredString(("Меню игры"));
            Console.WriteLine("\nВыберите размер карты:");
            Console.WriteLine("1. Маленький");
            Console.WriteLine("2. Средний");
            Console.WriteLine("3. Большой");

            mapSize = GetValidInput();

            Console.WriteLine("\nВыберите сложность змейки:");
            Console.WriteLine("1. Маленькая");
            Console.WriteLine("2. Нормальная");
            Console.WriteLine("3. Сложная");
            
            speedSnake = GetValidInput();
            
        }
        public int GetValidInput()
        {
            int val;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out val) || val < 1 || val > 3)
                {
                    Console.WriteLine("Неверный ввод. Пожалуйста, выберите от 1 до 3.");
                }
            } while (val < 1 || val > 3);

            return val;
        }
    }
}
