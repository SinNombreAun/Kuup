using Funciones.Kuup.Adicionales;
using Negocio.Kuup.Clases;
using Presentacion.Kuup.Nucleo.Funciones;
using Presentacion.Kuup.Nucleo.Motores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Presentacion.Kuup.Controllers
{
    public class VentaTotalController : BaseController
    {
        readonly ClsOperaVentaTotal Opera = new ClsOperaVentaTotal();
        readonly short NumeroDePantalla = (new ClsVentasTotales()).NumeroDePantallaKuup;
        [HttpGet]
        public ActionResult Index()
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.ACCESO))
            {
                return RedirectToAction("Index", "Home");
            }
            CargaCombos();
            return View();
        }
        [HttpPost]
        public JsonResult RegistraVentaTotal(decimal importeEntregado, decimal importeCambio, String registroVenta)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            if (!ValidaSesion())
            {
                return Json(new { UrlAccount = Url.Action("LoginOut", "Account") }, JsonRequestBehavior.AllowGet);
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.ALTA))
            {
                return Json(new { UrlFun = Url.Action("Index", "VentaTotal") }, JsonRequestBehavior.AllowGet);
            }
            return Json(Opera.RegistroDeVenta(importeEntregado, importeCambio, registroVenta), JsonRequestBehavior.AllowGet);
        }
        public JsonResult CargaProducto(String NombreOCodigoDeProducto, short NumeroDeProducto = 0)
        {
            List<ClsProductos> Productos = new List<ClsProductos>();
            List<ClsConfiguraPaquetes> Paquetes = new List<ClsConfiguraPaquetes>();
            ClsProductos Producto = new ClsProductos();
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            String Filtro = String.Empty;
            if (NumeroDeProducto == 0)
            {
                Filtro = String.Format("CodigoDeBarras == \"{0}\" && CveDeEstatus == {1}", NombreOCodigoDeProducto, (byte)ClsEnumerables.CveDeEstatusGeneral.ACTIVO);
                Productos = ClsProductos.getList(Filtro);
                if (Productos.Count == 0)
                {
                    Filtro = String.Format("NombreDeProducto == \"{0}\" && CveDeEstatus == {1}", NombreOCodigoDeProducto, (byte)ClsEnumerables.CveDeEstatusGeneral.ACTIVO);
                    Productos = ClsProductos.getList(Filtro);
                }
            }
            else
            {
                Filtro = String.Format("NumeroDeProducto == {0} && CveDeEstatus == {1}", NumeroDeProducto, (byte)ClsEnumerables.CveDeEstatusGeneral.ACTIVO);
                Productos = ClsProductos.getList(Filtro);
            }

            if (Productos.Count != 0)
            {
                Producto = Productos.FirstOrDefault();
                Filtro = String.Format("NumeroDeProductoPadre == {0}", Producto.NumeroDeProducto);
                Paquetes = ClsConfiguraPaquetes.getList(Filtro);
                if (Paquetes.Count() == 0)
                {
                    Filtro = String.Format("NumeroDeProductoHijo == {0}", Producto.NumeroDeProducto);
                    Paquetes = ClsConfiguraPaquetes.getList(Filtro);
                }
            }
            else
            {
                Resultado.Resultado = false;
                Resultado.Mensaje = "No fue posible encontrar el producto a registrar";
            }
            return Json(new { Resultado, Producto, TienePaquetes = Paquetes.Count() != 0, Paquetes , Productos }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ProductoParaTablaV2(short NumeroDeProducto, short Cantidad, String Paquetes, String RegistrosPrevios)
        {
            List<ClsVentas> RegistrosPrev = ClsAdicional.Deserializar<List<ClsVentas>>(RegistrosPrevios);
            if (RegistrosPrev == null)
            {
                RegistrosPrev = new List<ClsVentas>();
            }
            List<ClsVentas> Registro = new List<ClsVentas>();
            List<ClsConfiguraPaquetes> ListaPaquetes = new List<ClsConfiguraPaquetes>();
            String Filtro = String.Empty;
            short CantidadFija = Cantidad;
            if (!String.IsNullOrEmpty(Paquetes))
            {
                Filtro = String.Format("NumeroDeProductoPadre == {0} && NumeroDeProductoHijo == {1}", ClsAdicional.Convert<short>(Paquetes.Split('_')[0]), ClsAdicional.Convert<short>(Paquetes.Split('_')[1]));
                ListaPaquetes = ClsConfiguraPaquetes.getList(Filtro);
                if (ListaPaquetes.Count() == 1)
                {
                    if(RegistrosPrev.Exists(x => x.NumeroDeProducto == ListaPaquetes.FirstOrDefault().NumeroDeProductoPadre))
                    {
                        CantidadFija = (short)(CantidadFija + RegistrosPrev.FindAll(x => x.NumeroDeProducto == ListaPaquetes.FirstOrDefault().NumeroDeProductoPadre).FirstOrDefault().CantidadDeProducto);
                    }
                    ClsProductos Producto = ClsProductos.getList("NumeroDeProducto == " + ListaPaquetes.FirstOrDefault().NumeroDeProductoPadre.ToString() + " && CodigoDeBarras == \"" + ListaPaquetes.FirstOrDefault().CodigoDeBarrasPadre + "\" && CveDeEstatus == " + (byte)ClsEnumerables.CveDeEstatusGeneral.ACTIVO).FirstOrDefault();
                    decimal PrecioUnitario = ListaPaquetes.FirstOrDefault().PrecioDeProductoPadre;
                    short CantidadParaMayoreo = CantidadFija;
                    if (RegistrosPrev.Exists(x => x.NumeroDeTipoDeProducto == Producto.NumeroDeTipoDeProducto && x.NumeroDeMarca == Producto.NumeroDeMarca && x.NumeroDeProducto != Producto.NumeroDeProducto))
                    {
                        CantidadParaMayoreo = (short)(RegistrosPrev.FindAll(x => x.NumeroDeTipoDeProducto == Producto.NumeroDeTipoDeProducto && x.NumeroDeMarca == Producto.NumeroDeMarca).Sum(y => y.CantidadDeProducto) + CantidadFija);
                    }
                    Filtro = String.Format("NumeroDeProducto == {0} && CodigoDeBarras == \"{1}\" && CantidadMinima <= {2} && CantidadMaxima >= {2}", ListaPaquetes.FirstOrDefault().NumeroDeProductoPadre, ListaPaquetes.FirstOrDefault().CodigoDeBarrasPadre, CantidadParaMayoreo);
                    List<ClsConfiguraMayoreos> Mayoreo = ClsConfiguraMayoreos.getList(Filtro);
                    bool EsMayoreo = false;
                    if (Mayoreo.Count() != 0)
                    {
                        PrecioUnitario = Mayoreo.FirstOrDefault().PrecioDeMayoreo;
                        EsMayoreo = true;
                    }
                    if (RegistrosPrev.Exists(x => x.NumeroDeTipoDeProducto == Producto.NumeroDeTipoDeProducto && x.NumeroDeMarca == Producto.NumeroDeMarca && x.NumeroDeProducto != Producto.NumeroDeProducto))
                    {
                        foreach (ClsVentas venta in RegistrosPrev.FindAll(x => x.NumeroDeTipoDeProducto == Producto.NumeroDeTipoDeProducto && x.NumeroDeMarca == Producto.NumeroDeMarca && x.NumeroDeProducto != Producto.NumeroDeProducto))
                        {
                            Registro.Add(new ClsVentas()
                            {
                                NumeroDeProducto = venta.NumeroDeProducto,
                                CodigoDeBarras = venta.CodigoDeBarras,
                                NumeroDeTipoDeProducto = venta.NumeroDeTipoDeProducto,
                                NumeroDeMarca = venta.NumeroDeMarca,
                                CantidadDeProducto = venta.CantidadDeProducto,
                                ImporteDeProducto = Math.Round(venta.CantidadDeProducto * (EsMayoreo ? PrecioUnitario : venta.PrecioUnitario), 2),
                                PrecioUnitario = (EsMayoreo ? PrecioUnitario : venta.PrecioUnitario),
                                NombreDeProducto = venta.NombreDeProducto
                            });
                        }
                    }
                    Registro.Add(new ClsVentas()
                    {
                        NumeroDeProducto = ListaPaquetes.FirstOrDefault().NumeroDeProductoPadre,
                        CodigoDeBarras = ListaPaquetes.FirstOrDefault().CodigoDeBarrasPadre,
                        NumeroDeTipoDeProducto = Producto.NumeroDeTipoDeProducto,
                        NumeroDeMarca = Producto.NumeroDeMarca,
                        CantidadDeProducto = CantidadFija,
                        ImporteDeProducto = Math.Round(CantidadFija * PrecioUnitario, 2),
                        PrecioUnitario = PrecioUnitario,
                        NombreDeProducto = ListaPaquetes.FirstOrDefault().NombreDeProductoPadre
                    });
                    CantidadFija = Cantidad;
                    if (RegistrosPrev.Exists(x => x.NumeroDeProducto == ListaPaquetes.FirstOrDefault().NumeroDeProductoHijo))
                    {
                        CantidadFija = (short)(CantidadFija + RegistrosPrev.FindAll(x => x.NumeroDeProducto == ListaPaquetes.FirstOrDefault().NumeroDeProductoHijo).FirstOrDefault().CantidadDeProducto);
                    }
                    Producto = ClsProductos.getList("NumeroDeProducto == " + ListaPaquetes.FirstOrDefault().NumeroDeProductoHijo.ToString() + " && CodigoDeBarras == \"" + ListaPaquetes.FirstOrDefault().CodigoDeBarrasHijo + "\" && CveDeEstatus == " + (byte)ClsEnumerables.CveDeEstatusGeneral.ACTIVO).FirstOrDefault();
                    PrecioUnitario = (decimal)ListaPaquetes.FirstOrDefault().PrecioDeProductoHijo;
                    CantidadParaMayoreo = CantidadFija;
                    if (RegistrosPrev.Exists(x => x.NumeroDeTipoDeProducto == Producto.NumeroDeTipoDeProducto && x.NumeroDeMarca == Producto.NumeroDeMarca && x.NumeroDeProducto != Producto.NumeroDeProducto))
                    {
                        CantidadParaMayoreo = (short)(RegistrosPrev.FindAll(x => x.NumeroDeTipoDeProducto == Producto.NumeroDeTipoDeProducto && x.NumeroDeMarca == Producto.NumeroDeMarca).Sum(y => y.CantidadDeProducto) + CantidadFija);
                    }
                    Filtro = String.Format("NumeroDeProducto == {0} && CodigoDeBarras == \"{1}\" && CantidadMinima <= {2} && CantidadMaxima >= {2}", ListaPaquetes.FirstOrDefault().NumeroDeProductoHijo, ListaPaquetes.FirstOrDefault().CodigoDeBarrasHijo, CantidadParaMayoreo);
                    Mayoreo = ClsConfiguraMayoreos.getList(Filtro);
                    EsMayoreo = false;
                    if (Mayoreo.Count() != 0)
                    {
                        PrecioUnitario = Mayoreo.FirstOrDefault().PrecioDeMayoreo;
                        EsMayoreo = true;
                    }
                    if (RegistrosPrev.Exists(x => x.NumeroDeTipoDeProducto == Producto.NumeroDeTipoDeProducto && x.NumeroDeMarca == Producto.NumeroDeMarca && x.NumeroDeProducto != Producto.NumeroDeProducto))
                    {
                        foreach (ClsVentas venta in RegistrosPrev.FindAll(x => x.NumeroDeTipoDeProducto == Producto.NumeroDeTipoDeProducto && x.NumeroDeMarca == Producto.NumeroDeMarca && x.NumeroDeProducto != Producto.NumeroDeProducto))
                        {
                            Registro.Add(new ClsVentas()
                            {
                                NumeroDeProducto = venta.NumeroDeProducto,
                                CodigoDeBarras = venta.CodigoDeBarras,
                                NumeroDeTipoDeProducto = venta.NumeroDeTipoDeProducto,
                                NumeroDeMarca = venta.NumeroDeMarca,
                                CantidadDeProducto = venta.CantidadDeProducto,
                                ImporteDeProducto = Math.Round(venta.CantidadDeProducto * (EsMayoreo ? PrecioUnitario : venta.PrecioUnitario), 2),
                                PrecioUnitario = (EsMayoreo ? PrecioUnitario : venta.PrecioUnitario),
                                NombreDeProducto = venta.NombreDeProducto
                            });
                        }
                    }
                    Registro.Add(new ClsVentas()
                    {
                        NumeroDeProducto = ListaPaquetes.FirstOrDefault().NumeroDeProductoHijo,
                        CodigoDeBarras = ListaPaquetes.FirstOrDefault().CodigoDeBarrasHijo,
                        NumeroDeTipoDeProducto = Producto.NumeroDeTipoDeProducto,
                        NumeroDeMarca = Producto.NumeroDeMarca,
                        CantidadDeProducto = CantidadFija,
                        ImporteDeProducto = Math.Round(CantidadFija * PrecioUnitario, 2),
                        PrecioUnitario = PrecioUnitario,
                        NombreDeProducto = ListaPaquetes.FirstOrDefault().NombreDeProductoHijo
                    });
                }
            }
            else
            {
                CantidadFija = Cantidad;
                if (RegistrosPrev.Exists(x => x.NumeroDeProducto == NumeroDeProducto))
                {
                    CantidadFija = (short)(CantidadFija + RegistrosPrev.FindAll(x => x.NumeroDeProducto == NumeroDeProducto).FirstOrDefault().CantidadDeProducto);
                }
                ClsProductos Producto = ClsProductos.getList("NumeroDeProducto == " + NumeroDeProducto.ToString() + " && CveDeEstatus == " + (byte)ClsEnumerables.CveDeEstatusGeneral.ACTIVO).FirstOrDefault();
                decimal PrecioUnitario = Producto.PrecioUnitario;
                short CantidadParaMayoreo = CantidadFija;
                if (RegistrosPrev.Exists(x => x.NumeroDeTipoDeProducto == Producto.NumeroDeTipoDeProducto && x.NumeroDeMarca == Producto.NumeroDeMarca && x.NumeroDeProducto != NumeroDeProducto))
                {
                    CantidadParaMayoreo = (short)(RegistrosPrev.FindAll(x => x.NumeroDeTipoDeProducto == Producto.NumeroDeTipoDeProducto && x.NumeroDeMarca == Producto.NumeroDeMarca).Sum(y => y.CantidadDeProducto) + CantidadFija);
                }
                Filtro = String.Format("NumeroDeProducto == {0} && CodigoDeBarras == \"{1}\" && CantidadMinima <= {2} && CantidadMaxima >= {2}", Producto.NumeroDeProducto, Producto.CodigoDeBarras, CantidadParaMayoreo);
                List<ClsConfiguraMayoreos> Mayoreo = ClsConfiguraMayoreos.getList(Filtro);
                bool EsMayoreo = false;
                if (Mayoreo.Count() != 0)
                {
                    PrecioUnitario = Mayoreo.FirstOrDefault().PrecioDeMayoreo;
                    EsMayoreo = true;
                }
                if (RegistrosPrev.Exists(x => x.NumeroDeTipoDeProducto == Producto.NumeroDeTipoDeProducto && x.NumeroDeMarca == Producto.NumeroDeMarca && x.NumeroDeProducto != Producto.NumeroDeProducto))
                {
                    foreach (ClsVentas venta in RegistrosPrev.FindAll(x => x.NumeroDeTipoDeProducto == Producto.NumeroDeTipoDeProducto && x.NumeroDeMarca == Producto.NumeroDeMarca && x.NumeroDeProducto != Producto.NumeroDeProducto))
                    {
                        Registro.Add(new ClsVentas()
                        {
                            NumeroDeProducto = venta.NumeroDeProducto,
                            CodigoDeBarras = venta.CodigoDeBarras,
                            NumeroDeTipoDeProducto = venta.NumeroDeTipoDeProducto,
                            NumeroDeMarca = venta.NumeroDeMarca,
                            CantidadDeProducto = venta.CantidadDeProducto,
                            ImporteDeProducto = Math.Round(venta.CantidadDeProducto * (EsMayoreo ? PrecioUnitario : venta.PrecioUnitario), 2),
                            PrecioUnitario = (EsMayoreo ? PrecioUnitario : venta.PrecioUnitario),
                            NombreDeProducto = venta.NombreDeProducto
                        });
                    }
                }
                Registro.Add(new ClsVentas()
                {
                    NumeroDeProducto = Producto.NumeroDeProducto,
                    CodigoDeBarras = Producto.CodigoDeBarras,
                    NumeroDeTipoDeProducto = Producto.NumeroDeTipoDeProducto,
                    NumeroDeMarca = Producto.NumeroDeMarca,
                    CantidadDeProducto = CantidadFija,
                    ImporteDeProducto = Math.Round(CantidadFija * PrecioUnitario, 2),
                    PrecioUnitario = PrecioUnitario,
                    NombreDeProducto = Producto.NombreDeProducto
                });
            }
            return Json(new { Registro }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteDeProducto(String RegistroPrevio, String RegistrosPrevios)
        {
            List<ClsVentas> Registro = new List<ClsVentas>();
            ClsVentas RegistroPrev = ClsAdicional.Deserializar<ClsVentas>(RegistroPrevio);
            List<ClsVentas> RegistrosPrev = ClsAdicional.Deserializar<List<ClsVentas>>(RegistrosPrevios);
            List<ClsConfiguraPaquetes> Paquete = new List<ClsConfiguraPaquetes>();
            String Filtro = String.Empty;
            bool EsHijo = false;
            if (RegistrosPrev.FindAll(x => x.NumeroDeProducto != RegistroPrev.NumeroDeProducto).Count() != 0)
            {
                foreach (var item in RegistrosPrev.FindAll(x => x.NumeroDeProducto != RegistroPrev.NumeroDeProducto))
                {
                    Filtro = String.Format("NumeroDeProductoPadre == {0} && NumeroDeProductoHijo == {1}", RegistroPrev.NumeroDeProducto, item.NumeroDeProducto);
                    var Encontrado = ClsConfiguraPaquetes.getList(Filtro);
                    if (Encontrado.Count() != 0)
                    {
                        Paquete.AddRange(Encontrado);
                    }
                    else
                    {
                        Filtro = String.Format("NumeroDeProductoPadre == {0} && NumeroDeProductoHijo == {1}", item.NumeroDeProducto, RegistroPrev.NumeroDeProducto);
                        Encontrado = ClsConfiguraPaquetes.getList(Filtro);
                        if(Encontrado.Count() != 0)
                        {
                            EsHijo = true;
                            Paquete.AddRange(Encontrado);
                        }
                    }
                }
            }
            if(Paquete.Count() != 0)
            {
                Registro.Add(new ClsVentas() {
                    NumeroDeProducto = RegistroPrev.NumeroDeProducto,
                    CodigoDeBarras = RegistroPrev.CodigoDeBarras,
                    NumeroDeTipoDeProducto = RegistroPrev.NumeroDeTipoDeProducto,
                    NumeroDeMarca = RegistroPrev.NumeroDeMarca,
                    CantidadDeProducto = 0,
                    ImporteDeProducto = 0,
                    PrecioUnitario = 0,
                    NombreDeProducto = RegistroPrev.NombreDeProducto
                });
                short Cantidad = RegistroPrev.CantidadDeProducto;
                foreach(var pac in Paquete)
                {
                    var Item = RegistrosPrev.Find(x => x.NumeroDeProducto == pac.NumeroDeProductoHijo);
                    if (EsHijo)
                    {
                        Item = RegistrosPrev.Find(x => x.NumeroDeProducto == pac.NumeroDeProductoPadre);
                    }
                    if (Cantidad > Item.CantidadDeProducto)
                    {
                        Cantidad = (short)(Cantidad - Item.CantidadDeProducto);
                        Registro.Add(new ClsVentas() {
                            NumeroDeProducto = Item.NumeroDeProducto,
                            CodigoDeBarras = Item.CodigoDeBarras,
                            NumeroDeTipoDeProducto = Item.NumeroDeTipoDeProducto,
                            NumeroDeMarca = Item.NumeroDeMarca,
                            CantidadDeProducto = 0,
                            ImporteDeProducto = 0,
                            PrecioUnitario = 0,
                            NombreDeProducto = Item.NombreDeProducto
                        });
                    }
                    else if (Cantidad < Item.CantidadDeProducto)
                    {
                        Registro.Add(new ClsVentas()
                        {
                            NumeroDeProducto = Item.NumeroDeProducto,
                            CodigoDeBarras = Item.CodigoDeBarras,
                            NumeroDeTipoDeProducto = Item.NumeroDeTipoDeProducto,
                            NumeroDeMarca = Item.NumeroDeMarca,
                            CantidadDeProducto = (short)(Item.CantidadDeProducto - Cantidad),
                            ImporteDeProducto = 0,
                            PrecioUnitario = 0,
                            NombreDeProducto = Item.NombreDeProducto
                        });
                        Cantidad = 0;
                    }
                    else
                    {
                        Registro.Add(new ClsVentas()
                        {
                            NumeroDeProducto = Item.NumeroDeProducto,
                            CodigoDeBarras = Item.CodigoDeBarras,
                            NumeroDeTipoDeProducto = Item.NumeroDeTipoDeProducto,
                            NumeroDeMarca = Item.NumeroDeMarca,
                            CantidadDeProducto = 0,
                            ImporteDeProducto = 0,
                            PrecioUnitario = 0,
                            NombreDeProducto = Item.NombreDeProducto
                        });
                        Cantidad = 0;
                    } 
                }
            }
            else
            {
                Registro.Add(new ClsVentas()
                {
                    NumeroDeProducto = RegistroPrev.NumeroDeProducto,
                    CodigoDeBarras = RegistroPrev.CodigoDeBarras,
                    NumeroDeTipoDeProducto = RegistroPrev.NumeroDeTipoDeProducto,
                    NumeroDeMarca = RegistroPrev.NumeroDeMarca,
                    CantidadDeProducto = 0,
                    ImporteDeProducto = 0,
                    PrecioUnitario = 0,
                    NombreDeProducto = RegistroPrev.NombreDeProducto
                });
    
            }
            List<ClsVentas> RegistroTemp = new List<ClsVentas>();
            foreach(var reg in Registro)
            {
                if (RegistrosPrev.Exists(x => x.NumeroDeTipoDeProducto == reg.NumeroDeTipoDeProducto && x.NumeroDeMarca == reg.NumeroDeMarca && x.NumeroDeProducto != reg.NumeroDeProducto))
                {
                    List<ClsVentas> RegistroImplicados = RegistrosPrev.FindAll(x => x.NumeroDeTipoDeProducto == reg.NumeroDeTipoDeProducto && x.NumeroDeMarca == reg.NumeroDeMarca && x.NumeroDeProducto != reg.NumeroDeProducto);
                    short CantidadMayore = (short)(RegistroImplicados.Sum(x => x.CantidadDeProducto) + reg.CantidadDeProducto);
                    List<ClsConfiguraMayoreos> Mayoreos = ClsConfiguraMayoreos.getList(String.Format("NumeroDeProducto == {0} && CodigoDeBarras == \"{1}\" && CantidadMinima <= {2} && CantidadMaxima >= {2}", reg.NumeroDeProducto, reg.CodigoDeBarras, CantidadMayore));
                    if (Mayoreos.Count() != 0)
                    {
                        if (Mayoreos.FirstOrDefault().PrecioDeMayoreo != RegistroImplicados.FirstOrDefault().PrecioUnitario)
                        {
                            foreach (var item in RegistrosPrev.FindAll(x => x.NumeroDeTipoDeProducto == reg.NumeroDeTipoDeProducto && x.NumeroDeMarca == reg.NumeroDeMarca && x.NumeroDeProducto != reg.NumeroDeProducto))
                            {
                                if (!Registro.Exists(x => x.NumeroDeProducto == item.NumeroDeProducto))
                                {
                                    RegistroTemp.Add(new ClsVentas()
                                    {
                                        NumeroDeProducto = item.NumeroDeProducto,
                                        CodigoDeBarras = item.CodigoDeBarras,
                                        NumeroDeTipoDeProducto = item.NumeroDeTipoDeProducto,
                                        NumeroDeMarca = item.NumeroDeMarca,
                                        CantidadDeProducto = item.CantidadDeProducto,
                                        ImporteDeProducto = Math.Round(item.CantidadDeProducto * Mayoreos.FirstOrDefault().PrecioDeMayoreo, 2),
                                        PrecioUnitario = Mayoreos.FirstOrDefault().PrecioDeMayoreo,
                                        NombreDeProducto = item.NombreDeProducto
                                    });
                                }
                                else
                                {
                                    var previo = Registro.Find(x => x.NumeroDeProducto == item.NumeroDeProducto);
                                    if (previo.CantidadDeProducto != 0)
                                    {
                                        previo.PrecioUnitario = Mayoreos.FirstOrDefault().PrecioDeMayoreo;
                                        previo.ImporteDeProducto = Math.Round(reg.CantidadDeProducto * reg.PrecioUnitario, 2);
                                    }
                                }
                            }
                            if (reg.CantidadDeProducto != 0)
                            {
                                reg.PrecioUnitario = Mayoreos.FirstOrDefault().PrecioDeMayoreo;
                                reg.ImporteDeProducto = Math.Round(reg.CantidadDeProducto * reg.PrecioUnitario,2);
                            }
                        }

                    }
                    else
                    {
                        foreach (var item in RegistrosPrev.FindAll(x => x.NumeroDeTipoDeProducto == reg.NumeroDeTipoDeProducto && x.NumeroDeMarca == reg.NumeroDeMarca && x.NumeroDeProducto != reg.NumeroDeProducto))
                        {
                            if (!Registro.Exists(x => x.NumeroDeProducto == item.NumeroDeProducto))
                            {
                                ClsProductos producto = ClsProductos.getList("NumeroDeProducto == " + item.NumeroDeProducto + " && CodigoDeBarras == \"" + item.CodigoDeBarras + "\"").FirstOrDefault();
                                if (producto.PrecioUnitario != item.PrecioUnitario)
                                {
                                    RegistroTemp.Add(new ClsVentas()
                                    {
                                        NumeroDeProducto = item.NumeroDeProducto,
                                        CodigoDeBarras = item.CodigoDeBarras,
                                        NumeroDeTipoDeProducto = item.NumeroDeTipoDeProducto,
                                        NumeroDeMarca = item.NumeroDeMarca,
                                        CantidadDeProducto = item.CantidadDeProducto,
                                        ImporteDeProducto = Math.Round(item.CantidadDeProducto * producto.PrecioUnitario, 2),
                                        PrecioUnitario = producto.PrecioUnitario,
                                        NombreDeProducto = item.NombreDeProducto
                                    });
                                }
                            }
                            else
                            {
                                var previo = Registro.Find(x => x.NumeroDeProducto == item.NumeroDeProducto);
                                if (previo.CantidadDeProducto != 0)
                                {
                                    ClsProductos producto = ClsProductos.getList("NumeroDeProducto == " + previo.NumeroDeProducto + " && CodigoDeBarras == \"" + previo.CodigoDeBarras + "\"").FirstOrDefault();
                                    previo.PrecioUnitario = producto.PrecioUnitario;
                                    previo.ImporteDeProducto = Math.Round(previo.CantidadDeProducto * previo.PrecioUnitario, 2);
                                }
                            }
                        }
                        if (reg.CantidadDeProducto != 0)
                        {
                            ClsProductos producto = ClsProductos.getList("NumeroDeProducto == " + reg.NumeroDeProducto + " && CodigoDeBarras == \"" + reg.CodigoDeBarras + "\"").FirstOrDefault();
                            reg.PrecioUnitario = producto.PrecioUnitario;
                            reg.ImporteDeProducto = Math.Round(reg.CantidadDeProducto * reg.PrecioUnitario, 2);
                        }
                    }
                }
            }
            Registro.AddRange(RegistroTemp);
            return Json(new { Registro }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AutoCompleteProducto(String Prefix, short NumeroDeTipoDeProducto = 0, short NumeroDeMarca = 0)
        {
            return Json(ClsAdicional.ClsCargaCombo.AutoCompleteProducto(Prefix, NumeroDeTipoDeProducto, NumeroDeMarca), JsonRequestBehavior.AllowGet);
        }
        public void CargaCombos()
        {
            ViewBag.NumeroDeTipoDeProducto = ClsAdicional.ClsCargaCombo.CargaComboTipoDeProducto(String.Empty);
            ViewBag.NumeroDeMarca = ClsAdicional.ClsCargaCombo.CargaComboMarcaPorTipo(0, String.Empty);
        }
        public JsonResult ObtenMarcaPorTipo(short TipoDeProducto, short Marca = 0)
        {
            return Json(ClsAdicional.ClsCargaCombo.CargaComboMarcaPorTipo(TipoDeProducto, Marca.ToString()), JsonRequestBehavior.AllowGet);
        }
        public ActionResult DevolucionCambios()
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.BAJA) && !ValidaFuncionalidad(NumeroDePantalla,(byte)ClsEnumerables.Funcionalidades.EDITA))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public JsonResult BuscarVentaPorFolio(short FolioDeVenta)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            try
            {
                List<ClsVentas> VentaDetalle = ClsVentas.getList(String.Format("FolioDeOperacion == {0}", FolioDeVenta)).ToList();
                return Json(new { Resultado, Registro = VentaDetalle }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Resultado.Resultado = false;
                Resultado.Mensaje = "Ocurrio un error al realizar la busqueda de venta por el folio: " + FolioDeVenta.ToString();
            }
            return Json(new { Resultado, Registro = new List<Object>() }, JsonRequestBehavior.AllowGet);
        }
    }
}
