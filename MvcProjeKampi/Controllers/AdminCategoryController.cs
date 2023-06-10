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
    public class AdminCategoryController : Controller
    {
        CategoryManager cm = new CategoryManager(new EfCategoryDal());    //Burada EfCategoryDal kullanmamızın sebebi eğer ben projede EF kullanmayı bırakıp başka bir pakete geçersem bize kolaylık sağlaması için.
        
        public ActionResult Index()
        {
            var categoryList = cm.GetList();
            return View(categoryList);
        }
        
        
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }
        
        
        [HttpPost]
        public ActionResult AddCategory(Category p)
        {
            CategoryValidator categoryValidator = new CategoryValidator();
            ValidationResult result = categoryValidator.Validate(p);
            
            if(result.IsValid)   //Eğer Sonuç Geçerli ise
            {
                cm.CategoryAdd(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
            return View();
            
        }

        public ActionResult DeleteCategory(int id)     //BU ACTİONUN BİR VİEW'E OLMICAK SADECE INDEX İLE ÇAĞIRILARAK KULLANILACAK.
        {
            var CategoryValue= cm.GetById(id);     //Seçilen id değerinin tüm değerlerini tutar , name, description gibi.
            cm.CategoryDelete(CategoryValue);      //Yukarıda tuttuğumuz kategori değerini burada siliyoruz.

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateCategory(int id)
        {
            var CategoryValue = cm.GetById(id);
            return View(CategoryValue);
        }

        [HttpPost]
        public ActionResult UpdateCategory(Category category)
        {
            cm.CategoryUpdate(category);
            return RedirectToAction("Index");
        }
    }
}