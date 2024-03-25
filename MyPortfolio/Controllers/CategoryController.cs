using FluentValidation.Results;
using MyPortfolio.Models;
using MyPortfolio.ValidationRules.CategoryValidators;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    public class CategoryController : Controller
    {
        MyPortfolioEntities context = new MyPortfolioEntities();
        public ActionResult Index()
        {
            var values = context.Categories.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Categories categories)
        {
            CategoryValidator validationRules = new CategoryValidator();
            ValidationResult validationResult = validationRules.Validate(categories);
            if (validationResult.IsValid)
            {
                context.Categories.Add(categories);
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
        public ActionResult UpdateCategory(int id)
        {
            TempData["id"] = id;
            var values = context.Categories.Find(id);
            return View(values);
        }
        [HttpPost]
        public ActionResult UpdateCategory(Categories categories)
        {
            int _id = Convert.ToInt32(TempData["id"]);
            if (_id == categories.CategoryID)
            {
                CategoryValidator validationRules = new CategoryValidator();
                ValidationResult validationResult = validationRules.Validate(categories);
                if (validationResult.IsValid)
                {
                    var value = context.Categories.Find(categories.CategoryID);
                    value.Name = categories.Name;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in validationResult.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                    var values = context.Categories.Find(_id);
                    TempData["id"] = _id;
                    return View(values);

                }
              
            }
            else
            {
                var values = context.Categories.Find(_id);
                return View(values);
            }

        }

        public ActionResult DeleteCategory(int id)
        {
            var value = context.Categories.Find(id);
            context.Categories.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}