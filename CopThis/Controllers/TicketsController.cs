using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CopThis.Controllers
{
    public class TicketsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}