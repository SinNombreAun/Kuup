using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentacion.Kuup.Controllers
{
    public class ErrorController : BaseController
    {
        public ActionResult NotFound()
        {
            return View();
        }
    }
}