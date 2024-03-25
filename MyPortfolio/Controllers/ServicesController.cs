using FluentValidation;
using FluentValidation.Results;
using MyPortfolio.Models;
using MyPortfolio.ValidationRules.ServicesValidatiors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    public class ServicesController : Controller
    {
        MyPortfolioEntities context = new MyPortfolioEntities();
        public ActionResult Index()
        {
            var list = context.Services.ToList();
            return View(list);
        }
        [HttpGet]
        public ActionResult AddService()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddService(Services services)
        {
            ServicesValidator validationRules = new ServicesValidator();
            ValidationResult validationResult = validationRules.Validate(services);
            if (validationResult.IsValid)
            {
                context.Services.Add(services);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
       

        [HttpGet]
        public ActionResult UpdateService(int id)
        {
            var value = context.Services.Find(id);
            TempData["id"] = id;
            return View(value);
        }

        [HttpPost]
        public ActionResult UpdateService(Services services)
        {
            int id = Convert.ToInt32(TempData["id"]);
            if (id == services.ServicesID)
            {
                ServicesValidator validationRules = new ServicesValidator();
                ValidationResult validationResult = validationRules.Validate(services);
                if (validationResult.IsValid)
                {
                    var data = context.Services.Find(services.ServicesID);
                    data.Icon = services.Icon;
                    data.Title = services.Title;
                    data.Description = services.Description;
                    
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in validationResult.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
                var value = context.Services.Find(id);
                TempData["id"] = id;
                return View(value);
            }
            else
            {
                return HttpNotFound();
            }
        }
        public ActionResult DeleteService(int id)
        {
            var value = context.Services.Find(id);
            context.Services.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}