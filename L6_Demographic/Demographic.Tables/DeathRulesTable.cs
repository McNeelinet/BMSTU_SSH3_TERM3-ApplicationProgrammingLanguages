namespace Demographic.Tables;

public class DeathRulesTable : List<DeathRulesRow>
{
    public double GetDeathProbability(Genders gender, int age)
    {
        foreach (var row in this.Where(row => row.StartAgeRange <= age && age <= row.EndAgeRange))
        {
            return (gender == Genders.Male ? row.MaleDeathProbability : row.FemaleDeathProbability) / (row.EndAgeRange - row.StartAgeRange);
        }
        return 1.0;
    }
}