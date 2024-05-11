using System.Linq.Expressions;

namespace Contracts.Base;

public interface IRepositoryRead<T>
{
    IQueryable<T> GetAll(bool trackChanges);
    IQueryable<T> GetByCondition(Expression<Func<T, bool>> condition, bool trackChanges);
}
