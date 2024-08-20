namespace MotoRental.Repositories.Base;

public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>?> FindAll();
    Task<T> FindById(int id);
    Task<int> Save(T obj);
    Task<bool> Update(T obj);
    Task<bool> Delete(T obj);
    
}
