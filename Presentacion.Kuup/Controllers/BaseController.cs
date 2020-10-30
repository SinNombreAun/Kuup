using Presentacion.Kuup.Nucleo.Motores;
using System.Linq;
using System.Web.Mvc;

namespace Presentacion.Kuup.Controllers
{
    public class BaseController : Controller
    {
        public bool ValidaSesion()
        {
            if (MvcApplication.ListaDeSesionesActivas.ContainsKey(System.Web.HttpContext.Current.Session.SessionID))
            {
                if (!MvcApplication.ListaDeSesionesActivas[System.Web.HttpContext.Current.Session.SessionID].Deslogueado)
                {
                    return true;
                }
            }
            return false;
        }
    }
}