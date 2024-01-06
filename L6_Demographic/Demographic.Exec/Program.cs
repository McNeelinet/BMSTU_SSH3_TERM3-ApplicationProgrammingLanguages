using Demographic;
using Demographic.FileOperations;
using Demographic.Tables;


class Program
{
    static void Main(string[] args)
    {
        InitialAgeTable initialAgeTable;
        DeathRulesTable deathRulesTable;
        RelationshipsRulesTable relationshipsRulesTable;

        using (var sr = new StreamReader(args[0]))
        {
            initialAgeTable = CSVReader.ReadInitialAgeTable(sr);
        }
        
        using (var sr = new StreamReader(args[1]))
        {
            deathRulesTable = CSVReader.ReadDeathRulesTable(sr);
        }
        
        using (var sr = new StreamReader(args[2]))
        {
            relationshipsRulesTable = CSVReader.ReadrRelationshipsRulesTable(sr);
        }
        
        var engine = new Engine(deathRulesTable, relationshipsRulesTable, int.Parse(args[3]), int.Parse(args[4]));
        engine.CreatePopulation(initialAgeTable, int.Parse(args[5]));
        engine.Execute();
        
        CSVWriter.WriteAgeCount("MaleAgeDistribution.csv", engine.MaleAgeDistribution);
        CSVWriter.WriteAgeCount("FemaleAgeDistribution.csv", engine.FemaleAgeDistribution);
        CSVWriter.WriteYearCount("MalesDynamic.csv", engine.MaleDynamic);
        CSVWriter.WriteYearCount("FemalesDynamic.csv", engine.FemaleDynamic);
    }
}
