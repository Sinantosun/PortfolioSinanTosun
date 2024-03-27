using FluentValidation.Results;
using MyPortfolio.Models;
using MyPortfolio.ValidationRules.ExperiencesValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    [Authorize]
    public class ExperiencesController : Controller
    {
        MyPortfolioEntities context = new MyPortfolioEntities();

        public ActionResult Index()
        {
            var values = context.Experiences.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddExperiences()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddExperiences(Experiences experiences)
        {
            if (string.IsNullOrEmpty(experiences.EndYear))
            {
                experiences.EndYear = "Devam Ediyor";
            }

            ExperiencesValidator validationRules = new ExperiencesValidator();
            ValidationResult validationResult = validationRules.Validate(experiences);
            if (validationResult.IsValid)
            {
                context.Experiences.Add(experiences);
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
        public ActionResult UpdateExperiences(int id)
        {
            var value = context.Experiences.Find(id);
            TempData["id"] = id;
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateExperiences(Experiences experiences)
        {
            int id = Convert.ToInt32(TempData["id"]);
            if (id == experiences.ExperinceID)
            {
                ExperiencesValidator validationRules = new ExperiencesValidator();
                ValidationResult validationResult = validationRules.Validate(experiences);
                if (validationResult.IsValid)
                {
                    var value = context.Experiences.Find(experiences.ExperinceID);
                    value.CompanyName = experiences.CompanyName;
                    value.Description = experiences.Description;
                    value.EndYear = experiences.EndYear;
                    value.Location = experiences.Location;
                    value.StartYear = experiences.StartYear;
                    value.Title = experiences.Title;
      
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in validationResult.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                    var value = context.Experiences.Find(id);
                    TempData["id"] = id;
                    return View(value);
                }
            }
            else
            {
                return HttpNotFound();
            }
           
        }



        public ActionResult DeleteExperiences(int id)
        {
            var value = context.Experiences.Find(id);
            context.Experiences.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}