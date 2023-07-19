using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class HeadingController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new  EfCategoryDal());
        WriterManager wm = new WriterManager(new  EfWriterDal());   
        public ActionResult Index()
        {
            var headingValues = hm.GetList();
            return View(headingValues);
        }


        public ActionResult HeadingReport()        //Rapor Alabileceğimiz Sayfa.
        {
            var headingValues = hm.GetList();
            return View(headingValues);
        }


        [HttpGet]
        public ActionResult AddHeading()
        {
            //Bu işlemi yapmamızın nedeni kategori kısmının ilişkili olması ve bizim kategoriyi eklerken sürekli id'sini bilmemiz gerekmektedir onun yerine ismiyle erişeceğimiz dropDown Yapıyoruz.

            List<SelectListItem> valueCategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();

            List<SelectListItem> valueWriter = (from x in wm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.WriterName +" "+ x.WriterSurname,
                                                      Value = x.WriterID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valueCategory;
            ViewBag.vlw = valueWriter;
            return View();
        }

        public ActionResult AddHeading(Heading p)
        {
            p.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());      //Bugünün Tarihini ekleme işlemi yapar.
            hm.HeadingAdd(p);
            return RedirectToAction("Index");
        }

        [HttpGet]
       public ActionResult EditHeading (int id)     //Başlığı düzenleme işlemlerini burada yapıyoruz.
        {
            List<SelectListItem> valueCategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valueCategory;
            var HeadingValue = hm.GetByID(id);
            return View(HeadingValue);
        }

        [HttpPost]
        public ActionResult EditHeading (Heading p)
        {
            hm.HeadingUpdate(p);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteHeading(int id)
        {
            var HeadingValue = hm.GetByID (id);
            HeadingValue.HeadingStatus = false;
            hm.HeadingDelete(HeadingValue);
            return RedirectToAction("Index");
        }
    }
}