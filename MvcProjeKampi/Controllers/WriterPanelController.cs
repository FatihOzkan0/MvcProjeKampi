﻿using BusinessLayer.Concrete;
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
    public class WriterPanelController : Controller
    {
                  //YAZARIN BAŞLIK İLE İLGİLİ İŞLEMLERİNİ BU CONTROLLERDA GERÇEKLEŞTİRİYORUZ.

        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        Context c = new Context();
    

        public ActionResult WriterProfile()
        {
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

        public ActionResult AllHeading()
        {
            var headings = hm.GetList();
            return View(headings);
        }
    }
}