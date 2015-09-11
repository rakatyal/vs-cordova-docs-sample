using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACOM.DocumentationSample.Models;

namespace ACOM.DocumentationSample.Controllers
{
    public class LicenseController : Controller
    {
        public ActionResult Index()
        {
            var model = new License();
            model.PageTitle = "License- Visual Studio Cordova";
            return View(model);
        }
    }
}