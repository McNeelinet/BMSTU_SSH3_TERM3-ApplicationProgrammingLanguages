using System.Globalization;
using Demographic.Tables;

namespace Demographic.FileOperations;

public class CSVReader
{
    public static InitialAgeTable ReadInitialAgeTable(StreamReader reader)
    {
        var table = new InitialAgeTable();

        reader.ReadLine();
        var cells = ReadCells(reader);
        while (cells != null)
        {
            var item = new InitialAgeRow
            {
                Age = int.Parse(cells[0]),
                PersonsPerHundred = double.Parse(cells[1], CultureInfo.InvariantCulture),
            };
            
            table.Add(item);
            cells = ReadCells(reader);
        }

        return table;
    }
    
    public static DeathRulesTable ReadDeathRulesTable(StreamReader reader)
    {
        var table = new DeathRulesTable();

        reader.ReadLine();
        var cells = ReadCells(reader);
        while (cells != null)
        {
            var item = new DeathRulesRow
            {
                StartAgeRange = int.Parse(cells[0]),
                EndAgeRange = int.Parse(cells[1]),
                MaleDeathProbability = double.Parse(cells[2], CultureInfo.InvariantCulture),
                FemaleDeathProbability = double.Parse(cells[3], CultureInfo.InvariantCulture)
            };
            
            table.Add(item);
            cells = ReadCells(reader);
        }

        return table;
    }
    
    public static RelationshipsRulesTable ReadrRelationshipsRulesTable(StreamReader reader)
    {
        var table = new RelationshipsRulesTable();

        reader.ReadLine();
        var cells = ReadCells(reader);
        while (cells != null)
        {
            var item = new RelationshipsRulesRow
            {
                StartAgeRange = int.Parse(cells[0]),
                EndAgeRange = int.Parse(cells[1]),
                FindPartnerProbability = double.Parse(cells[2], CultureInfo.InvariantCulture),
                LosePartnerProbability = double.Parse(cells[3], CultureInfo.InvariantCulture)
            };
            
            table.Add(item);
            cells = ReadCells(reader);
        }

        return table;
    }
    
    private static string[]? ReadCells(StreamReader reader)
    {
        var line = reader.ReadLine();
        if (line == null)
            return null;

        var cells = line
            .Split(',')
            .Select(p => p.Trim())
            .ToArray();

        return cells;
    }
}