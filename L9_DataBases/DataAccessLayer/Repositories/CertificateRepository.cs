using DataAccessLayer.Context;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class CertificateRepository : IRepository<Certificate>
{
    private readonly HospitalDatabaseContext _dbContext;

    public CertificateRepository(HospitalDatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Certificate> GetAll()
    {
        return _dbContext.Certificates;
    }

    public Certificate? Get(int id)
    {
        return _dbContext.Certificates.Find(id);
    }

    public void Create(Certificate certificate)
    {
        _dbContext.Certificates.Add(certificate);
    }

    public void Update(Certificate certificate)
    {
        _dbContext.Entry(certificate).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
        var certificate = Get(id);
        if (certificate is not null)
            _dbContext.Certificates.Remove(certificate);
    }
}
