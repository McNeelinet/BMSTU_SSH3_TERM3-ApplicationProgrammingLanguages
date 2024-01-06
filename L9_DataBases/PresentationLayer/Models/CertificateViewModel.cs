namespace PresentationLayer.Models;

public class CertificateViewModel
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    
    public void PrintTable()
    {
        Console.WriteLine($"DoctorId: {DoctorId}");
        Console.WriteLine($"Description: {Description ?? "<empty>"}");
        Console.WriteLine($"Date: {Date.Date}");
    }
}