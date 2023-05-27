using Snake.Model; // импортирует пространство имен Snake.Model, чтобы использовать классы из этого пространства имен в текущем файле.
using System; // импортирует пространство имен System, которое содержит основные типы и классы .NET Framework.
using System.Collections.Generic; // импортирует пространство имен  которое содержит обобщенные типы и интерфейсы, такие как List<T>
using System.Threading; // импортирует пространство имен System.Threading, которое содержит типы для работы с многопоточностью и синхронизацией.
using System.IO; // импортирует пространство имен System.IO, которое содержит типы, предназначенные для чтения и записи данных из файлов и потоков.
using Snake; // импортирует пространство имен Snake, чтобы использовать классы из этого пространства имен в текущем файле.

namespace Snake
{ 
   
    public class Game
    {
        public InputControl InputControl { get; private set; } // обьявление свойства управления
        public Canvas Canvas { get; private set; } // обьявление свойства отрисовки
        public Field Field { get; private set; } // обьявление свойства игрового поля

        private int _timeBetweenFrames; // переменная время обновления кадра
        private bool _isFinished; 
        private Leaderboard _leaderboard; // обьявление обьекта типа Leaderboard
        private readonly string _leaderboardFilePath = "leaderboard.txt"; 
       
        public Game()
        {
            var gameSettings = new GameSettings();
            gameSettings.ChoiceDifficulty();
            GameState.Settings = gameSettings;
            Init();
        }

        public void Init() // метод который  запускает игровое меню с выбором сложности
        {
            InputControl = new InputControl();
            switch (GameState.Settings.mapSize)
            {
                case 1:
                    Canvas = new Canvas(30, 30);
                    Field = new Field(30, 30, 1);
                    break;

                case 2:
                    Canvas = new Canvas(70, 70);
                    Field = new Field(70, 70, 2);
                    break;

                case 3:
                    Canvas = new Canvas(90, 90);
                    Field = new Field(90, 90, 3);
                    break;
            }
            switch (GameState.Settings.speedSnake)
            {
                case 1:
                    _timeBetweenFrames = 100;
                    break;
                case 2:
                    _timeBetweenFrames = 70;
                    break;
                case 3:
                    _timeBetweenFrames = 45;
                    break;
            }

            _isFinished = false; 

            _leaderboard = new Leaderboard(_leaderboardFilePath);
            Field.OnSnakeDeath += LevelFinished; // сррабатывает событие когда змейка умирает
            Field.OnSnakeEat += DrawNewFood; // срабатывает событие когда змейка ест еду

        }
        private void DrawNewFood() // метод который спавнит новую еду
        {
           Canvas.DrawObject(Field.Food);
        }
        private void DrawBombs() // метод который спавнит новую бомбу
        {

            for (int i = 0; i < Field.Bombs.Count; i++)
            {
                Canvas.DrawObject(Field.Bombs[i]);
            }

        }
        private void LevelFinished() // метод который вызывается после смерти змейки и выводит сообщение о законченой игре
        {
            _isFinished = true;
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Canvas.DrawCenteredString(("Game Over"));
            Thread.Sleep(2000);
            Console.WriteLine();

            Console.Write("Пожалуйста, введите свой никнейм: ");
            var playerName = Console.ReadLine();

            _leaderboard.AddRecord(new LeaderboardRecord(playerName, Field.Snake.Score, GameState.Settings.Difficulty, GameState.Settings.Size));

            DisplayLeaderboard();
            Restart();
        }

        private void DisplayLeaderboard() // метод который выводит таблицу лидеров на консоль 
        {
            Console.WriteLine("Список лидеров\n");
            var records = _leaderboard.GetRecords();
            if (records.Count == 0)
            {
                Console.WriteLine("Нет рекордов\n");
                return;
            }

            for (int i = 0; i < records.Count; i++)
            {
                var record = records[i];
                Console.WriteLine($"{i + 1}. " +
                    $"Ник: {record.NamePlayer,-15}" +
                    $"Счет: {record.Score,-10}" +
                    $"Размер карты: {GameState.Settings.mapSize,-5}" +
                    $"Сложность змейки: {GameState.Settings.speedSnake,-5}");
            }

            Console.WriteLine();
        }

        public void Run() // метод который срабатывание при запуске игры 
        {

            Console.Clear();
            AdjustConsoleWindowSize();
            Canvas.DrawObject(Field.Border);
            Canvas.DrawObject(Field.Snake);
            Canvas.DrawObject(Field.Food);
            Direction? dir = null;

            while (!_isFinished)
            {
                string scoreInfo = $"Score: {Field.Snake.Score}";
                string speedInfo = $"Speed: {GameState.Settings.speedSnake}";

                DrawBombs();
                Canvas.UnDrawObject(Field.Snake);
                if (Console.KeyAvailable)
                    dir = InputControl.GetCurrentDirection(Console.ReadKey(true).Key);

                Field.UpdateSnake(dir);

                Canvas.DrawObject(Field.Snake);

                // Очищаем и рисуем строку с информацией о счете и скорости под игровой областью
                Console.SetCursorPosition(0, Canvas.Height);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Canvas.Height);
                Console.Write(scoreInfo.PadRight(Canvas.Width / 2));
               Console.Write(speedInfo.PadLeft((Canvas.Width / 2) + (Canvas.Width % 2)));

                Thread.Sleep(_timeBetweenFrames);
            }

            Console.WriteLine();
            Restart();
        }

        private void AdjustConsoleWindowSize() // метод который обновляет размер консоли
        {
            int desiredWidth = Canvas.Width;
            int desiredHeight = Canvas.Height + 1; // Увеличение высоты консоли для отображения информации о счете
            int currentWidth = Console.WindowWidth;
            int currentHeight = Console.WindowHeight;

            if (desiredWidth > currentWidth || desiredHeight > currentHeight)
            {
                int newWidth = Math.Max(desiredWidth, currentWidth);
                int newHeight = Math.Max(desiredHeight, currentHeight);
                Console.SetWindowSize(newWidth, newHeight);
                Console.SetBufferSize(newWidth, newHeight);
            }
        }

        public void Restart() // метод который перезапускает игру
        {
            GameState.Reset();
            Console.WriteLine("Press any key to restart the game.");
            Console.ReadKey(true);
            Console.Clear();
            var gameSettings = new GameSettings();
            gameSettings.ChoiceDifficulty();
            GameState.Settings = gameSettings;
            Init();

            Run();
        }

    }

}

