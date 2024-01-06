using DataAccessLayer.Context;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class SpecializationRepository : IRepository<Specialization>
{
    private readonly HospitalDatabaseContext _dbContext;

    public SpecializationRepository(HospitalDatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Specialization> GetAll()
    {
        return _dbContext.Specializations;
    }

    public Specialization? Get(int id)
    {
        return _dbContext.Specializations.Find(id);
    }

    public void Create(Specialization specialization)
    {
        _dbContext.Specializations.Add(specialization);
    }

    public void Update(Specialization specialization)
    {
        _dbContext.Entry(specialization).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
        var specialization = Get(id);
        if (specialization is not null)
            _dbContext.Specializations.Remove(specialization);
    }
}
