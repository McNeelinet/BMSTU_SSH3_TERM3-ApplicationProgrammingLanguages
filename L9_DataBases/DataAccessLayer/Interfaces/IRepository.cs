namespace DataAccessLayer.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    IEnumerable<TEntity> GetAll();
    TEntity? Get(int id);
    void Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(int id);
}