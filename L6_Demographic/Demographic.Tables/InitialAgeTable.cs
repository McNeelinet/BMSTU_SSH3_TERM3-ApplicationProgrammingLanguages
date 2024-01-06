using Demographic.Structs;

namespace Demographic.Tables;

public class InitialAgeTable : List<InitialAgeRow>
{
    public List<AgeCount> GetDistribution(int population)
    {
        var distribution = new List<AgeCount>();

        foreach (var element in this)
        {
            distribution.Add(new AgeCount(element.Age, (int)(element.PersonsPerHundred * population / 1000)));
        }

        return distribution;
    }
}