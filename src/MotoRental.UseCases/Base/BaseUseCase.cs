using MotoRental.Repositories.Base;

namespace MotoRental.UseCases.Base;

public abstract class BaseUseCase<T> where T : class
{
    private readonly IBaseRepository<T> _repository;

    protected BaseUseCase(IBaseRepository<T> repository)
    {
        _repository = repository;
    }

    public virtual async Task<bool> Delete(T obj)
    {
        return await _repository.Delete(obj);
    }

    public virtual async Task<IEnumerable<T>?> FindAll()
    {
        var response = await _repository.FindAll();

        return response;

    }

    public virtual async Task<T> FindById(int id)
    {
        var response = await _repository.FindById(id);

        return response;
    }

    public virtual async Task<int> Save(T request)
    {
        return await _repository.Save(request);
    }

    public virtual async Task<bool> Update(T content)
    {
        var response = await _repository.FindById(GetIdValue(content));

        var obj = SetSourceValueIfContentValueNull(content, response);

        return await _repository.Update(obj);

    }

    private static int GetIdValue(T content)
    {
        int id = 0;
        var type = content.GetType();
        var property = type.GetProperty("Id");
        if (!(property is null))
        {
            id = Convert.ToInt32(property?.GetValue(content));
        }
        return id;
    }

    private static T SetSourceValueIfContentValueNull(T content, T source)
    {
        var typeContent = content.GetType();
        var response = content;

        foreach (var property in typeContent.GetProperties())
        {
            var valueContent = property.GetValue(content);
            var valueSource = property.GetValue(source);

            if (valueContent is null)
            {
                property.SetValue(response, valueSource);
            }
        }

        return response;
    }
}