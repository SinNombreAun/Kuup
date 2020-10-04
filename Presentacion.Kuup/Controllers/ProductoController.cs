using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Funciones.Kuup.Adicionales;
using Negocio.Kuup.Clases;
using Presentacion.Kuup.Models;
using Presentacion.Kuup.Nucleo.Motores;

namespace Presentacion.Kuup.Controllers
{
    public class ProductoController : BaseController
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
            ProductoModel Entidad = new ProductoModel();
            this.CargaCombos(Entidad);
            return View();
        }
        [HttpPost]
        public ActionResult Alta(ProductoModel RegistroCapturado)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado();
            if (ModelState.IsValid)
            {
                RegistroCapturado.fFechaDeRegistro = DateTime.Now;
                RegistroCapturado.fCantidadDeProductoNueva = RegistroCapturado.CantidadDeProductoTotal;
                RegistroCapturado.fCveEstatus = (byte)ClsEnumerables.CveEstatusGeneral.ACTIVO;
                if (!RegistroCapturado.Existe())
                {
                    if (RegistroCapturado.Insert())
                    {
                        return RedirectToAction("Detalle", "Producto", new { RegistroCapturado.NumeroDeProducto });
                    }
                    else
                    {
                        Resultado.Resultado = false;
                        Resultado.Mensaje = "Ocurrio un error al insertar el Producto";
                    }
                }
                else
                {
                    Resultado.Resultado = false;
                    Resultado.Mensaje = "El producto a registrar ya Existe";
                }
            }
            else
            {
                Resultado.Resultado = false;
                Resultado.Mensaje = "Campos incorrectos";
            }
            this.CargaCombos(RegistroCapturado);
            ViewBag.Resultado = Resultado;
            return View(RegistroCapturado);
        }
        [HttpGet]
        public ActionResult Detalle(short NumeroDeProducto)
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            ProductoModel Producto = new ProductoModel((from q in ClsProductos.getList() where q.NumeroDeProducto == NumeroDeProducto select q).ToList().FirstOrDefault());
            if (Producto == null)
            {
                return RedirectToAction("Index", "Producto");
            }
            this.CargaCombos(Producto);
            return View(Producto);
        }
        private void CargaCombos(ProductoModel Entidad)
        {
            ViewBag.CveAviso = ClsAdicional.ClsCargaCombo.CargaComboClave(4, Entidad.CveAviso.ToString());
            ViewBag.CveCorreoSurtido = ClsAdicional.ClsCargaCombo.CargaComboClave(4, Entidad.CveCorreoSurtido.ToString());
            ViewBag.NumeroDeProveedor = ClsAdicional.ClsCargaCombo.CargaComboProveedor(Entidad.NumeroDeProveedor);
            ViewBag.CveAplicaMayoreo = ClsAdicional.ClsCargaCombo.CargaComboClave(4, Entidad.CveAplicaMayoreo.ToString());
            ViewBag.CveEstatus = ClsAdicional.ClsCargaCombo.CargaComboClave(1, Entidad.CveEstatus.ToString());
        }
    }
}
