using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Common.Exceptions;
using TodoApp.Domain.Common;

namespace TodoApp.Application.Common.Extensions
{

    public static class DbContextExtensions
    {
        public static async Task<T> FindNonNullableAsync<T>(this DbSet<T> dbSet, Guid id, CancellationToken cancellationToken) where T : class
        {
            return await dbSet.FindAsync([id], cancellationToken)
                ?? throw new NotFoundException($"The item of type {typeof(T)} and Id: {id} was not found");
        }

        public static async Task<T> GetNonNullableByIdAsync<T>(this IQueryable<T> dbSet, Guid id, CancellationToken cancellationToken) where T : BaseEntity
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                ?? throw new NotFoundException($"The item of type {typeof(T)} and Id: {id} was not found");
        }

        public static async Task<T?> GetNullableByIdAsync<T>(this DbSet<T> dbSet, Guid id, CancellationToken cancellationToken) where T : class
        {
            return await dbSet.FindAsync([id], cancellationToken);
        }
    }
}
