using FluentValidation.Results;
using MyPortfolio.Models;
using MyPortfolio.ValidationRules.MessagesValidators;
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
        public PartialViewResult DefaultSkillsPartial()
        {
            var value = context.Skills.ToList();
            return PartialView(value);
        }
        public PartialViewResult DefaultServicesPartial()
        {
            var value = context.Services.Where(x => x.Status == true).ToList();
            return PartialView(value);
        }
        public PartialViewResult DefaultExperincePartial()
        {
            var value = context.Experiences.ToList();
            return PartialView(value);
        }
        public PartialViewResult DefaulProjectPartial()
        {
            var categoiries = context.Categories.ToList();
            ViewBag.Categories = categoiries;
            var value = context.Projects.Take(6).ToList();
            return PartialView(value);
        }
        public PartialViewResult DefaulTestimonailPartial()
        {
            var value = context.Testimonials.ToList();
            return PartialView(value);
        }
        public PartialViewResult DefaulTeamPartial()
        {
            var value = context.Teams.ToList();
            return PartialView(value);
        }
        [HttpGet]
        public PartialViewResult DefaulContactPartial()
        {
           
            return PartialView();
        }

        [HttpPost]
        public ActionResult DefaulContactPartial(Messages messages)
        {
            MessageValidator validationRules = new MessageValidator();
            ValidationResult validationResult = validationRules.Validate(messages);
            if (validationResult.IsValid)
            {
                messages.IsRead = false;
                messages.MessageDate = Convert.ToDateTime(DateTime.Now.ToString("f"));
                context.Messages.Add(messages);
                
                context.SaveChanges();
                return RedirectToAction("../Default/Index");
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

        public ActionResult ProductDetails(int id)
        {
            var value = context.Projects.Find(id);
            return View(value);
        }
    }

}