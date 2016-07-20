using _Proje_Blog_.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _Proje_Blog_.Controllers
{
    public class LoginController : Controller
    {
        public string kullaniciAdi = "gb";
        public string sifre = "1";
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string pass, string userName)
        {
            if (pass != sifre && userName != kullaniciAdi)
            {
                ViewBag.ErrorMessage = "Kullanıcı adı veya şifre hatalı";
                return View("Index");
            }
            else
            {
                UserHelper.Id = 1;
                UserHelper.UserName = "Admin";

                return RedirectToAction("Index", "Admin");
            }

        }

        public ActionResult LogOut()
        {
            UserHelper.Id = null;
            UserHelper.UserName = null;

            return View("Index");
        }
    }
}