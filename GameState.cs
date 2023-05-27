using System;  // импортирует пространство имен System, которое содержит основные типы и классы .NET Framework.
using System.Collections.Generic; // импортирует пространство имен  которое содержит обобщенные типы и интерфейсы, такие как List<T>
using System.Linq; // импортируется пространство имен System.Linq, которое содержит расширения LINQ  для работы с коллекциями и запросами.
using System.Text; // импортируется пространство имен System.Text, которое содержит классы для работы с различными кодировками и символами.
using System.Threading.Tasks; // импортирует пространство имен System.Threading.Tasks, которое содержит типы для работы с многопоточностью и синхронизацией.

namespace Snake
{
    public  static class GameState
    {
        public static GameSettings Settings { get; set; } // обьявление свойства игровых настроек
        public static void Reset()  // метод который сбрасывает состояние на null
        {
            Settings = null;
        }

    }
}
