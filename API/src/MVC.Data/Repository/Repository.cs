using Microsoft.EntityFrameworkCore;
using MVC.Business.Interfaces;
using MVC.Business.Models;
using MVC.Data.Context;
using System.Linq.Expressions;

namespace MVC.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly MeuDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(MeuDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> expression)
            => await DbSet.AsNoTrackingWithIdentityResolution().Where(expression).ToListAsync();

        public virtual async Task<TEntity> ObterPorId(Guid id)
            => await DbSet.FindAsync(id);

        public virtual async Task<List<TEntity>> ObterTodos()
            => await DbSet.ToListAsync();

        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remover(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
            => await Context.SaveChangesAsync();

        public void Dispose()
            => Context?.Dispose();
    }
}

