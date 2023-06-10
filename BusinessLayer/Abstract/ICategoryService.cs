using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetList();
        void CategoryAdd (Category category);
        Category GetById (int id);       //ID bulma işlemi yapıcak.Silme ve Güncelleme işlemleri için öncelikle ID yi bulmamız lazım.
        void CategoryDelete(Category category);
        void CategoryUpdate(Category category);


    }
}
