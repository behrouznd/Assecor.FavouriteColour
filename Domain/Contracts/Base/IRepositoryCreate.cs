namespace Contracts.Base;

public interface IRepositoryCreate<T>
{
    T Create(T entity);
}
