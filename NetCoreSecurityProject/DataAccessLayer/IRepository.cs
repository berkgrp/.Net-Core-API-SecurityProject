using EntityLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccesLayer
{
    public interface IRepository
    {
        public interface IRepository<T> where T : class
        {
            IEnumerable<T> GetAll();
            T GetByID(int ID);
            void Update(T model);
            void Create(T model);
            void Delete(T model);
            void DeleteID(int ID);
            T GetByEmail(string email);
            Task UpdateAsync(T entity);
        }
        public class Repository<T> : IRepository<T> where T : class
        {
            protected KartOyunuDBContext _context;
            private readonly DbSet<T> _dbSet;
            public Repository(KartOyunuDBContext context)
            {
                this._context = context ?? throw new ArgumentNullException("dbContext can not be null.");
                _dbSet = _context.Set<T>();
            }
            public IEnumerable<T> GetAll()
            {
                var allData = _dbSet.ToList();
                if (allData == null)
                {
                    throw new ArgumentNullException("Veritabanındaki ilgili tabloda veri bulunmamaktadır.");
                }
                else
                {
                    return _dbSet.ToList();
                }
            }
            public void Create(T model)
            {
                _dbSet.Add(model);
            }
            public void Delete(T model)
            {
                if (model.GetType().GetProperty("IsDeleted") == null)
                {
                    T _model = model;
                    _model.GetType().GetProperty("IsDeleted").SetValue(_model, true);
                    this.Update(_model);
                }
                else
                {
                    _dbSet.Attach(model);
                    _dbSet.Remove(model);
                }
            }
            public void Update(T model)
            {
                if (model == null)
                {
                    throw new ArgumentNullException("veride boşluk var");
                }//else
                _dbSet.Update(model);
            }

            public T GetByID(int ID)
            {
                return _dbSet.Find(ID);
            }

            public T GetByEmail(string email)
            {
                return _dbSet.Find(email);
            }

            public void DeleteID(int ID)
            {
                _dbSet.Remove(GetByID(ID));
            }

            public List<T> GetAllAsList()
            {
                return _dbSet.ToList();
            }

            public IEnumerable<T> GetAllAsNoTracing()
            {
                return _dbSet.AsNoTracking().ToList();
            }

            public Task UpdateAsync(T entity)
            {
                _dbSet.Update(entity).State = EntityState.Modified;
                return Task.CompletedTask;
            }

            public KartOyunuDBContext KartOyunuDBContext { get { return _context as KartOyunuDBContext; } }
        }

    }
}
