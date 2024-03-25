using FluentValidation.Results;
using MyPortfolio.Models;
using MyPortfolio.ValidationRules.SkillsValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    public class SkillsController : Controller
    {
        MyPortfolioEntities context = new MyPortfolioEntities();
        public ActionResult Index()
        {
            var value = context.Skills.ToList();
            return View(value);
        }
        [HttpGet]
        public ActionResult AddSkill()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSkill(Skills skills)
        {
            SkillValidator validationRules = new SkillValidator();
            ValidationResult validationResult = validationRules.Validate(skills);
            if (validationResult.IsValid)
            {
                context.Skills.Add(skills);
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
        public ActionResult UpdateSkill(int id)
        {
            var value = context.Skills.Find(id);
            TempData["id"] = id;
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateSkill(Skills skills)
        {
            int id = Convert.ToInt32(TempData["id"]);
            if (id == skills.SkillID)
            {
                SkillValidator validationRules = new SkillValidator();
                ValidationResult validationResult = validationRules.Validate(skills);
                if (validationResult.IsValid)
                {
                    var value = context.Skills.Find(skills.SkillID);
                    value.Amount = skills.Amount;
                    value.Name = skills.Name;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in validationResult.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                    var value = context.Skills.Find(id);
                    TempData["id"] = id;
                    return View(value);
                }
            }
            else
            {
                return HttpNotFound();
            }

        }
        public ActionResult DeleteSkill(int id)
        {
            var value = context.Skills.Find(id);
            context.Skills.Remove(value);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}