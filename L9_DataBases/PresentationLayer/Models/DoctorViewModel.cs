namespace PresentationLayer.Models;

public class DoctorViewModel
{
    public int Id { get; set; }
    public int SpecializationId { get; set; }
    public string? Name { get; set; }
    
    public void PrintTable()
    {
        Console.WriteLine($"SpecializationId: {SpecializationId}");
        Console.WriteLine($"Name: {Name ?? "<empty>"}");
    }
}