using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Presentacion.Kuup.Nucleo.Motores;

namespace Presentacion.Kuup.Controllers
{
    public class BaseController : Controller
    {
        public bool ValidaSesion()
        {
            if (System.Web.HttpContext.Current.Session["NumeroDePerfil"] == null)
            {
                return false;
            }
            else
            {
                if (MoSesion.NumeroDePerfil == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}