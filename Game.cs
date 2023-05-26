using Snake.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using Snake;

namespace Snake
{ 
    using Snake;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    public class Game
    {
        public InputControl InputControl { get; private set; }
        public Canvas Canvas { get; private set; }
        public Field Field { get; private set; }

        private int _timeBetweenFrames;
        private bool _isFinished;
        private Leaderboard _leaderboard;
        private readonly string _leaderboardFilePath = "leaderboard.txt";
       // private int _scorePanelWidth = 15;
        public Game()
        {
            var gameSettings = new GameSettings();
            gameSettings.ChoiceDifficulty();
            GameState.Settings = gameSettings;
            Init();
        }

        public void Init()
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
            Field.OnSnakeDeath += LevelFinished;
            Field.OnSnakeEat += DrawNewFood;

        }
        private void DrawNewFood()
        {
            Canvas.DrawObject(Field.Food);
        }
        private void DrawBombs()
        {

            for (int i = 0; i < Field.Bombs.Count; i++)
            {
                Canvas.DrawObject(Field.Bombs[i]);
            }

        }
        private void LevelFinished()
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

        private void DisplayLeaderboard()
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

        public void Run()
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

        private void AdjustConsoleWindowSize()
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

        public void Restart()
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

