using Demographic.Structs;
using Demographic.Tables;

namespace Demographic;

public interface IEngine
{
    public List<YearCount> MaleDynamic { get; }
    public List<YearCount> FemaleDynamic { get; }
    
    public List<AgeCount> MaleAgeDistribution { get; }
    
    public List<AgeCount> FemaleAgeDistribution { get; }

    public void CreatePopulation(InitialAgeTable initialAgeTable, int population);

    public void Execute();
}