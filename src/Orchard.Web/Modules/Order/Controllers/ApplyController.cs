using Orchard.Themes;
using Order.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Order.Controllers
{
    [Themed]
    public class ApplyController : Controller
    {
        // GET: Apply
        public ActionResult Index()
        {
            return View(new ItemDetail());
        }
    }
}