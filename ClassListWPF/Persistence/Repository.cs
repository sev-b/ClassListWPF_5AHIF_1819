using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ClassListWPF.Persistence
{
    public interface IRepository<T>
    {
        // CRUD operations
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        // queries
        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
        T GetById(object id);
    }

    public class Repository<T, Ctx> : IRepository<T>
        where T : class
        where Ctx : DbContext, new()
    {
        public void Create(T entity)
        {
            using (Ctx db = new Ctx())
            {
                db.Set<T>().Add(entity);
                db.SaveChanges();
            }
        }

        public void Update(T entity)
        {
            using (Ctx db = new Ctx())
            {
                db.Set<T>().Attach(entity);
                db.Entry<T>(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Delete(T entity)
        {
            using (Ctx db = new Ctx())
            {
                db.Set<T>().Attach(entity);
                db.Set<T>().Remove(entity);
                db.SaveChanges();
            }
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> order = null,
            string include = "")
        {
            using (Ctx db = new Ctx())
            {
                var result = db.Set<T>() as IQueryable<T>;
                if (filter != null)
                    result = result.Where(filter);

                foreach (string prop in include.Split(
                    new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    result = result.Include(prop);
                }

                if (order != null)
                    result = order(result);

                return result.ToList();
            }
        }

        public T GetById(object id)
        {
            using (Ctx db = new Ctx())
            {
                return db.Set<T>().Find(id);
            }
        }
    }

    public class RepositoryUow<T, Ctx> : IRepository<T>
        where T : class
        where Ctx : DbContext, new()
    {
        private Ctx db;

        public RepositoryUow(Ctx db)
        {
            this.db = db;
        }

        public void Create(T entity)
        {
            db.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Entry<T>(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            db.Set<T>().Attach(entity);
            db.Set<T>().Remove(entity);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> order = null,
            string include = "")
        {
            var result = db.Set<T>() as IQueryable<T>;
            if (filter != null)
                result = result.Where(filter);

            foreach (string prop in include.Split(
                new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                result = result.Include(prop);
            }

            if (order != null)
                result = order(result);

            return result.ToList();
        }

        public T GetById(object id)
        {
            return db.Set<T>().Find(id);
        }
    }
}
