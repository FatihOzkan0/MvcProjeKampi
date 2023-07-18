using BusinessLayer.Concrete;
using DataAccessLayer.Concret;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using BusinessLayer.ValidationRules;
using FluentValidation.Results;

namespace MvcProjeKampi.Controllers
{
    public class WriterPanelController : Controller
    {
                  //YAZARIN BAŞLIK İLE İLGİLİ İŞLEMLERİNİ BU CONTROLLERDA GERÇEKLEŞTİRİYORUZ.

        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        Context c = new Context();

        [HttpGet]
        public ActionResult WriterProfile(int id=0)
        {
           string p = (string)Session["WriteMail"];
            id= c.Writers.Where(x => x.WriteMail == p).Select(y => y.WriterID).FirstOrDefault();
            var writerValue = wm.GetByID(id);
            return View(writerValue);
        }
        [HttpPost]
        public ActionResult WriterProfile(Writer p)
        {
            WriterValidator writeValidator = new WriterValidator();
            ValidationResult results = writeValidator.Validate(p);

            if (results.IsValid)  //Eğer Sonuçlar geçerli ise.
            {
                wm.WriterUpdate(p);
                return RedirectToAction("AllHeading","WriterPanel");    //Ekleme işlemini yap ve beni geri Index'e gönder
            }
            else
            {
                foreach (var x in results.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }
            }
            return View();


        }

        public ActionResult MyHeading(string p)   //Burada Sadece Giren Yazarın kendi başlıklarını getiricem.
        {
            
            p = (string)Session["WriteMail"];
            var writerIdInfo = c.Writers.Where(x => x.WriteMail == p).Select(y => y.WriterID).FirstOrDefault();
          
            var values = hm.GetListByWriter(writerIdInfo);
            return View(values);
        }
        [HttpGet]
        public ActionResult NewHeading()
        {
            //Burada dropDown yapıyoruz ve heading içinde bulunan categoryId nin name 'ini getiriyoruz.
           


            List<SelectListItem> valueCategory = (from x in cm.GetList()            
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            ViewBag.vlc=valueCategory;
            return View();
        }
        [HttpPost]
        public ActionResult NewHeading(Heading p)
        {


            string writerMailInfo = (string)Session["WriteMail"]; //Burada Session ile mail adresinden giriş yapanı tutuyoruz.
            var writerIdInfo = c.Writers.Where(x => x.WriteMail == writerMailInfo).Select(y => y.WriterID).FirstOrDefault();   //Burada da mail adresi hangi yazarın ise onun Id sini alıyoruz.


            p.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.WriterID =writerIdInfo;
            p.HeadingStatus = true;
            hm.HeadingAdd(p);
            return RedirectToAction("MyHeading");
        }

        [HttpGet]
        public ActionResult EditHeading(int id)     //Başlığı düzenleme işlemlerini burada yapıyoruz.
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
        public ActionResult EditHeading(Heading p)
        {
            hm.HeadingUpdate(p);
            return RedirectToAction("MyHeading");
        }

        public ActionResult DeleteHeading(int id)
        {
            var HeadingValue = hm.GetByID(id);
            HeadingValue.HeadingStatus = false;
            hm.HeadingDelete(HeadingValue);
            return RedirectToAction("MyHeading");
        }

        public ActionResult AllHeading(int p =1)       //Sayfalamayı temsil eder
        {
            var headings = hm.GetList().ToPagedList(p, 4);      //Her sayfada 4 er 4er listelicek.Başta ki  bir ise 1. 4 lü grubu belirtiyor.
            return View(headings);
        }
    }
}