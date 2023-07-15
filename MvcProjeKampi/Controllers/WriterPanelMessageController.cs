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
        Context c = new Context();
        public ActionResult Inbox()
        {
            string p = (string)Session["WriteMail"];  //Session İşlemi ile sadece sisteme otantike olan yazara gelen mesajlar listeleniyor.
            var writerIdInfo = c.Writers.Where(x=>x.WriteMail==p).Select(y=>y.WriterID).FirstOrDefault();
            var messageValues = mm.GetListInbox(p);
            return View(messageValues);
        }

        public ActionResult Sendbox()
        {
            var messageValues = mm.GetListSendbox();
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
            ValidationResult results = validator.Validate(p);
            if (results.IsValid)
            {
                p.SenderMail = "admin@gmail.com";
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