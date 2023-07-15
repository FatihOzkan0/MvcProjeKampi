using BusinessLayer.Concrete;
using DataAccessLayer.Concret;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
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
        Context c = new Context();
        public ActionResult MyContent(string p)
        {
            //SESSİON KULLANIYORUZ. Session Sayesinde sisteme kim giriş yapar ise mail ile o mailin içeriklerini getiricek.
            
            p = (string)Session["WriteMail"];        //Session değerimiz writerMail.
            var writerIdInfo = c.Writers.Where(x=>x.WriteMail==p).Select(y=>y.WriterID).FirstOrDefault();        //Mail adresi ile parametreden gelen mail adresi eşit ise seçili Id yi getir

            var contentValues = cm.GetListByWriter(writerIdInfo);
            return View(contentValues);
        }

        [HttpGet]
        public ActionResult AddContent(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddContent(Content p )
        {
            string mail = (string)Session["WriteMail"];        
            var writerIdInfo = c.Writers.Where(x => x.WriteMail == mail).Select(y => y.WriterID).FirstOrDefault();
            p.ContentDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.WriterID= writerIdInfo;
            p.ContentStatus = true;
            cm.ContentAdd(p);
            return RedirectToAction("MyContent");
        }
    }
}