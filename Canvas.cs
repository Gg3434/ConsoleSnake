using Snake.Model;
using System;

namespace Snake
{
    public class Canvas
    {
        private const char EmptyPixel = ' ';
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Canvas() : this(30, 30) { }
        public Canvas(int width, int height)
        {
           

            Console.Clear(); 
            Console.SetWindowSize(width, height); // установка размера консоли
            Console.SetBufferSize(width, height); // установка размера буфера
            Console.CursorVisible = false;  // чтобы не было мигающего курсора на консоли(делает курсор невидимым)

            Width = width;
            Height = height;
        }

        public void DrawObject(IDrawable obj) // Метод который принимает объект, реализующий интерфейс IDrawable, и отрисовывает его на консоли
        {
            var positions = obj.GetPositions(); // мы вызываем метод обьекта который передали и получаем все позиции
            foreach (var pos in positions)
                DrawPixel(pos, obj.Color, obj.Pixel); // отрисовка пикселей
           
        }
        
        public void UnDrawObject(IDrawable obj) //  Метод который принимает объект, реализующий интерфейс IDrawable, и стирает его с консоли
        {
            var positions = obj.GetPositions();
            foreach (var pos in positions)
                UnDrawPixel(pos);
        }
        public void DrawPixel(Position position, ConsoleColor color, char pixelBomb)  // Метод  который принимает позицию и цвет и отрисовывает пиксель на консоли:
        {
            Console.ForegroundColor = color; 
            Console.SetCursorPosition(position.X, position.Y); // устанавливаю курсор
            Console.Write(pixelBomb); // передаю пиксель
            Console.ResetColor(); // сброс цвета
        }
        
        public void UnDrawPixel(Position position) //  Метод  который принимает позицию и стирает пиксель с консоли:
        {
            Console.SetCursorPosition(position.X, position.Y);
            Console.Write(EmptyPixel);
        }
        public static void DrawCenteredString(string text)
        {
            int y = Console.WindowHeight / 2;
            int x = (Console.WindowWidth - text.Length) / 2;
            Console.SetCursorPosition(x, y);
            Console.Write(text);
        }
    }
}
