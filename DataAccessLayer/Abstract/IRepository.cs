using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IRepository<T>

    //CRUD Operasyonlarının gerçekleşeceği metotları burada tanımlıyoruz.
    {
        List<T> List();
        void Insert(T p);
        T Get(Expression<Func<T,bool>>filter);    //Silme Güncelleme işlemleri için ID bulma methodu.
        void Delete(T p);
        void Update(T p);

        List<T> List (Expression<Func<T, bool>> filter);   //Şartlı Listeleme methodudur.

    }
}
