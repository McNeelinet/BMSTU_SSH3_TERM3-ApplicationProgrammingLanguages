using DataAccessLayer.Context;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class DoctorRepository : IRepository<Doctor>
{
    private readonly HospitalDatabaseContext _dbContext;

    public DoctorRepository(HospitalDatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Doctor> GetAll()
    {
        return _dbContext.Doctors;
    }

    public Doctor? Get(int id)
    {
        return _dbContext.Doctors.Find(id);
    }

    public void Create(Doctor doctor)
    {
        _dbContext.Doctors.Add(doctor);
    }

    public void Update(Doctor doctor)
    {
        _dbContext.Entry(doctor).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
        var doctor = Get(id);
        if (doctor is not null)
            _dbContext.Doctors.Remove(doctor);
    }
}
