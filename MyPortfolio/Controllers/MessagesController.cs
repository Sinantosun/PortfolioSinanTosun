using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    public class MessagesController : Controller
    {
        MyPortfolioEntities MyPortfolioEntities = new MyPortfolioEntities();
        public ActionResult Index()
        {
            var value = MyPortfolioEntities.Messages.Where(x => x.IsRead == false).ToList();
            return View(value);
        }

        public ActionResult GetAllMessage()
        {
            var value = MyPortfolioEntities.Messages.ToList();
            return View(value);
        }

        public ActionResult GetReadedMesssage()
        {
            var value = MyPortfolioEntities.Messages.Where(x => x.IsRead == true).ToList() ;
            return View(value);
        }
        [HttpPost]
        public ActionResult GetMessageByNameSurname(string NameSurname)
        {
            var value = MyPortfolioEntities.Messages.Where(x => x.NameSurname == NameSurname).ToList();
            return View("Index",value);
        }

        public ActionResult ReadMessage(int id)
        {
            var value = MyPortfolioEntities.Messages.Find(id);
            value.IsRead = true;
            MyPortfolioEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MessageDetail(int id)
        {
            var value = MyPortfolioEntities.Messages.Find(id);
            return View(value);
        }
    }
}