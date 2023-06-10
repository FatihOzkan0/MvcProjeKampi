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
    public class CategoryController : Controller
    {
        // GET: Category

        CategoryManager cm = new CategoryManager(new EfCategoryDal());

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCategoryList()
        {
            var categoryValues = cm.GetList();
            return View(categoryValues);
        }

        [HttpGet]   //Sayfa Yüklenince Gerçekleşicek işlemler.
        public ActionResult AddCategory()
        {
            return View();  //Sayfa yüklenince bana sadece sayfayı geri döndür.
        }

        [HttpPost]  //HttpPost Attribute sayfada bir buttona tıklanıldığı zaman çalışıcak komutunu verir. Bunu eklemezsek bu şekilde sayfa her yenilendiğinde ekleme yapar.
        public ActionResult AddCategory(Category p)
        {
            

            CategoryValidator categoryValidator = new CategoryValidator();
            ValidationResult results = categoryValidator.Validate(p);    //Validation sonuçlarını tutuyoruz ValidationResult ile. Validate ile de Category den gelen değerinin kontrolünü yapıyoruz uyuyormu kurallara diye.

            if (results.IsValid)    //Validasyon Doğrulandıysa
            {
                cm.CategoryAdd(p);
                return RedirectToAction("GetCategoryList");    //Ekleme işlemi gerçekleştikten sonra beni "GetCategoryList" methoduna gönder demek
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
    }
}