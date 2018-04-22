using Orchard.DisplayManagement;
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
        public ApplyController
            (
               IShapeFactory shapeFactory
            )
        {
            Shape = shapeFactory;
        }

        public dynamic Shape { get; set; }

        // GET: Apply
        public ActionResult Index()
        {
            return View(new OrderPart());
        }
    }
}