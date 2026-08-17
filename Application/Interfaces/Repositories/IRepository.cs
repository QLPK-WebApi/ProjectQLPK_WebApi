
using App_QLPK.Domain.Entities;

namespace App_QLPK.Application.Interfaces;


public interface IRepository<T>
{
    Task<T?> GetByIdAsync(int id, CancellationToken ct = default);

    Task AddAsync(T entity, CancellationToken ct = default);

    Task UpdateAsync(T entity, CancellationToken ct = default);

    void DeleteAsync(T entity);
}














// public interface IRepository<T>
// {
//     Task <T?> GetByIdAsync(int id, CancellationToken ct = default);

//     Task AddAsync(T entity, CancellationToken ct = default);

//     Task DeleteAsync(T entity, CancellationToken ct = default);

//     Task UpdateAsync(T entity, CancellationToken ct = default);
// }