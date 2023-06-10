using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class WriterController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterDal());
        WriterValidator writeValidator = new WriterValidator();

        public ActionResult Index()
        {
            var WriterValues = wm.GetList();
            return View(WriterValues);
        }
        [HttpGet]
        public ActionResult AddWriter()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddWriter(Writer p)
        {
           
            ValidationResult results = writeValidator.Validate(p);

            if (results.IsValid)  //Eğer Sonuçlar geçerli ise.
            {
                wm.WriterAdd(p);
                return RedirectToAction("Index");    //Ekleme işlemini yap ve beni geri Index'e gönder
            }
            else
            {
                foreach(var x in results.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }
            }
            return View();
        }
        
        [HttpGet]
        public ActionResult UpdateWriter(int id)
        {
            var WriterValue = wm.GetByID(id);
            return View(WriterValue);

        }
        [HttpPost]
        public ActionResult UpdateWriter(Writer p)
        {
            ValidationResult results = writeValidator.Validate(p);

            if (results.IsValid)  //Eğer Sonuçlar geçerli ise.
            {
                wm.WriterUpdate(p);
                return RedirectToAction("Index");    //Ekleme işlemini yap ve beni geri Index'e gönder
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
    }
}