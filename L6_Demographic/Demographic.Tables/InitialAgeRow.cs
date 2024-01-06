namespace Demographic.Tables;

public struct InitialAgeRow
{
    private int _age;
    private double _personsPerHundred;

    public InitialAgeRow(int age, double personsPerHundred)
    {
        Age = age;
        PersonsPerHundred = personsPerHundred;
    }

    public int Age
    {
        get => _age;
        set
        {
            if (value >= 0)
                _age = value;
        }
    }

    public double PersonsPerHundred
    {
        get => _personsPerHundred;
        set
        {
            if (value >= 0)
                _personsPerHundred = value;
        }
    }
}