using FluentValidation.Results;
using MyPortfolio.Models;
using MyPortfolio.ValidationRules.AboutValidators;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    public class AboutController : Controller
    {
        MyPortfolioEntities context = new MyPortfolioEntities();
        public ActionResult Index()
        {
            var value = context.Abouts.ToList();
            return View(value);
        }
        [HttpGet]
        public ActionResult AddAbout()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAbout(Abouts abouts)
        {
            AboutValidator validationRules = new AboutValidator();
            ValidationResult validationResult = validationRules.Validate(abouts);
            if (validationResult.IsValid)
            {
                var fileName = Guid.NewGuid();
                string ex = Path.GetExtension(Request.Files[0].FileName);
                string fullFileName = "~/Images/About/" + fileName + ex;
                Request.Files[0].SaveAs(Server.MapPath(fullFileName));
                abouts.ImageURL = "/Images/About/" + fileName + ex;
                context.Abouts.Add(abouts);
               context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View();
            }


        }

        [HttpGet]
        public ActionResult UpdateAbout(int id)
        {

            var values = context.Abouts.Find(id);
            TempData["id"] = id;
            return View(values);
        }
        [HttpPost]
        public ActionResult UpdateAbout(Abouts abouts)
        {
            int id = Convert.ToInt32(TempData["id"]);
            if (abouts.AboutID == id)
            {
                AboutValidator validationRules = new AboutValidator();
                ValidationResult validationResult = validationRules.Validate(abouts);
                if (validationResult.IsValid)
                {
                    var values = context.Abouts.Find(abouts.AboutID);
                    values.Description = abouts.Description;
                    values.Title = abouts.Title;

                    if (!string.IsNullOrEmpty(abouts.ImageURL))
                    {
                        if (System.IO.File.Exists(Server.MapPath(values.ImageURL)))
                        {
                            System.IO.File.Delete(Server.MapPath(values.ImageURL));
                        }
                        var filename = Guid.NewGuid();
                        string extens = Path.GetExtension(Request.Files[0].FileName);
                        string path = "~/Images/About/" + filename + extens;
                        Request.Files[0].SaveAs(Server.MapPath(path));
                        values.ImageURL = "/Images/About/" + filename + extens;
                    }


                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in validationResult.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                    var values = context.Abouts.Find(id);
                    TempData["id"] = id;
                    return View(values);
                }


            }
            else
            {
                return HttpNotFound();
            }


        }


        public ActionResult DeleteAbout(int id)
        {
            var values = context.Abouts.Find(id);
            context.Abouts.Remove(values);
            context.SaveChanges();

            if (System.IO.File.Exists(Server.MapPath(values.ImageURL)))
            {
                System.IO.File.Delete(Server.MapPath(values.ImageURL));
            }
            return RedirectToAction("Index");
        }
    }
}