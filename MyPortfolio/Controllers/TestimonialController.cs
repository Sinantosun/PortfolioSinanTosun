using FluentValidation.Results;
using MyPortfolio.Models;
using MyPortfolio.ValidationRules.TestimonialValidators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    public class TestimonialController : Controller
    {
        MyPortfolioEntities context = new MyPortfolioEntities();
        public ActionResult Index()
        {
            var value = context.Testimonials.ToList();
            return View(value);
        }

        [HttpGet]
        public ActionResult AddTestimionial()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTestimionial(Testimonials testimonials)
        {
            if (testimonials.ImageURL != null)
            {
                TestimonialValidator validationRules = new TestimonialValidator();
                ValidationResult validationResult = validationRules.Validate(testimonials);
                if (validationResult.IsValid)
                {
                    var guid = Guid.NewGuid();
                    var ex = Path.GetExtension(Request.Files[0].FileName);
                    var fullPath = "~/Images/Testimonials/" + guid + ex;
                    Request.Files[0].SaveAs(Server.MapPath(fullPath));
                    testimonials.ImageURL = "/Images/Testimonials/" + guid + ex;
                    context.Testimonials.Add(testimonials);
                    testimonials.Status = true;
                    testimonials.Date = Convert.ToDateTime(DateTime.Now.ToString("f"));
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
            }
            else
            {
                ModelState.AddModelError("ImageURL", "Resim Seçiniz.");
            }

            return View();
        }

        [HttpGet]
        public ActionResult UpdateTestimionial(int id)
        {
            var value = context.Testimonials.Find(id);
            TempData["id"] = id;
            return View(value);
        }

        [HttpPost]
        public ActionResult UpdateTestimionial(Testimonials testimonials)
        {
            int id = Convert.ToInt32(TempData["id"]);
            if (id == testimonials.TestimonialID)
            {
                TestimonialValidator validationRules = new TestimonialValidator();
                ValidationResult validationResult = validationRules.Validate(testimonials);
                if (validationResult.IsValid)
                {
                    var value = context.Testimonials.Find(testimonials.TestimonialID);

                    if (testimonials.ImageURL != null)
                    {
                        var guid = Guid.NewGuid();
                        var ex = Path.GetExtension(Request.Files[0].FileName);
                        var fullPath = "~/Images/Testimonials/" + guid + ex;
                        Request.Files[0].SaveAs(Server.MapPath(fullPath));

                        if (System.IO.File.Exists(Server.MapPath(value.ImageURL)))
                        {
                            System.IO.File.Delete(Server.MapPath(value.ImageURL));
                        }

                        value.ImageURL = "/Images/Testimonials/" + guid + ex;
                    }
                    value.Title = testimonials.Title;
                    value.Comment = testimonials.Comment;

                    value.NameSurname = testimonials.NameSurname;

                    value.Status = true;
                    value.Date = testimonials.Date;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in validationResult.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                    var value = context.Testimonials.Find(id);
                    TempData["id"] = id;
                    return View(value);
                }

            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult DeleteTestimionial(int id)
        {
            var value = context.Testimonials.Find(id);
            if (System.IO.File.Exists(Server.MapPath(value.ImageURL)))
            {
                System.IO.File.Delete(Server.MapPath(value.ImageURL));
            }


            context.Testimonials.Remove(value);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}