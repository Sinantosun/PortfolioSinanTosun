using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio.Controllers
{
    public class AdminLayoutController : Controller
    {
        public PartialViewResult _AdminLayoutSideBar()
        {
            return PartialView();
        }

        public PartialViewResult _AdminLayoutHead()
        {
            return PartialView();
        }

    }
}