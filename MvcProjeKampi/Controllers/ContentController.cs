using BusinessLayer.Concrete;
using DataAccessLayer.Concret;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class ContentController : Controller
    {
        ContentManager cm = new ContentManager(new EfContentDal());
        public ActionResult Index()
        {
            return View();
        }
        Context context = new Context();
        public ActionResult GetAllContent(string p)            //Arama işlemi Yapıcaz. Onun için dışardan bi parametre gönderiyoruz.
        {
            
            var values = from x in context.Contents select x;
            if(!string.IsNullOrEmpty(p) )     //p değeri boş değilse.
            {
                values = values.Where(y=>y.ContentValue.Contains(p));  //Where ile koşulumuzu veriyoruz ContentValue p yi içermeli.
            }
           
                return View(values.ToList());
            
           
        }

        public ActionResult ContentByHeading(int id)                                    //Başlık ile ilgili içeriklerin yazıların olduğu sayfayı tutucak.
        {
            var contentValues= cm.GetListByHeadingID(id);
            return View(contentValues);
        }
    }
}