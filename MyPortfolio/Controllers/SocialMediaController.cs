using FluentValidation.Results;
using MyPortfolio.Models;
using MyPortfolio.ValidationRules.SocialMediasValidatiors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    [Authorize]
    public class SocialMediaController : Controller
    {
        MyPortfolioEntities context = new MyPortfolioEntities();
        public ActionResult Index()
        {
            var value = context.SocialMedias.ToList();
            return View(value);
        }
        [HttpGet]
        public ActionResult AddSocialMedia()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSocialMedia(SocialMedias socialMedias)
        {
            SocialMediasValidator validationRules = new SocialMediasValidator();
            ValidationResult validationResult = validationRules.Validate(socialMedias);
            if (validationResult.IsValid)
            {
                context.SocialMedias.Add(socialMedias);
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
        public ActionResult UpdateSocialMedia(int id)
        {
            var value = context.SocialMedias.Find(id);
            TempData["id"] = id;
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateSocialMedia(SocialMedias socialMedias)
        {
            int id = Convert.ToInt32(TempData["id"]);
            if (id == socialMedias.SocailMediaID)
            {
                SocialMediasValidator validationRules = new SocialMediasValidator();
                ValidationResult validationResult = validationRules.Validate(socialMedias);
                if (validationResult.IsValid)
                {
                    var value = context.SocialMedias.Find(socialMedias.SocailMediaID);
                    value.Icon = socialMedias.Icon;
                    value.Name = socialMedias.Name;
                    value.URL = socialMedias.URL;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in validationResult.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                    var value = context.SocialMedias.Find(id);
                    TempData["id"] = id;
                    return View(value);
                }
            }
            else
            {
                return HttpNotFound();
            }
        }
        public ActionResult DeleteSocialMedia(int id)
        {
            var value = context.SocialMedias.Find(id);
            context.SocialMedias.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}