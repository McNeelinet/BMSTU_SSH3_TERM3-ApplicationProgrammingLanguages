namespace DataAccessLayer.Models;

public class Doctor
{
    public int Id { get; set; }
    public int SpecializationId { get; set; }
    public Specialization? Specialization { get; set; }
    public string? Name { get; set; }

    public List<Certificate>? Certificates { get; set; }
}
