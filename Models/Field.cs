using System;
using System.Collections.Generic; // импортирует пространство имен  которое содержит обобщенные типы и интерфейсы, такие как List<T>
using System.Linq; // импортируется пространство имен System.Linq, которое содержит расширения LINQ  для работы с коллекциями и запросами.
using System.Threading;
using System.Timers;

namespace Snake.Model
{
    public class Field
    {
        public int Width { get; private set; } // ширина поля
        public int Height { get; private set; } // высота поля
        public int EmptyPositionsCount { get; private set; } // колличество пустых позиций
        public string NamePlayer { get; set; }
        public int Score { get; set; }
        public Snake Snake { get; private set; }
        public Food Food { get; private set; }
        public List<Bomb> Bombs { get; private set; } = new List<Bomb>();
        public Border Border { get; private set; } // граница поля

        public event Action OnSnakeEat;
        public event Action OnSnakeDeath;
        public event Action<Bomb> OnSnakeEatBomb ;

        private readonly Random _random; // создание поля для генерации случайных случайних чисел при определении новой позиции еды

        public Field() : this(30, 30, 1) { } // создание поля размером 30х30

        public Field(int width, int height, int obstacleData)
        {
            Width = width;
            Height = height;

            _random = new Random();
            Border = new Border(width, height, obstacleData);
            Snake = new Snake(new Position(width / 2, height / 2));
            Food = new Food(GetFreeRandomPosition());

            EmptyPositionsCount = height * width - Border.BorderPositions.Count; // находим колличество всех ячеек поля и отнимает позиции занятых ячеек поля(границы)

            OnSnakeEat += Snake.Eat;
            OnSnakeEatBomb += Snake.Bomb;
            OnSnakeEat += ReSpawnFood;
            SetTimer();
            OnSnakeEatBomb += EatBomb;

        }
        private void EatBomb(Bomb bomb)
        {
            Bombs.Remove(bomb);
        }
        private void SetTimer() // создание таймера бомб
        {
            var aTimer = new System.Timers.Timer(20000);
            aTimer.Elapsed += SpawnBomb;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void SpawnBomb(object sender, ElapsedEventArgs e) 
        {
            var bomb = new Bomb(GetFreeRandomPosition());
            Bombs.Add(bomb);
        }

        public void UpdateSnake(Direction? snakeDirection) // метод обновления состояния змейки на основе направления
        {
            Snake.Move(snakeDirection); // движение змейки в указанном направлении

            var snakeHead = Snake.Body.First.Value; // получение позиции змейки
            if (Border.BorderPositions.Any(pos => pos.Equals(snakeHead)) || Snake.IsEatItself()) /* проверяем не врезалась ли змейка в границу поля
                                                                                                  или саму в себя*/
                OnSnakeDeath?.Invoke(); //  если произошло столкновение то происходит вызов события 

            if (Bombs.Any(b => b.Position.Equals(snakeHead)))
                OnSnakeEatBomb?.Invoke(Bombs.First(b => b.Position.Equals(snakeHead))); // вызов события когда змея сьедает бомбу
            
            if (snakeHead.Equals(Food.Position))
                OnSnakeEat?.Invoke(); // вызов события когда голова находится на той же позиции где еда
                
        }

        public Position GetFreeRandomPosition() // метод получения случайной позиции на поле
        {
            var IsEmptyPosAvailable = Snake.Length != EmptyPositionsCount; // проверяем не заняла ли змейка все  свободные позиции карты
            Position freePos;
            do
            {
                freePos = new Position(
                    _random.Next(1, Width - 1),
                    _random.Next(1, Height - 1));

            } while (Snake.Body.Any(pos => pos.Equals(freePos)) || !IsEmptyPosAvailable|| Bombs.Any(b => b.Position.Equals(freePos))); // проходит генерация случ позиции и проверка не находится ли змейка на этой позиции

            return freePos; // возврат найденой свободной позиции
        }
       
        public void ReSpawnFood() // метод перемещает еду на новую случ позицию 
        {
            Food.ReLocate(GetFreeRandomPosition());
        }
    }
}
