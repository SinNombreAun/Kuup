using Funciones.Kuup.Adicionales;
using Negocio.Kuup.Clases;
using Presentacion.Kuup.Nucleo.Motores;
using System;
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
        public bool ValidaFuncionalidad(short NumeroDePantalla, byte Funcionalidad)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, string.Empty);
            if(!ClsFuncionesPerfiles.getList().Exists(x => x.NumeroDePerfil == MoSesion.NumeroDePerfil && x.NumeroDePantalla == NumeroDePantalla && x.NumeroDeFuncionalidad == Funcionalidad))
            {
                switch (Funcionalidad)
                {
                    case (byte)ClsEnumerables.Funcionalidades.ACCESO:
                        Resultado.Mensaje = "No cuenta con privilegios de Acceso";
                        break;
                    case (byte)ClsEnumerables.Funcionalidades.ALTA:
                        Resultado.Mensaje = "No cuenta con privilegios de Alta";
                        break;
                    case (byte)ClsEnumerables.Funcionalidades.BAJA:
                        Resultado.Mensaje = "No cuenta con privilegios de Baja";
                        break;
                    case (byte)ClsEnumerables.Funcionalidades.DETALLE:
                        Resultado.Mensaje = "No cuenta con privilegios de Detalle";
                        break;
                    case (byte)ClsEnumerables.Funcionalidades.EDITA:
                        Resultado.Mensaje = "No cuenta con privilegios de Edita";
                        break;
                    case (byte)ClsEnumerables.Funcionalidades.IMPORTAR:
                        Resultado.Mensaje = "No cuenta con privilegios de Importación";
                        break;
                }
                Resultado.Resultado = false;
                TempData["Resultado"] = Resultado.MensajeController();
                return false;
            }
            return true;
        }
    }
}