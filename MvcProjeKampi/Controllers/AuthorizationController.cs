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
    public class AuthorizationController : Controller       //YETKİLENDİRME DÜZENLEMELERİNİ YAPICAZ.
    {
        AdminManager am = new AdminManager(new EfAdminDal());
        public ActionResult Index()
        {
            var adminValues = am.GetList();
            return View(adminValues);
        }

        [HttpGet]
        public ActionResult AdminAdd() 
        {
            return View(); 
        }

        [HttpPost]
        public ActionResult AdminAdd(Admin p)
        {
            am.AdminAdd(p);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditAdmin(int id)
        {
            var adminValue = am.GetByID(id);
            return View(adminValue);
        }
        [HttpPost]
        public ActionResult EditAdmin(Admin p)
        {
            am.AdminUpdate(p);
            return RedirectToAction("Index");   
        }
    }
}