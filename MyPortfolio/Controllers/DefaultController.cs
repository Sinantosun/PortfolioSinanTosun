using MyPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    public class DefaultController : Controller
    {
        MyPortfolioEntities context = new MyPortfolioEntities();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult DefaultFeaturePartial()
        {
            var values = context.Features.ToList();
            return PartialView(values);
        }
        public PartialViewResult DefaultAboutPartial()
        {
            var value = context.Abouts.ToList();
            return PartialView(value);
        }
    }
}