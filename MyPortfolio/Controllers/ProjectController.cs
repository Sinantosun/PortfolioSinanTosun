using FluentValidation.Results;
using MyPortfolio.Models;
using MyPortfolio.ValidationRules.ProjectValidators;
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
    public class ProjectController : Controller
    {
        MyPortfolioEntities context = new MyPortfolioEntities();
        public ActionResult Index()
        {
            var value = context.Projects.ToList();
            return View(value);
        }
        void loadDropDown()
        {
            var categories = context.Categories.ToList();
            List<SelectListItem> List = (from x in categories select new SelectListItem { Text = x.Name, Value = x.CategoryID.ToString() }).ToList();
            ViewBag.CategoryList = List;
        }
        [HttpGet]
        public ActionResult AddProject()
        {
            loadDropDown();
            return View();
        }
        [HttpPost]
        public ActionResult AddProject(Projects projects)
        {
            ProjectValidator validationRules = new ProjectValidator();
            ValidationResult validationResult = validationRules.Validate(projects);
            if (validationResult.IsValid)
            {
                if (string.IsNullOrEmpty(projects.Image))
                {
                    ModelState.AddModelError("Image", "Resim Seçiniz.");
                    loadDropDown();
                    return View();
                }
                else
                {
                    projects.Image = saveImage();
                    context.Projects.Add(projects);
                    context.SaveChanges();

                    return RedirectToAction("Index");
                }

           
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                loadDropDown();
                return View();
            }
        }
        
        string saveImage()
        {
            var guid = Guid.NewGuid();
            var ex = Path.GetExtension(Request.Files[0].FileName);
            string fullPath = "~/Images/Projects/" + guid + ex;
            Request.Files[0].SaveAs(Server.MapPath(fullPath));
            string dbPath = "/Images/Projects/" + guid + ex;
            return dbPath;
        }


        [HttpGet]
        public ActionResult UpdateProject(int id)
        {
            loadDropDown();

            var value = context.Projects.Find(id);

            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateProject(Projects projects)
        {
            ProjectValidator validationRules = new ProjectValidator();
            ValidationResult validationResult = validationRules.Validate(projects);
            if (validationResult.IsValid)
            {
                var value = context.Projects.Find(projects.ProjectID);
                value.Github = projects.Github;
              
                if (projects.Image != null)
                {

                    if (System.IO.File.Exists(Server.MapPath(value.Image)))
                    {
                        System.IO.File.Delete(Server.MapPath(value.Image));
                    }

                    value.Image = saveImage();

                }
                value.Name = projects.Name;
                value.Category = projects.Category;
                value.Description = projects.Description;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }

                loadDropDown();
                var value = context.Projects.Find(projects.ProjectID);

                return View(value);
            }


        }

        public ActionResult DeleteProject(int id)
        {
            var value = context.Projects.Find(id);

            context.Projects.Remove(value);
            context.SaveChanges();

            if (System.IO.File.Exists(Server.MapPath(value.Image)))
            {
                System.IO.File.Delete(Server.MapPath(value.Image));
            }


            return RedirectToAction("Index");
        }

    }
}