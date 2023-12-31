﻿using BusinessLayer.Concrete;
using DataAccessLayer.Concret;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcProjeKampi.Controllers
{
    [AllowAnonymous]      //Bu sayede erişim engelinden muaf tutuyoruz ve erişim olmayan sayfaya girince buraya yönlendiriyoruz.
    public class LoginController : Controller
    {
        WriterLoginManager wm = new WriterLoginManager(new EfWriterDal());

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Admin p)
        {
            Context context = new Context();
            var adminUserInfo = context.Admins.FirstOrDefault(x => x.AdminUserName == p.AdminUserName && x.AdminPassword == p.AdminPassword);
            if (adminUserInfo != null)
            {
                FormsAuthentication.SetAuthCookie(adminUserInfo.AdminUserName, false);       //Yetkilendirme işlemini yapıyoruz ve UserName yetki veriyoruz
                Session["AdminUserName"] = adminUserInfo.AdminUserName;
                return RedirectToAction("Index", "AdminCategory");
            }
            else
            {
                return RedirectToAction("Index");
            }


        }
        //Yazar Giriş Sayfası

        [HttpGet]
        public ActionResult WriterLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WriterLogin(Writer p)
        {
            //Context context = new Context();
            //var writerUserInfo = context.Writers.FirstOrDefault(x => x.WriteMail == p.WriteMail && x.WriterPassword == p.WriterPassword);

            var writerUserInfo = wm.GetWriter(p.WriteMail, p.WriterPassword);


            if (writerUserInfo != null)
            {
                FormsAuthentication.SetAuthCookie(writerUserInfo.WriteMail, false);  //Yetkilendirme işlemini yapıyoruz ve UserName yetki veriyoruz
                Session["WriteMail"] = writerUserInfo.WriteMail;
                return RedirectToAction("MyContent", "WriterPanelContent");
            }
            else
            {
                return RedirectToAction("WriterLogin");
            }

        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();       //Oturuma Giriş yapan kullanıcıdan çıkar bilgilerini Siler.
            Session.Abandon();                   //Oturumu Sonlandırır.
            return RedirectToAction("Headings", "Default");
        }
    }
}