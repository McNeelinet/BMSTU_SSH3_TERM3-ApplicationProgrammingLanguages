namespace BusinessLogicLayer.DTO;

public class CertificateDTO
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
}