namespace Demographic.Structs;

public struct AgeCount
{
    public AgeCount(int age, int count)
    {
        Age = age;
        Count = count;
    }

    public int Age { get; set; }

    public int Count { get; set; }
}