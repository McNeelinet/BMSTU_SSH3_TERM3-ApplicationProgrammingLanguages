namespace PresentationLayer.Models;

public class SpecializationViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public void PrintTable()
    {
        Console.WriteLine($"Name: {Name ?? "<empty>"}");
    }
}