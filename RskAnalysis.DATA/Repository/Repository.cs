
using Microsoft.EntityFrameworkCore;
using RskAnalysis.CORE.IntRepository;
using RskAnalysis.CORE.Models;
using System.Linq.Expressions;


namespace RskAnalysis.DATA.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _db;
        
        private readonly DbSet<T?> _dbSet;

        public Repository(AppDbContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
           
        }

        public async Task AddAsync(T? entity)
        {   
            await _dbSet.AddAsync(entity);//geri donus degeri icin unutturma



            _ = _db.SaveChangesAsync();
            //if (entity is BaseClass ent)
            //{
            //    DateTime now = DateTime.Now;
            //    ent.CreatedDate = now;
            //    ent.LastUpdatedDate = now;
            //    ent.CreatedUserName = "onur";
            //    ent.LastUpdatedUserName = "onur";
            //}

        }

        public async Task AddRangeAsync(IEnumerable<T?> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            //foreach (var item in entities)
            //{
            //    if (item is BaseClass ent)
            //    {
            //        DateTime now = DateTime.Now;
            //        ent.CreatedDate = now;
            //        ent.LastUpdatedDate = now;
            //        ent.CreatedUserName = "onur";
            //        ent.LastUpdatedUserName = "onur";
            //    }
            //}

        }

        public async Task<IEnumerable<T?>> Where(Expression<Func<T?, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T?>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        
        public async Task<T?> GetByIdAsync(int id)
        {            
            return await _dbSet.FindAsync(id);
        }

        public void Remove(T? entity)
        {
            _dbSet.Remove(entity);
            _ = _db.SaveChanges();
        }

        public void RemoveRange(IEnumerable<T?> entities)
        {
            _dbSet.RemoveRange(entities);
            _ = _db.SaveChanges();
        }

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T?, bool>> predicate)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate);
        }

        public T Update(T entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            _ = _db.SaveChanges();

            //if (entity is BaseClass ent)
            //{
            //    DateTime now = DateTime.Now;
            //    ent.LastUpdatedDate = now;
            //    ent.LastUpdatedUserName = "onur";
            //}
            return entity;
        }

        
    }
}
