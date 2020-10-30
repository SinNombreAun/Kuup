using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Negocio.Kuup.Clases;

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