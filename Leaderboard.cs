using Snake; // импортирует пространство имен Snake, чтобы использовать классы из этого пространства имен в текущем файле.
using System;  // импортирует пространство имен System, которое содержит основные типы и классы .NET Framework.
using System.Collections.Generic; // импортирует пространство имен  которое содержит обобщенные типы и интерфейсы, такие как List<T>
using System.IO; // импортирует пространство имен, которое содержит типы, предназначенные для чтения и записи данных из файлов и потоков.
using System.Linq; // // импортируется пространство имен System.Linq, которое содержит расширения LINQ  для работы с коллекциями и запросами.

public class Leaderboard
{
    private readonly string _leaderboardFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "leaderboard.txt");
    // определяется приватное только для чтения поле _leaderboardFilePath, которое содержит путь к файлу лидерборда
    private List<LeaderboardRecord> _records; // объявляется приватное поле _records типа List<LeaderboardRecord>, которое будет содержать записи лидерборда.

    public Leaderboard(string filePath = null) // обьявление публичного конструктора который принимает путь к файлу
    {
        if (!string.IsNullOrEmpty(filePath)) // проверка является ли файл пустым либо null
        {
            _leaderboardFilePath = filePath;
        }

        _records = LoadRecords(); // результат записи таблицы из файла в переменную _records
    }

    private List<LeaderboardRecord> LoadRecords() // обьявление приватного метода который загружает записи таблицы из файла
    {
        var records = new List<LeaderboardRecord>(); // создается пустой список для хранения записей

        if (!File.Exists(_leaderboardFilePath))
        {
            return records;
        }

        using (var fileStream = new FileStream(_leaderboardFilePath, FileMode.OpenOrCreate, FileAccess.Read)) // открытие файла в режиме чтения 
        {
            using (var streamReader = new StreamReader(fileStream)) // открытие потока для чтения файла 
            {
                string line; // длина файла
                while ((line = streamReader.ReadLine()) != null) // считывание строки из файла пока длина != null
                {
                    var parts = line.Split(',');
                    if (parts.Length == 4 && int.TryParse(parts[1], out int score)) // проверка если массив содержит 4 элемента и 2 элемент можно преобразовать в число score
                    {
                        records.Add(new LeaderboardRecord(parts[0], score, parts[2], int.Parse(parts[3]))); // создание нового обьекта таблицы лидеров и добавление в records
                    }
                }
            }
        }

        return records;
    }

    public List<LeaderboardRecord> GetRecords() // публичный метод  который возвращает список записей таблицы лидеров
    {
        return _records.OrderByDescending(r => r.Score).Take(9).ToList();
    }

    public void AddRecord(LeaderboardRecord record) // публичный метод который принимает список рекордов
    {
        _records.Add(record);
        _records = _records.OrderByDescending(r => r.Score).Take(9).ToList();
        SaveRecords();
    }

    private void SaveRecords() // приватный метод который сохраняет данные таблицы лидеров в файл
    {
        using (var fileStream = new FileStream(_leaderboardFilePath, FileMode.Create, FileAccess.Write)) // открытие файла в режиме записи
        {
            using (var streamWriter = new StreamWriter(fileStream)) // открытие потока для записи в файл
            {
                foreach (var record in _records) // происходит запись текущей записи в файл 
                {
                    streamWriter.WriteLine($"{record.NamePlayer},{record.Score},{GameState.Settings.mapSize},{GameState.Settings.speedSnake}");

                }
            }
        }
    }
}
