using Funciones.Kuup.Adicionales;
using Negocio.Kuup.Clases;
using Presentacion.Kuup.Nucleo.Funciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Presentacion.Kuup.Controllers
{
    public class VentaTotalController : BaseController
    {
        ClsOperaVentaTotal Opera = new ClsOperaVentaTotal();
        [HttpGet]
        public ActionResult Index()
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            return View();
        }
        public JsonResult RegistraVentaTotal(String RegistroVenta)
        {
            return Json(Opera.RegistroDeVenta(RegistroVenta), JsonRequestBehavior.AllowGet);
        }
        public JsonResult CargaProducto(String NombreOCodigoDeProducto)
        {
            List<ClsProductos> Productos = new List<ClsProductos>();
            List<ClsConfiguraPaquetes> Paquetes = new List<ClsConfiguraPaquetes>();
            ClsProductos Producto = new ClsProductos();
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            Productos = (from q in ClsProductos.getList() where q.CodigoDeBarras == NombreOCodigoDeProducto select q).ToList();
            if (Productos.Count == 0)
            {
                Productos = (from q in ClsProductos.getList() where q.NombreDeProducto == NombreOCodigoDeProducto select q).ToList();
            }

            if (Productos.Count != 0)
            {
                Producto = Productos.FirstOrDefault();

                Paquetes = (from q in ClsConfiguraPaquetes.getList() where q.NumeroDeProductoPadre == Producto.NumeroDeProducto select q).ToList();
                if (Paquetes.Count() == 0)
                {
                    Paquetes = (from q in ClsConfiguraPaquetes.getList() where q.NumeroDeProductoHijo == Producto.NumeroDeProducto select q).ToList();
                }
            }
            else
            {
                Resultado.Resultado = false;
                Resultado.Mensaje = "No fue posible encontrar el producto a registrar";
            }
            return Json(new { Resultado, Producto, TienePaquetes = Paquetes.Count() != 0, Paquetes }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ProductoParaTabla(String NombreOCodigoDeProducto, short Cantidad, String Paquetes, String RegistrosPrevios)
        {
            List<ClsVentas> RegistrosPrev = ClsAdicional.Deserializar<List<ClsVentas>>(RegistrosPrevios);
            if (RegistrosPrev == null)
            {
                RegistrosPrev = new List<ClsVentas>();
            }
            List<ClsProductos> Productos = new List<ClsProductos>();
            List<ClsVentas> Registro = new List<ClsVentas>();
            List<ClsConfiguraPaquetes> ListaPaquetes = new List<ClsConfiguraPaquetes>();
            if (!string.IsNullOrEmpty(Paquetes))
            {
                ListaPaquetes = (from q in ClsConfiguraPaquetes.getList() where q.NumeroDeProductoPadre == ClsAdicional.Convert<short>(Paquetes.Split('_')[0]) && q.NumeroDeProductoHijo == ClsAdicional.Convert<short>(Paquetes.Split('_')[1]) select q).ToList();
                if (ListaPaquetes.Count() == 1)
                {
                    if (!RegistrosPrev.Exists(x => x.NumeroDeProducto == ListaPaquetes.FirstOrDefault().NumeroDeProductoPadre))
                    {
                        Productos = (from q in ClsProductos.getList() where q.NumeroDeProducto == ListaPaquetes.FirstOrDefault().NumeroDeProductoPadre select q).ToList();
                        Registro.Add(new ClsVentas()
                        {
                            NumeroDeProducto = ListaPaquetes.FirstOrDefault().NumeroDeProductoPadre,
                            CodigoDeBarras = Productos.FirstOrDefault().CodigoDeBarras,
                            CantidadDeProducto = Cantidad,
                            ImporteDeProducto = Math.Round(Cantidad * ListaPaquetes.FirstOrDefault().PrecioDeProductoPadre, 2),
                            PrecioUnitario = ListaPaquetes.FirstOrDefault().PrecioDeProductoPadre,
                            NombreDeProducto = ListaPaquetes.FirstOrDefault().NombreDeProductoPadre
                        });
                    }
                    else
                    {
                        var Previo = RegistrosPrev.FindAll(x => x.NumeroDeProducto == ListaPaquetes.FirstOrDefault().NumeroDeProductoPadre).FirstOrDefault();
                        Registro.Add(new ClsVentas()
                        {
                            NumeroDeProducto = Previo.NumeroDeProducto,
                            CodigoDeBarras = Previo.CodigoDeBarras,
                            CantidadDeProducto = (short)(Cantidad + Previo.CantidadDeProducto),
                            ImporteDeProducto = Math.Round((short)(Cantidad + Previo.CantidadDeProducto) * ListaPaquetes.FirstOrDefault().PrecioDeProductoPadre, 2),
                            PrecioUnitario = ListaPaquetes.FirstOrDefault().PrecioDeProductoPadre,
                            NombreDeProducto = ListaPaquetes.FirstOrDefault().NombreDeProductoPadre
                        });
                    }
                    if (!RegistrosPrev.Exists(x => x.NumeroDeProducto == ListaPaquetes.FirstOrDefault().NumeroDeProductoHijo))
                    {
                        Productos = (from q in ClsProductos.getList() where q.NumeroDeProducto == ListaPaquetes.FirstOrDefault().NumeroDeProductoHijo select q).ToList();
                        Registro.Add(new ClsVentas()
                        {
                            NumeroDeProducto = ListaPaquetes.FirstOrDefault().NumeroDeProductoHijo,
                            CodigoDeBarras = Productos.FirstOrDefault().CodigoDeBarras,
                            CantidadDeProducto = Cantidad,
                            ImporteDeProducto = Math.Round(Cantidad * (decimal)ListaPaquetes.FirstOrDefault().PrecioDeProductoHijo, 2),
                            PrecioUnitario = (decimal)ListaPaquetes.FirstOrDefault().PrecioDeProductoHijo,
                            NombreDeProducto = ListaPaquetes.FirstOrDefault().NombreDeProductoHijo
                        });
                    }
                    else
                    {
                        var Previo = RegistrosPrev.FindAll(x => x.NumeroDeProducto == ListaPaquetes.FirstOrDefault().NumeroDeProductoHijo).FirstOrDefault();
                        Registro.Add(new ClsVentas()
                        {
                            NumeroDeProducto = Previo.NumeroDeProducto,
                            CodigoDeBarras = Previo.CodigoDeBarras,
                            CantidadDeProducto = (short)(Cantidad + Previo.CantidadDeProducto),
                            ImporteDeProducto = Math.Round((short)(Cantidad + Previo.CantidadDeProducto) * (decimal)ListaPaquetes.FirstOrDefault().PrecioDeProductoHijo, 2),
                            PrecioUnitario = (decimal)ListaPaquetes.FirstOrDefault().PrecioDeProductoHijo,
                            NombreDeProducto = ListaPaquetes.FirstOrDefault().NombreDeProductoHijo
                        });
                    }
                }
            }
            else
            {
                bool Existe = false;
                short NumeroDeProducto = 0;
                if (RegistrosPrev.Exists(x => x.CodigoDeBarras == NombreOCodigoDeProducto))
                {
                    NumeroDeProducto = RegistrosPrev.Where(x => x.CodigoDeBarras == NombreOCodigoDeProducto).FirstOrDefault().NumeroDeProducto;
                    Existe = true;
                }
                else if (RegistrosPrev.Exists(x => x.NombreDeProducto == NombreOCodigoDeProducto))
                {
                    NumeroDeProducto = RegistrosPrev.Where(x => x.NombreDeProducto == NombreOCodigoDeProducto).FirstOrDefault().NumeroDeProducto;
                    Existe = true;
                }
                if (!Existe)
                {
                    Productos = (from q in ClsProductos.getList() where q.CodigoDeBarras == NombreOCodigoDeProducto select q).ToList();
                    if (Productos.Count == 0)
                    {
                        Productos = (from q in ClsProductos.getList() where q.NombreDeProducto == NombreOCodigoDeProducto select q).ToList();
                    }
                    if (Productos.Count != 0)
                    {
                        decimal PrecioUnitario = Productos.FirstOrDefault().PrecioUnitario;
                        if (Productos.FirstOrDefault().CveAplicaMayoreo == 1)
                        {
                            if (Productos.FirstOrDefault().CantidadMinimaMayoreo >= Cantidad)
                            {
                                PrecioUnitario = (decimal)Productos.FirstOrDefault().PrecioMayoreo;
                            }
                        }
                        Registro.Add(new ClsVentas()
                        {
                            NumeroDeProducto = Productos.FirstOrDefault().NumeroDeProducto,
                            CodigoDeBarras = Productos.FirstOrDefault().CodigoDeBarras,
                            CantidadDeProducto = Cantidad,
                            ImporteDeProducto = Math.Round(Cantidad * PrecioUnitario, 2),
                            PrecioUnitario = PrecioUnitario,
                            NombreDeProducto = Productos.FirstOrDefault().NombreDeProducto
                        });
                    }
                }
                else
                {
                    var Previo = RegistrosPrev.FindAll(x => x.NumeroDeProducto == NumeroDeProducto).FirstOrDefault();
                    Productos = (from q in ClsProductos.getList() where q.NumeroDeProducto == NumeroDeProducto select q).ToList();
                    decimal PrecioUnitario = Productos.FirstOrDefault().PrecioUnitario;
                    if (Productos.FirstOrDefault().CveAplicaMayoreo == 1)
                    {
                        if (Productos.FirstOrDefault().CantidadMinimaMayoreo >= (Cantidad + Previo.CantidadDeProducto))
                        {
                            PrecioUnitario = (decimal)Productos.FirstOrDefault().PrecioMayoreo;
                        }
                    }
                    Registro.Add(new ClsVentas()
                    {
                        NumeroDeProducto = Previo.NumeroDeProducto,
                        CodigoDeBarras = Previo.CodigoDeBarras,
                        CantidadDeProducto = (short)(Cantidad + Previo.CantidadDeProducto),
                        ImporteDeProducto = Math.Round((short)(Cantidad + Previo.CantidadDeProducto) * PrecioUnitario, 2),
                        PrecioUnitario = PrecioUnitario,
                        NombreDeProducto = Previo.NombreDeProducto
                    });
                }
            }
            return Json(new { Registro }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteDeProducto(String RegistroPrevio, String RegistrosPrevios)
        {
            List<ClsVentas> Registro = new List<ClsVentas>();
            ClsVentas RegistroPrev = ClsAdicional.Deserializar<ClsVentas>(RegistroPrevio);
            List<ClsVentas> RegistrosPrev = ClsAdicional.Deserializar<List<ClsVentas>>(RegistrosPrevios);
            List<ClsConfiguraPaquetes> Paquete = (from q in ClsConfiguraPaquetes.getList() where q.NumeroDeProductoPadre == RegistroPrev.NumeroDeProducto select q).ToList();
            if (Paquete.Count == 0)
            {
                Paquete = (from q in ClsConfiguraPaquetes.getList() where q.NumeroDeProductoHijo == RegistroPrev.NumeroDeProducto select q).ToList();
            }
            if (Paquete.Count != 0)
            {
                List<ClsVentas> RegistrosAEliminar = RegistrosPrev.FindAll(x => x.NumeroDeProducto == Paquete.FirstOrDefault().NumeroDeProductoPadre || x.NumeroDeProducto == Paquete.FirstOrDefault().NumeroDeProductoHijo).ToList();
                if (RegistrosAEliminar.Count() == 2)
                {
                    foreach (var RegistroEliminado in RegistrosAEliminar)
                    {
                        Registro.Add(new ClsVentas()
                        {
                            NumeroDeProducto = RegistroEliminado.NumeroDeProducto,
                            CodigoDeBarras = RegistroEliminado.CodigoDeBarras,
                            CantidadDeProducto = 0,
                            ImporteDeProducto = 0,
                            PrecioUnitario = 0,
                            NombreDeProducto = RegistroEliminado.NombreDeProducto
                        });
                    }
                }
                else if (RegistrosAEliminar.Count() == 1)
                {
                    Registro.Add(new ClsVentas()
                    {
                        NumeroDeProducto = RegistroPrev.NumeroDeProducto,
                        CodigoDeBarras = RegistroPrev.CodigoDeBarras,
                        CantidadDeProducto = 0,
                        ImporteDeProducto = 0,
                        PrecioUnitario = 0,
                        NombreDeProducto = RegistroPrev.NombreDeProducto
                    });
                }
            }
            else
            {
                Registro.Add(new ClsVentas()
                {
                    NumeroDeProducto = RegistroPrev.NumeroDeProducto,
                    CodigoDeBarras = RegistroPrev.CodigoDeBarras,
                    CantidadDeProducto = 0,
                    ImporteDeProducto = 0,
                    PrecioUnitario = 0,
                    NombreDeProducto = RegistroPrev.NombreDeProducto
                });
            }
            return Json(new { Registro }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AutoCompleteProducto(String Prefix)
        {
            List<ClsProductos> Productos = new List<ClsProductos>();
            Productos = (from q in ClsProductos.getList() where q.CodigoDeBarras.StartsWith(Prefix) select new ClsProductos() { NombreDeProducto = q.CodigoDeBarras }).ToList();
            if (Productos.Count == 0)
            {
                Productos = (from q in ClsProductos.getList() where q.NombreDeProducto.StartsWith(Prefix) select new ClsProductos() { NombreDeProducto = q.NombreDeProducto }).ToList();
            }
            return Json(Productos, JsonRequestBehavior.AllowGet);
        }
    }
}