using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IContentService
    {
        List<Content> GetList();

        List<Content> GetListByHeadingID(int id);           //Bana ID parametresine göre sadece o ID nin listesini döndürür.Yani amacımız Hangi Başlık seçiliyse onun ID sine göre listeleme yapmak.
        void ContentAdd(Content content);
        Content GetById(int id);      
        void ContentDelete(Content content);
        void ContentUpdate(Content content);
    }
}
