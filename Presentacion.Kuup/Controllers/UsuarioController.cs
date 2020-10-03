using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Presentacion.Kuup.Models;

namespace Presentacion.Kuup.Controllers
{
    public class UsuarioController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Alta()
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Alta(UsuarioModel Alta)
        {

            return View();
        }
    }
}
