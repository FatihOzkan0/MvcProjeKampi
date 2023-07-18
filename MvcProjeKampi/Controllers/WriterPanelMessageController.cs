using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concret;
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
    public class WriterPanelMessageController : Controller
    {
        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator validator = new MessageValidator();
        
        public ActionResult Inbox()
        {
            string p = (string)Session["WriteMail"];  //Session İşlemi ile sadece sisteme otantike olan yazara gelen mesajlar listeleniyor.
            var messageValues = mm.GetListInbox(p);
            return View(messageValues);
        }

        public ActionResult Sendbox()
        {
            string p = (string)Session["WriteMail"];
            var messageValues = mm.GetListSendbox(p);
            return View(messageValues);
        }

        public PartialViewResult MessageListMenu()        //Yazarın Messaj kısmını açtığında bazı message bölümlerini görmemesi için menuyu ayarlıcaz.
        {
            return PartialView();
        }

        public ActionResult GetInboxMessageDetails(int id)      //Gelen Kutusuna Gelen Mesaj Detayları
        {
            var values = mm.GetByID(id);
            return View(values);
        }

        public ActionResult GetSendboxMessageDetails(int id) //Mesaj detaylarını gösterir.
        {
            var values = mm.GetByID(id);
            return View(values);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewMessage(Message p)
        {
            string sender = (string)Session["WriteMail"];
            ValidationResult results = validator.Validate(p);
            if (results.IsValid)
            {
                p.SenderMail = sender;
                p.MessageDate = DateTime.Parse(DateTime.Now.ToShortDateString());      //Ekleme İşlemi Yaparken bugünün Tarihini al.
                mm.MessageAdd(p);
                return RedirectToAction("Sendbox");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
    }
}