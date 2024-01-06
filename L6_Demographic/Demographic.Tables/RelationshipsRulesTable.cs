namespace Demographic.Tables;

public class RelationshipsRulesTable : List<RelationshipsRulesRow>
{
    public double GetFindPartnerProbability(int age)
    {
        foreach (var row in this.Where(row => row.StartAgeRange <= age && age <= row.EndAgeRange))
        {
            return row.FindPartnerProbability;
        }
        return 0.0;
    }
    
    public double GetLosePartnerProbability(int age)
    {
        foreach (var row in this.Where(row => row.StartAgeRange <= age && age <= row.EndAgeRange))
        {
            return row.LosePartnerProbability;
        }
        return 1.0;
    }
}