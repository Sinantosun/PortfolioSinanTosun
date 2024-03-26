using FluentValidation.Results;
using MyPortfolio.Models;
using MyPortfolio.ValidationRules.TeamsValidatiors;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{

    public class TeamsController : Controller
    {
        MyPortfolioEntities context = new MyPortfolioEntities();
        public ActionResult Index()
        {
            var value = context.Teams.ToList();

            return View(value);
        }
        [HttpGet]
        public ActionResult AddTeams()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTeams(Teams teams)
        {
            if (teams.ImageURL != null)
            {
                TeamsValidator validationRules = new TeamsValidator();
                ValidationResult validationResult = validationRules.Validate(teams);
                if (validationResult.IsValid)
                {
                    var fileName = Guid.NewGuid();
                    var fileEx = Path.GetExtension(Request.Files[0].FileName);
                    var fullFileName = "~/Images/Teams/" + fileName + fileEx;
                    Request.Files[0].SaveAs(Server.MapPath(fullFileName));
                    teams.ImageURL = "/Images/Teams/" + fileName + fileEx;
                    context.Teams.Add(teams);
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
            else
            {
                ModelState.AddModelError("ImageURL", "Resim Seçiniz.");
                return View();
            }


        }

        [HttpGet]
        public ActionResult UpdateTeams(int id)
        {
            var value = context.Teams.Find(id);
            TempData["id"] = id;
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateTeams(Teams teams)
        {
            int id = Convert.ToInt32(TempData["id"]);
            if (id == teams.TeamID)
            {
                TeamsValidator validationRules = new TeamsValidator();
                ValidationResult validationResult = validationRules.Validate(teams);
                if (validationResult.IsValid)
                {


                    var value = context.Teams.Find(teams.TeamID);
                    value.GithubURL = teams.GithubURL;
                    value.InstagramURL = teams.InstagramURL;
                    value.LinkedinURL = teams.LinkedinURL;
                    value.NameSurname = teams.NameSurname;
                    value.Title = teams.Title;
                    value.Description = teams.Description;

                    if (teams.ImageURL != null)
                    {
                        var fileName = Guid.NewGuid();
                        var fileEx = Path.GetExtension(Request.Files[0].FileName);
                        var fullFileName = "~/Images/Teams/" + fileName + fileEx;
                        Request.Files[0].SaveAs(Server.MapPath(fullFileName));

                        if (System.IO.File.Exists(Server.MapPath(value.ImageURL)))
                        {
                            System.IO.File.Delete(Server.MapPath(value.ImageURL));
                        }

                        value.ImageURL = "/Images/Teams/" + fileName + fileEx;


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
                    var value = context.Teams.Find(id);
                    TempData["id"] = id;
                    return View(value);
                  
                }

            }
            else
            {
                return HttpNotFound();
            }

        }


        public ActionResult DeleteTeams(int id)
        {
            var value = context.Teams.Find(id);
            context.Teams.Remove(value);

            if (System.IO.File.Exists(Server.MapPath(value.ImageURL)))
            {
                System.IO.File.Delete(Server.MapPath(value.ImageURL));
            }

            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}