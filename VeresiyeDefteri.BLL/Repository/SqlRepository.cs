using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VeresiyeDefteri.DAL.Contexts;

namespace VeresiyeDefteri.BLL.Repository
{
    public class SqlRepository<T> where T : class
    {
        SqlDbContext db = new SqlDbContext();
        public IQueryable<T> Listele()
        {
            return db.Set<T>();
        }
        public IQueryable<T> Listele(Expression<Func<T, bool>> expression)
        {
            return db.Set<T>().Where(expression);
        }
        public void Insert(T entity)
        {
            db.Set<T>().Add(entity);
            db.SaveChanges();
        }
        public void Delete(T entity)
        {
            db.Set<T>().Remove(entity);
            db.SaveChanges();
        }
        public void Update(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
