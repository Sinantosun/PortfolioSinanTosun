using FluentValidation.Results;
using MyPortfolio.Models;
using MyPortfolio.ValidationRules.FeatureValidators;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    [Authorize]
    public class FeatureController : Controller
    {
        MyPortfolioEntities context = new MyPortfolioEntities();
        public ActionResult Index()
        {
            var value = context.Features.ToList();
            return View(value);
        }

        [HttpGet]
        public ActionResult AddFeature()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddFeature(Features features)
        {

            FeatureValidator validationRules = new FeatureValidator();
            ValidationResult validationResult = validationRules.Validate(features);
            if (validationResult.IsValid)
            {
                var filename = Guid.NewGuid();
                string extens = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/Feature/" + filename + extens;
                Request.Files[0].SaveAs(Server.MapPath(path));
                features.ImageUrl = "/Images/Feature/" + filename + extens;
                context.Features.Add(features);
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
        public ActionResult UpdateFeature(int id)
        {
            TempData["id"] = id;
            var value = context.Features.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateFeature(Features features)
        {
            int id = Convert.ToInt32(TempData["id"]);
            if (id == features.FeatureID)
            {
                FeatureValidator validationRules = new FeatureValidator();
                ValidationResult validationResult = validationRules.Validate(features);
                if (validationResult.IsValid)
                {
                    var value = context.Features.Find(features.FeatureID);
                    value.NameSurname = features.NameSurname;
                    value.Status = true;
                    value.Title = features.Title;

                    if (!string.IsNullOrEmpty(features.ImageUrl))
                    {
                        if (System.IO.File.Exists(Server.MapPath(value.ImageUrl)))
                        {
                            System.IO.File.Delete(Server.MapPath(value.ImageUrl));
                        }
                        var filename = Guid.NewGuid();
                        string extens = Path.GetExtension(Request.Files[0].FileName);
                        string path = "~/Images/Feature/" + filename + extens;
                        Request.Files[0].SaveAs(Server.MapPath(path));
                        value.ImageUrl = "/Images/Feature/" + filename + extens;
                    }

                  
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
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
        public ActionResult DeleteFeature(int id)
        {
            var values = context.Features.Find(id);
            if (System.IO.File.Exists(Server.MapPath(values.ImageUrl)))
            {
                System.IO.File.Delete(Server.MapPath(values.ImageUrl));
            }
            context.Features.Remove(values);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

//System.IO.File.Delete(Server.MapPath(value.ImageUrl));