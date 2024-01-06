using Demographic.Structs;

namespace Demographic.FileOperations;

public class CSVWriter
{
    public static void WriteAgeCount(string filePath, List<AgeCount> ageCounts)
    {
        using var sw = new StreamWriter(filePath, false);
        
        sw.WriteLine("Age, Count");
        foreach (var item in ageCounts)
        {
            sw.WriteLine($"{item.Age}, {item.Count}");
        }
    }

    public static void WriteYearCount(string filePath, List<YearCount> yearCounts)
    {
        using var sw = new StreamWriter(filePath, false);
        
        sw.WriteLine("Year, Count");
        foreach (var item in yearCounts)
        {
            sw.WriteLine($"{item.Year}, {item.Count}");
        }
    }
}