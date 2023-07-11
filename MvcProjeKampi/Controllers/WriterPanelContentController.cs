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
    public class WriterPanelContentController : Controller
    {
        ContentManager cm = new ContentManager(new EfContentDal());
        public ActionResult MyContent(string p)
        {
            //SESSİON KULLANIYORUZ. Session Sayesinde sisteme kim giriş yapar ise mail ile o mailin içeriklerini getiricek.
            Context c = new Context();
            p = (string)Session["WriteMail"];        //Session değerimiz writerMail.
            var writerIdInfo = c.Writers.Where(x=>x.WriteMail==p).Select(y=>y.WriterID).FirstOrDefault();        //Mail adresi ile parametreden gelen mail adresi eşit ise seçili Id yi getir

            var contentValues = cm.GetListByWriter(writerIdInfo);
            return View(contentValues);
        }
    }
}