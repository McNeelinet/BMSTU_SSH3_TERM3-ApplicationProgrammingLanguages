using DataAccessLayer.Models;

namespace DataAccessLayer.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<Specialization> Specializations { get; }
    IRepository<Doctor> Doctors { get; }
    IRepository<Certificate> Certificates { get; }
    void Save();
}