using System;
using System.Collections.Generic; // импортирует пространство имен  которое содержит обобщенные типы и интерфейсы, такие как List<T>
using System.Linq;  // импортируется пространство имен System.Linq, которое содержит расширения LINQ  для работы с коллекциями и запросами.

namespace Snake.Model
{
    public class Snake : IDrawable
    {
        public int Length { get; private set; } // длина змейки
        public Direction? CurrentDirection { get; private set; } // текущее направление змейки
        public LinkedList<Position> Body { get; private set; } // тело змейки
        public string NamePlayer { get; set; } // имя игрока

        public int Score { get; set; } // счет игрока
        public ConsoleColor Color { get; set; } = ConsoleColor.Green; // установление цвета змейки
        public char Pixel { get; set; } = 'o';

        public Action Eat; // Делегат Eat представляет действие, которое должно быть выполнено при поглощении змейкой еды.
        public Action<Bomb> Bomb;
        public Action Death;
        
        public Snake(Position position) // 
        {
            Length = 1;
            CurrentDirection = Direction.Right;

            Body = new LinkedList<Position>();
            for (int i = 0; i < Length; i++)
                Body.AddLast(new Position(position.X - i, position.Y)); // добавление кординат змейки

            Eat = EatFood;
            Bomb = EatBomb;
        }

        public void EatBomb(Bomb bomb)
        {
            var deleteLenght = Body.Count()/2;
            for (int i = 0; i < deleteLenght; i++)
                Body.RemoveLast();
            Score = Body.Count();
        }

        public void EatFood() // метод который вызывается при поглощении змейкой еды
        {
            Body.AddFirst(Body.First.Value); // добавляет новый узел содержащий заданное значение в начало linked list
            Score++;
        }

        public void Move(Direction? direction) // метод который отвечает за движение змейки
        {
            if (direction == null || IsReverse(direction)) // Проверка и корректировка направления движения змейки
                direction = CurrentDirection;

            var firstPos = Body.First.Value;
            Body.RemoveLast(); // Обновление позиции тела змейки в соответствии с направлением движения

            switch (direction)
            {
                case Direction.Up:
                    Body.AddFirst(new Position(firstPos.X, firstPos.Y - 1));
                    break;
                case Direction.Down:
                    Body.AddFirst(new Position(firstPos.X, firstPos.Y + 1));
                    break;
                case Direction.Right:
                    Body.AddFirst(new Position(firstPos.X + 1, firstPos.Y));
                    break;
                case Direction.Left:
                    Body.AddFirst(new Position(firstPos.X - 1, firstPos.Y));
                    break;
            }

            CurrentDirection = direction; // установка текущего направления
        }
        private bool IsReverse(Direction? direction) // проверка на ошибочное направление змейки
        {
            switch (direction)
            {
                case Direction.Up:
                    return CurrentDirection == Direction.Down;
                case Direction.Down:
                    return CurrentDirection == Direction.Up;
                case Direction.Right:
                    return CurrentDirection == Direction.Left;
                case Direction.Left:
                    return CurrentDirection == Direction.Right;
            }

            return false;
        }

        public bool IsEatItself()  // метод проверяет столкнулась ли змейка сама с собой
        {
            var snakeHead = Body.First.Value;
            return Body.Skip(1).Any(pos => pos.Equals(snakeHead)); // проверка находится ли голова змеейки в теле змейки
        }


        public List<Position> GetPositions() // Метод  возвращает список позиций, которые занимает тело змейки
        {
            return Body.ToList();
        }
    }
}
