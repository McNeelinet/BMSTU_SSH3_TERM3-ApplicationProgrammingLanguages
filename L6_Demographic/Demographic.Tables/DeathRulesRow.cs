namespace Demographic.Tables;

public struct DeathRulesRow
{
    private int _startAgeRange;
    private int _endAgeRange;
    private double _maleDeathProbability;
    private double _femaleDeathProbability;

    public DeathRulesRow(int startAgeRange, int endAgeRange, double maleDeathProbability, double femaleDeathProbability)
    {
        StartAgeRange = startAgeRange;
        EndAgeRange = endAgeRange;
        MaleDeathProbability = maleDeathProbability;
        FemaleDeathProbability = femaleDeathProbability;
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

    public double MaleDeathProbability
    {
        get => _maleDeathProbability;
        set
        {
            if (value > 0)
                _maleDeathProbability = value;
        }
    }

    public double FemaleDeathProbability
    {
        get => _femaleDeathProbability;
        set
        {
            if (value > 0)
                _femaleDeathProbability = value;
        }
    }
}