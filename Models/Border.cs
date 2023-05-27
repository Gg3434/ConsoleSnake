using System;
using System.Collections.Generic; // импортирует пространство имен  которое содержит обобщенные типы и интерфейсы, такие как List<T>
using System.Linq; 

namespace Snake.Model
{
    public class Border : IDrawable
    {
        public HashSet<Position> BorderPositions { get; private set; } // свойство которое хранит набор позиций границы 
        public ConsoleColor Color { get; set; } = ConsoleColor.White; // установка цвета границы
        public char Pixel { get; set; } = '#'; // установка пикселя для отрисовки границы

        public Border(int width, int height, int obstacleData) // конструктор принимает ширину высоту и целое число которое указывает какое препятствие должно быть добавлено
        {
            BorderPositions = CreateBorder(width, height);
            var obstacle = DrawObstacle(width, height, obstacleData);  // Создание препятствия с помощью метода DrawObstacle и добавление его позиций
                                                                       // в набор BorderPositions с помощью метода UnionWith.
            BorderPositions.UnionWith(obstacle);
        }

        private HashSet<Position> CreateBorder(int width, int height) // метод создает границу, добавляя позиции внешних ячеек игрового поля в HashSet<Position>.
        {
            var border = new HashSet<Position>();

            for (int i = 0; i < width; i++) // получение всех крайних точек поля
            {
                border.Add(new Position(i, 0)); // добавляем верхнюю границу поля
                border.Add(new Position(i, height - 1)); // добавляем нижнюю границу поля
            }

            for (int i = 0; i < height; i++) // получение всех крайних точек поля
            {
                border.Add(new Position(0, i)); // добавляем верхнюю границу поля
                border.Add(new Position(width - 1, i)); // добавляем нижнюю границу поля
            }

            return border;
        }

        private HashSet<Position> DrawObstacle(int width, int height, int obstacleData) // метод который принимает ширину и высоту и номер сложности препятствия
                                                                                        
        {
            HashSet<Position> obstacle = new(); // создание пустого множества которое будет содержать препятствие
            var center = new Position(width / 2, height / 2);

            switch (obstacleData)
            {
                case 1:
                    for (int i = -6; i < 7; i++)
                    {
                        obstacle.Add(new Position(center.X + 5, center.Y + i)); // Каждый символ представлен как объект Position, у которого координата
                                                                                // в зависимости от того, находится ли он справа или слева от центра, а координата y меняется в цикле.
                        obstacle.Add(new Position(center.X - 5, center.Y - i));
                    }
                    break;

                case 2:
                    for (int i = -6; i < 7; i++)
                    {
                        obstacle.Add(new Position(center.X + i, center.Y + 15));
                        obstacle.Add(new Position(center.X + i, center.Y - 15));
                    }
                    break;

                case 3:
                    for (int i = -6; i < 7; i++)
                    {
                        obstacle.Add(new Position(center.X + i, center.Y + 20));
                        obstacle.Add(new Position(center.X + i, center.Y - 20));

                        obstacle.Add(new Position(center.X + 20, center.Y + i));
                        obstacle.Add(new Position(center.X - 20, center.Y + i));
                    }
                    break;
            }

            return obstacle;
        }

        public List<Position> GetPositions() // метод получения позиции
        {
            return BorderPositions.ToList(); // возвращает список позиций границы для отображения на экране. 
        }
    }
}


