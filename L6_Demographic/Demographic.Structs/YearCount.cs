namespace Demographic.Structs;

public struct YearCount
{
    public YearCount(int age, int count)
    {
        Year = age;
        Count = count;
    }

    public int Year { get; set; }

    public int Count { get; set; }
}