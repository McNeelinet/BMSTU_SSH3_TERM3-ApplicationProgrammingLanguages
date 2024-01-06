using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Infrastructure;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;

namespace BusinessLogicLayer.Services;

public class HospitalService : IHospitalService
{
    private IUnitOfWork Database { get; set; } = new EFUnitOfWork();

    public IEnumerable<SpecializationDTO> GetSpecializations()
    {
        var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Specialization, SpecializationDTO>()).CreateMapper();
        return mapper.Map<IEnumerable<Specialization>, List<SpecializationDTO>>(Database.Specializations.GetAll());
    }
    
    public IEnumerable<DoctorDTO> GetDoctors()
    {
        var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Doctor, DoctorDTO>()).CreateMapper();
        return mapper.Map<IEnumerable<Doctor>, List<DoctorDTO>>(Database.Doctors.GetAll());
    }

    public IEnumerable<CertificateDTO> GetCertificates()
    {
        var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Certificate, CertificateDTO>()).CreateMapper();
        return mapper.Map<IEnumerable<Certificate>, List<CertificateDTO>>(Database.Certificates.GetAll());
    }

    public void AddSpecialization(SpecializationDTO specializationDto)
    {
        ValidateSpecializationDto(specializationDto);
        var specialization = new Specialization
        {
            Name = specializationDto.Name
        };
        Database.Specializations.Create(specialization);
        Database.Save();
    }
    
    public void AddDoctor(DoctorDTO doctorDto)
    {
        ValidateDoctorDto(doctorDto);
        var doctor = new Doctor
        {
            SpecializationId = doctorDto.SpecializationId,
            Name = doctorDto.Name
        };
        Database.Doctors.Create(doctor);
        Database.Save();
    }

    public void AddCertificate(CertificateDTO certificateDto)
    {
        ValidateCertificateDto(certificateDto);
        var certificate = new Certificate
        {
            DoctorId = certificateDto.DoctorId,
            Description = certificateDto.Description,
            Date = certificateDto.Date
        };
        Database.Certificates.Create(certificate);
        Database.Save();
    }

    public void UpdateSpecialization(int id, SpecializationDTO specializationDto)
    {
        ValidateSpecializationDto(specializationDto);
        var specialization = Database.Specializations.Get(id);
        if (specialization is null)
            throw new InvalidOperationException("Нет специализации с таким id");
        specialization.Name = specializationDto.Name;
        
        Database.Specializations.Update(specialization);
        Database.Save();
    }
    
    public void UpdateDoctor(int id, DoctorDTO doctorDto)
    {
        ValidateDoctorDto(doctorDto);
        var doctor = Database.Doctors.Get(id);
        if (doctor is null)
            throw new InvalidOperationException("Нет доктора с таким id");
        doctor.SpecializationId = doctorDto.SpecializationId;
        doctor.Name = doctorDto.Name;
        
        Database.Doctors.Update(doctor);
        Database.Save();
    }

    public void UpdateCertificate(int id, CertificateDTO certificateDto)
    {
        ValidateCertificateDto(certificateDto);
        var certificate = Database.Certificates.Get(id);
        if (certificate is null)
            throw new InvalidOperationException("Нет сертификата с таким id");
        certificate.DoctorId = certificateDto.DoctorId;
        certificate.Description = certificateDto.Description;
        certificate.Date = certificateDto.Date;
        
        Database.Certificates.Update(certificate);
        Database.Save();
    }

    public void DeleteSpecialization(int id)
    {
        Database.Specializations.Delete(id);
        Database.Save();
    }

    public void DeleteDoctor(int id)
    {
        Database.Doctors.Delete(id);
        Database.Save();
    }

    public void DeleteCertificate(int id)
    {
        Database.Certificates.Delete(id);
        Database.Save();
    }

    public int GetDoctorsCountBySpecializationId(int id)
    {
        return Database.Doctors
            .GetAll()
            .Count(d => d.SpecializationId == id);
    }

    public string GetSpecializationNameByCertificateId(int id)
    {
        var certificate = Database.Certificates.Get(id);
        if (certificate is null)
            throw new DTOValidationException("Сертификат не найден", "");
        
        var doctor = Database.Doctors.Get(certificate.DoctorId);
        if (doctor is null)
            throw new DTOValidationException("Доктор не найден", "");
        
        var specialization = Database.Specializations.Get(doctor.SpecializationId);
        if (specialization is null)
            throw new DTOValidationException("Специализация не найдена", "");
        
        return specialization.Name!;
    }

    public CertificateDTO GetLastCertificate()
    {
        var certificates = GetCertificates()
            .OrderByDescending(c => c.Date.Date)
            .ThenBy(c => c.Date.TimeOfDay);
        if (!certificates.Any())
            throw new DTOValidationException("Список сертификатов пуст", "");

        var certificate = certificates.First();
        return new CertificateDTO
        {
            Id = certificate.Id,
            DoctorId = certificate.DoctorId,
            Description = certificate.Description,
            Date = certificate.Date
        };
    }

    private void ValidateSpecializationDto(SpecializationDTO specializationDto)
    {
        if (specializationDto.Name is null)
            throw new DTOValidationException("Имеется незаполненное поле", "Name");
    }

    private void ValidateDoctorDto(DoctorDTO doctorDto)
    {
        if (Database.Specializations.Get(doctorDto.SpecializationId) is null)
            throw new DTOValidationException("Невалидное значение поля", "SpecializationId");
        if (doctorDto.Name is null)
            throw new DTOValidationException("Имеется незаполненное поле", "Name");
    }

    private void ValidateCertificateDto(CertificateDTO certificateDto)
    {
        if (Database.Doctors.Get(certificateDto.DoctorId) is null)
            throw new DTOValidationException("Невалидное значение поля", "DoctorId");
        if (certificateDto.Description is null)
            throw new DTOValidationException("Имеется незаполненное поле", "Description");
    }

    public void Dispose()
    {
        Database.Dispose();
    }
}