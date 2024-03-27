using FluentValidation;
using FluentValidation.Results;
using MyPortfolio.Models;
using MyPortfolio.ValidationRules.ContactValidators;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {

        MyPortfolioEntities context = new MyPortfolioEntities();

        public ActionResult Index()
        {
            var values = context.Contacts.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddContact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddContact(Contacts contacts)
        {
            ContactValidator validationRules = new ContactValidator();
            ValidationResult validationResult = validationRules.Validate(contacts);
            if (validationResult.IsValid)
            {
                context.Contacts.Add(contacts);
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

        public ActionResult UpdateContact(int id)
        {
            TempData["id"] = id;
            var values = context.Contacts.Find(id);
            return View(values);
        }
        [HttpPost]
        public ActionResult UpdateContact(Contacts contacts)
        {
            int convertId = Convert.ToInt32(TempData["id"]);
            if (contacts.ContactID==convertId)
            {
                ContactValidator validationRules = new ContactValidator();
                ValidationResult validationResult = validationRules.Validate(contacts);
                if (validationResult.IsValid)
                {
                    var values = context.Contacts.Find(contacts.ContactID);
                    values.Address = contacts.Address;
                    values.Email = contacts.Email;
                    values.NameSurname = contacts.NameSurname;
                    values.Phone = contacts.Phone;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in validationResult.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                    var values = context.Contacts.Find(convertId);
                    TempData["id"] = convertId;
                    return View(values);
                }
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult DeleteContact(int id)
        {
            var values = context.Contacts.Find(id);
            context.Contacts.Remove(values);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}