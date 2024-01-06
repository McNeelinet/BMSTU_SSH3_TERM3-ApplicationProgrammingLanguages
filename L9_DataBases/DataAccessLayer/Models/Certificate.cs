namespace DataAccessLayer.Models;

public class Certificate
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public Doctor? Doctor { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
}
