using MyPortfolio.DAL;
using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyPortfolio.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        MyPortfolioEntities context = new MyPortfolioEntities();
        [HttpPost]
        public ActionResult Index(Admins admins)
        {


            var userName = context.Admins.FirstOrDefault(x => x.UserName == admins.UserName);
            if (userName != null)
            {
                var PasswordWithSalt = userName.PasswordSalt + admins.Password + userName.PasswordSalt;
                string HashedPassword = PasswordHasher.HashPassword(PasswordWithSalt);
                if (HashedPassword == userName.Password)
                {
                    FormsAuthentication.SetAuthCookie(userName.UserName, false);
                    return RedirectToAction("Index", "Project");
                }
                else
                {
                    ModelState.AddModelError("UserName", "Kullanıcı Adı Veya Şifre Hatalı");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("UserName", "Kullanıcı Adı Veya Şifre Hatalı");
                return View();
            }
          


        }

        [HttpGet]
        public ActionResult SignUp()
        {

            return View();
        }
        [HttpPost]
        public ActionResult SignUp(Admins admins)
        {
            var guid = Guid.NewGuid();
            admins.PasswordSalt = guid.ToString();
            admins.Role = "A";
            string PasswordWithSalt = guid + admins.Password + guid;
            string HashedPassword = PasswordHasher.HashPassword(PasswordWithSalt);
            admins.Password = HashedPassword;
            context.Admins.Add(admins);
            context.SaveChanges();
            return View();
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();


            return RedirectToAction("../Default/Index");
        }
    }
}