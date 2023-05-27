using System;  // импортирует пространство имен System, которое содержит основные типы и классы .NET Framework.
using System.Collections.Generic; // импортирует пространство имен  которое содержит обобщенные типы и интерфейсы, такие как List<T>
using System.Linq; // импортируется пространство имен System.Linq, которое содержит расширения LINQ  для работы с коллекциями и запросами.
using System.Text; // импортируется пространство имен System.Text, которое содержит классы для работы с различными кодировками и символами.
using System.Threading.Tasks;  // импортируется пространство имен System.Threading.Tasks, которое содержит классы для работы с асинхронными операциями и параллельным выполнением кода.

namespace Snake
{
    public class GameSettings
    {
        public int mapSize { get; set; } // обьявление свойства размер карты
        public int speedSnake { get; set; } //обьявление свойства скорость змейки
        public string Difficulty { get; set; } // обьявление свойства сложности игры
        public int Size { get; set; } 
        public GameSettings() { }
        public void  ChoiceDifficulty() // метод который вызывает меню выбора сложности
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
        public int GetValidInput() // метод который проверяет валидно ли значения выбранной сложности игроком
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
