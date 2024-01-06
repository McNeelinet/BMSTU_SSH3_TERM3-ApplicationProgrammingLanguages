namespace Demographic.Tables;

public struct RelationshipsRulesRow
{
    private int _startAgeRange;
    private int _endAgeRange;
    private double _findPartnerProbability;
    private double _losePartnerProbability;

    public RelationshipsRulesRow(int startAgeRange, int endAgeRange, double findPartnerProbability, double losePartnerProbability)
    {
        StartAgeRange = startAgeRange;
        EndAgeRange = endAgeRange;
        FindPartnerProbability = findPartnerProbability;
        LosePartnerProbability = losePartnerProbability;
    }

    public int StartAgeRange
    {
        get => _startAgeRange;
        set
        {
            if (value >= 0)
                _startAgeRange = value;
        }
    }

    public int EndAgeRange
    {
        get => _endAgeRange;
        set
        {
            if (value >= _startAgeRange)
                _endAgeRange = value;
        }
    }

    public double FindPartnerProbability
    {
        get => _findPartnerProbability;
        set
        {
            if (value > 0)
                _findPartnerProbability = value;
        }
    }

    public double LosePartnerProbability
    {
        get => _losePartnerProbability;
        set
        {
            if (value > 0)
                _losePartnerProbability = value;
        }
    }
}