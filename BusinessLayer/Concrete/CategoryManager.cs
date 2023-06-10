using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concret.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
       


        ICategoryDal _categoryDal;      //ICategoryDal' ın içindeki methodları alarak kullanıcaz burada.

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void CategoryAdd(Category category)
        {
            _categoryDal.Insert(category);
        }

        public void CategoryDelete(Category category)
        {
            _categoryDal.Delete(category);    //Bu Method bizim Irepositoryde tanımlayıp içini GenericRepositoryde doldurduğumuz methoddan gelmektedir.
        }

        public void CategoryUpdate(Category category)
        {
            _categoryDal.Update(category);
        }

        public Category GetById(int id)      //Silme güncelleme işlemleri için id bulma methodu.
        {
            return _categoryDal.Get(x=>x.CategoryID==id);    //ID girilen ID ye eşit olacak.
        }

        public List<Category> GetList()
        {
            return _categoryDal.List();       //Bu Yapı Sayesinde new lemeden GenericRepositoryde ki methodlara erişebiliyorum.Bağımlılığı Önledik.
        }
    }
}
