using MotoRental.Borders.Helpers;
using MotoRental.Repositories.Base;

namespace MotoRental.UseCases.Base;

public abstract class BaseUseCase<T>(IBaseRepository<T> repository)
    where T : class
{
    public virtual async Task<bool> Delete(T obj)
    {
        return await repository.Delete(obj);
    }

    public virtual async Task<IEnumerable<T>?> FindAll()
    {
        return await repository.FindAll();
    }

    public virtual async Task<T> FindById(int id)
    {
        return await repository.FindById(id);
    }

    public virtual async Task<int> Save(T request)
    {
        return await repository.Save(request);
    }

    public virtual async Task<bool> Update(T content)
    {
        var response = await repository.FindById(content.GetIdValue());
        var obj = DapperHelper.SetSourceValueIfContentValueNull(content, response);

        return await repository.Update(obj);
    }
    
}