using DataAccessLayer.Context;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories;


public class EFUnitOfWork : IUnitOfWork
{
    private readonly HospitalDatabaseContext _dbContext = new();
    private SpecializationRepository? _specializationRepository;
    private DoctorRepository? _doctorRepository;
    private CertificateRepository? _certificateRepository;

    public IRepository<Specialization> Specializations
    {
        get
        {
            if (_specializationRepository is null)
                _specializationRepository = new SpecializationRepository(_dbContext);
            return _specializationRepository;
        }
    }
    
    public IRepository<Doctor> Doctors
    {
        get
        {
            if (_doctorRepository is null)
                _doctorRepository = new DoctorRepository(_dbContext);
            return _doctorRepository;
        }
    }
    
    public IRepository<Certificate> Certificates
    {
        get
        {
            if (_certificateRepository is null)
                _certificateRepository = new CertificateRepository(_dbContext);
            return _certificateRepository;
        }
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }

    public void Dispose()
    {
        throw new NotImplementedException();  // НЕ ЗАБЫТЬ
    }
}