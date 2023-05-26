using Snake;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Leaderboard
{
    private readonly string _leaderboardFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "leaderboard.txt");

    private List<LeaderboardRecord> _records;

    public Leaderboard(string filePath = null)
    {
        if (!string.IsNullOrEmpty(filePath))
        {
            _leaderboardFilePath = filePath;
        }

        _records = LoadRecords();
    }

    private List<LeaderboardRecord> LoadRecords()
    {
        var records = new List<LeaderboardRecord>();

        if (!File.Exists(_leaderboardFilePath))
        {
            return records;
        }

        using (var fileStream = new FileStream(_leaderboardFilePath, FileMode.OpenOrCreate, FileAccess.Read))
        {
            using (var streamReader = new StreamReader(fileStream))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 4 && int.TryParse(parts[1], out int score))
                    {
                        records.Add(new LeaderboardRecord(parts[0], score, parts[2], int.Parse(parts[3])));
                    }
                }
            }
        }

        return records;
    }

    public List<LeaderboardRecord> GetRecords()
    {
        return _records.OrderByDescending(r => r.Score).Take(9).ToList();
    }

    public void AddRecord(LeaderboardRecord record)
    {
        _records.Add(record);
        _records = _records.OrderByDescending(r => r.Score).Take(9).ToList();
        SaveRecords();
    }

    private void SaveRecords()
    {
        using (var fileStream = new FileStream(_leaderboardFilePath, FileMode.Create, FileAccess.Write))
        {
            using (var streamWriter = new StreamWriter(fileStream))
            {
                foreach (var record in _records)
                {
                    streamWriter.WriteLine($"{record.NamePlayer},{record.Score},{GameState.Settings.mapSize},{GameState.Settings.speedSnake}");

                }
            }
        }
    }
}
