namespace MotoRental.UseCases.Base;

public interface IBaseUseCase<T>
{
    Task<IEnumerable<T>?> FindAll();
    Task<T> FindById(int id);
    Task<int> Save(T obj);
    Task<bool> Update(T obj);
    Task<bool> Delete(T obj);
}