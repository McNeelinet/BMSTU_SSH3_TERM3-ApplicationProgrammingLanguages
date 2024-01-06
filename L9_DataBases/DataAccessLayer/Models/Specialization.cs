namespace DataAccessLayer.Models;

public class Specialization
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public List<Doctor>? Doctors { get; set; }
}
