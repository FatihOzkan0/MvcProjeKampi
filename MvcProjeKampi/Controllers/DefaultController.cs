using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    [AllowAnonymous]    //Sisteme otantşke oluyoruz ve giriş yapmadan içine giriyoruz.
    public class DefaultController : Controller
    {
        //Projenin Herkesin erişebileceği Ana Sayfası olucak.

        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        ContentManager cm = new ContentManager(new EfContentDal());
        public PartialViewResult Index(int id = 7)          //Contentleri Bunun içine çekiyoruz.
        {
            var contentList = cm.GetListByHeadingID(id);
            return PartialView(contentList);
        }

        public ActionResult Headings()             //Sol Menü olucak bu Layout.
        {
            var headingList = hm.GetList();
            return View(headingList);
        }
    }
}