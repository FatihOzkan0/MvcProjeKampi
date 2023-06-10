using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concret.Repositories
{


    public class GenericRepository<T> : IRepository<T> where T : class
    {
        Context c = new Context();
        DbSet<T> _object;   //Burada ki T sınıfımızı tutmaktadır.

        

        public GenericRepository()    //NOT: T değerimizi karşılık gelicek olan sınıfı nasıl bulabaliriz? Constructor kullanarak.
        { 
            _object = c.Set<T>();     // Senin değerin contexte bağlı olan T değerinden gelicek demek.
        }
        

        public void Delete(T p)
        {
            var deletedEntity = c.Entry(p);
            deletedEntity.State= EntityState.Deleted;
            c.SaveChanges();

            //_object.Remove(p);         //Artık EntityState kullanıyoruz.
        }

        

        public void Insert(T p)
        {
            var addedEntity = c.Entry(p);
            addedEntity.State = EntityState.Added;
            c.SaveChanges();

            // _object.Add(p); EKLEME İŞLEMİNİ İLK BAŞTA  BU ŞEKİLDE YAPIYORDUK FAKAR SOLID PRENSİPLERİ İÇİN YUKARIDAKİ GİBİ ENTİTY STATE KULLANARAK YAPICAZ.

        }

        public void Update(T p)
        {
            var updatedEntity = c.Entry(p);
            updatedEntity.State = EntityState.Modified; 
            c.SaveChanges();
        }

        public List<T> List()
        {
           return _object.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> filter)
        {
            return _object.Where(filter).ToList();     //Bana Filterdan gelen şarta göre şartlı listeleme yap.cscsscs
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return _object.SingleOrDefault(filter);      //Sadece Tek bir değeri arıcaksak bu method kullanılır. Örnek ID si 5 olan.
        }


    }
}
