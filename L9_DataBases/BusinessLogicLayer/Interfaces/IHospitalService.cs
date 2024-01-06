using BusinessLogicLayer.DTO;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Interfaces;

public interface IHospitalService
{
    IEnumerable<SpecializationDTO> GetSpecializations();
    IEnumerable<DoctorDTO> GetDoctors();
    IEnumerable<CertificateDTO> GetCertificates();

    void AddSpecialization(SpecializationDTO specializationDto);
    void AddDoctor(DoctorDTO doctorDto);
    void AddCertificate(CertificateDTO certificateDto);

    void UpdateSpecialization(int id, SpecializationDTO specializationDto);
    void UpdateDoctor(int id, DoctorDTO doctorDto);
    void UpdateCertificate(int id, CertificateDTO certificateDto);

    void DeleteSpecialization(int id);
    void DeleteDoctor(int id);
    void DeleteCertificate(int id);

    int GetDoctorsCountBySpecializationId(int id);
    string GetSpecializationNameByCertificateId(int id);
    CertificateDTO GetLastCertificate();

    void Dispose();
}