using Negocio.Kuup.Clases;
using Presentacion.Kuup.Nucleo.Motores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Kuup
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static Dictionary<Object, SesionActiva> ListaDeSesionesActivas = new Dictionary<Object, SesionActiva>();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            this.RegisterGlobalFilters(GlobalFilters.Filters);
            this.RegisterRoutes(RouteTable.Routes);
            //this.RegisterBundles(BundleTable.Bundles);
        }
        private void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Home",
                url: "Home/{action}",
                defaults: new { controller = "Home", action = "Index" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
        private void RegisterGlobalFilters(GlobalFilterCollection Filters)
        {
            Filters.Add(new HandleErrorAttribute());
        }
        protected void Application_Error(Object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            if (exception != null)
            {
                String TipoExcepction = exception.GetType().Name;
                if (TipoExcepction.Substring(0, 4) == "Http")
                {
                    Response.StatusCode = ((HttpException)exception).GetHttpCode();
                }
                Response.Clear();
                Funciones.Kuup.Adicionales.ClsAdicional.ClsManejaError MensajeError = new Funciones.Kuup.Adicionales.ClsAdicional.ClsManejaError();

                try
                {
                    if (Funciones.Kuup.Adicionales.ClsAdicional.ClsArchivos.FileExists(Server.MapPath("~/App_Data/Application_Error"), true, true))
                    {
                        if (Funciones.Kuup.Adicionales.ClsAdicional.ClsArchivos.FileExists(Server.MapPath("~/App_Data/Application_Error/Log.txt"), false, true))
                        {
                            String Mensaje = MensajeError.MensajeErrorException(exception, MoSesion.NombreDeUsuario, TipoExcepction, Response.StatusCode.ToString()) + Funciones.Kuup.Adicionales.ClsAdicional.ClsArchivos.LeeArchivo(Server.MapPath("~/App_Data/Application_Error/Log.txt"));

                            Funciones.Kuup.Adicionales.ClsAdicional.ClsArchivos.EscribeArchivo(Server.MapPath("~/App_Data/Application_Error/Log.txt"), Mensaje);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                try
                {
                    ClsBitacora.GeneraBitacora(10, 10, "Application_Error", MensajeError.MensajeErrorException(exception, MoSesion.NombreDeUsuario, TipoExcepction, Response.StatusCode.ToString()));
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
