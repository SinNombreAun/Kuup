using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography;
using Funciones.Kuup.Adicionales;
using Funciones.Kuup.CodigoDeBarras;
using Funciones.Kuup.Tecket;
using Mod.Entity;
using Negocio.Kuup.Clases;
using Presentacion.Kuup.Models;
using Presentacion.Kuup.Nucleo.Motores;

namespace Presentacion.Kuup.Nucleo.Funciones
{
    public class ClsOperaVentaTotal : Models.VentaModel
    {
        public ClsAdicional.ClsResultado RegistroDeVenta(decimal ImporteEntregado, decimal ImporteCambio, String ObjetoVenta)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, "Registro de Venta Correcto");
            List<Object> AvisaCantidad = new List<Object>();
            if (!String.IsNullOrEmpty(ObjetoVenta))
            {
                List<ClsVentas> RegistrosDeVentas = ClsAdicional.Deserializar<List<ClsVentas>>(ObjetoVenta);
                if (RegistrosDeVentas == null)
                {
                    RegistrosDeVentas = new List<ClsVentas>();
                }
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    using (var Transaccion = db.Database.BeginTransaction())
                    {
                        try
                        {
                            ClsSequence Sequence = new ClsSequence(db.Database);
                            ClsVentasTotales VentasTotales = new ClsVentasTotales();
                            VentasTotales.db = db;
                            VentasTotales.FolioDeOperacion = Sequence.SQ_FolioVenta();
                            VentasTotales.FechaDeOperacion = DateTime.Now;
                            VentasTotales.NumeroDeUsuario = MoSesion.NumeroDeUsuario;
                            VentasTotales.NombreDeCliente = String.Empty;
                            VentasTotales.CveAplicaDescuento = 2;
                            VentasTotales.CveDeEstatus = (byte)ClsEnumerables.CveDeEstatusVentas.VENDIDA;
                            VentasTotales.ImporteBruto = (from q in RegistrosDeVentas select q.ImporteDeProducto).Sum();
                            VentasTotales.ImporteNeto = VentasTotales.ImporteBruto;
                            VentasTotales.ImporteEntregado = ImporteEntregado;
                            VentasTotales.ImporteCambio = ImporteCambio;
                            if (VentasTotales.Insert())
                            {
                                var Productos = ClsProductos.getList().Where(x => RegistrosDeVentas.Exists(y => y.NumeroDeProducto == x.NumeroDeProducto)).ToList();
                                foreach (var Ventas in RegistrosDeVentas)
                                {
                                    Ventas.db = db;
                                    Ventas.FolioDeOperacion = VentasTotales.FolioDeOperacion;
                                    if (Ventas.Insert())
                                    {
                                        var Producto = (from q in Productos where q.NumeroDeProducto == Ventas.NumeroDeProducto select q).FirstOrDefault();
                                        if (Producto != null)
                                        {
                                            var Cantidad = Producto.CantidadDeProductoTotal - Ventas.CantidadDeProducto;
                                            if (Cantidad >= 0)
                                            {
                                                Producto.db =db;
                                                Producto.CantidadDeProductoTotal = (short)(Producto.CantidadDeProductoTotal - Ventas.CantidadDeProducto);
                                                if (!Producto.Update())
                                                {
                                                    Resultado.Resultado = false;
                                                    Resultado.Mensaje = "No fue posible actualizar los titulos disponibles";
                                                    break;
                                                }
                                                if (Producto.CveAviso == 1)
                                                {
                                                    if (Cantidad <= Producto.CantidadMinima)
                                                    {
                                                        AvisaCantidad.Add(String.Format("El producto {0} esta proximo a terminarce Cantidad Actual {1}", Producto.NombreDeProducto, Producto.CantidadDeProductoTotal));
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Resultado.Resultado = false;
                                                AvisaCantidad.Add(String.Format("El producto {0} no cuenta con la cantidad a vender Cantidad Actual: {1} Cantidad a Vender: {2}", Producto.NombreDeProducto, Producto.CantidadDeProductoTotal, Ventas.CantidadDeProducto));
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Resultado.Resultado = false;
                                        Resultado.Mensaje = "No fue posible cargar el detalle de la Venta";
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                Resultado.Resultado = false;
                                Resultado.Mensaje = "No fue posible realizar la Venta";
                            }
                            String Mensaje = String.Empty;
                            if (AvisaCantidad.Count() != 0)
                            {
                                Mensaje = String.Join("</ br>", AvisaCantidad);
                            }
                            if (Resultado.Resultado)
                            {
                                Transaccion.Commit();
                                ClsAdicional.ClsResultado RTicket = GeneraTicket(VentasTotales.FolioDeOperacion);
                                Resultado.Adicional = new { MensajeAviso = Mensaje, Ticket = RTicket.Adicional };
                            }
                            else
                            {
                                Object Ticket = null;
                                Transaccion.Rollback();
                                Resultado.Adicional = new { MensajeAviso = Mensaje, Ticket };
                            }
                        }
                        catch (Exception e)
                        {
                            Object Ticket = null;
                            Transaccion.Rollback();
                            Resultado.Adicional = new { MensajeAviso = String.Empty, Ticket };
                            Resultado.Resultado = false;
                            Resultado.Mensaje = Recursos.Textos.Bitacora_TextoTryCatchGenerico;
                            ClsBitacora.GeneraBitacora(1, 1, "RegistroDeVenta", String.Format(Recursos.Textos.Bitacora_TextoDeError, e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                        }
                    }
                }
            }
            return Resultado;
        }
        public ClsAdicional.ClsResultado GeneraTicket(short FolioDeVenta)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            try
            {
                List<ClsParametros> ParametrosTicket = (from q in ClsParametros.getList() where q.CveTipo == 2 select q).ToList();
                var lista = (from q in ClsVentasTotales.getList() join v in ClsVentas.getList() on q.FolioDeOperacion equals v.FolioDeOperacion where q.FolioDeOperacion == FolioDeVenta select new { q, v }).ToList();

                List<Object> Producto = new List<Object>();
                foreach (var vproducto in lista)
                {
                    Producto.Add(new { vproducto.v.NombreDeProducto,vproducto.v.PrecioUnitario,vproducto.v.CantidadDeProducto,vproducto.v.ImporteDeProducto});
                }

                Object TicketObj = new { 
                    Empresa = (from q in ParametrosTicket where q.NombreDeParametro == "Empresa" select q.ValorDeParametro).FirstOrDefault(),
                    Dir = (from q in ParametrosTicket where q.NombreDeParametro == "Direccion" select q.ValorDeParametro).FirstOrDefault(),
                    Tel = (from q in ParametrosTicket where q.NombreDeParametro == "Telefono" select q.ValorDeParametro).FirstOrDefault(),
                    Atiende = String.Format("{0} {1} {2}", MoSesion.NombreDePersona, MoSesion.ApellidoPaterno, MoSesion.ApellidoMaterno).ToUpper().Trim(),
                    Fecha = String.Format("Fecha: {0} Hora: {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString()),
                    ImporteTotal = lista.FirstOrDefault().q.ImporteNeto,
                    ImporteEntregado = lista.FirstOrDefault().q.ImporteEntregado,
                    Cambio = lista.FirstOrDefault().q.ImporteCambio,
                    TextoFinal = (from q in ParametrosTicket where q.NombreDeParametro == "TextoFinal" select q.ValorDeParametro).FirstOrDefault(),
                    ListaProducto = Producto
                };
                //ClsTicket Ticket = new ClsTicket();
                //Ticket.TextoCentro(String.Format("Empresa {0}", (from q in ParametrosTicket where q.NombreDeParametro == "Empresa" select q.ValorDeParametro).FirstOrDefault()));
                //ClsTicket.LineasCaracter("*");
                //Ticket.TextoCentro(String.Format("Dir: {0}", (from q in ParametrosTicket where q.NombreDeParametro == "Direccion" select q.ValorDeParametro).FirstOrDefault()));
                //Ticket.TextoCentro(String.Format("Tel: {0}", (from q in ParametrosTicket where q.NombreDeParametro == "Telefono" select q.ValorDeParametro).FirstOrDefault()));
                //Ticket.TextoIzquierda(String.Empty);
                //Ticket.TextoCentro("Ticket de Venta");
                //Ticket.TextoIzquierda(String.Format("Fecha: {0} Hora: {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString()));
                //Ticket.TextoIzquierda("Le Atendio: " + String.Format("{0} {1} {2}", MoSesion.NombreDePersona, MoSesion.ApellidoPaterno, MoSesion.ApellidoMaterno).ToUpper());
                //Ticket.TextoIzquierda(String.Empty);
                //ClsTicket.LineasCaracter("*");
                //ClsTicket.EncabezadoVenta();
                //ClsTicket.LineasCaracter("*");
                //foreach (var vproducto in lista)
                //{
                //    Ticket.AgregaArticulo(vproducto.v.NombreDeProducto, vproducto.v.PrecioUnitario, vproducto.v.CantidadDeProducto, vproducto.v.ImporteDeProducto);
                //}
                //ClsTicket.LineasCaracter("*");
                //Ticket.AgregaTotales("Total:", lista.FirstOrDefault().q.ImporteNeto);
                //Ticket.TextoIzquierda(String.Empty);
                //Ticket.AgregaTotales("Efectivo Entregado:", lista.FirstOrDefault().q.ImporteEntregado);
                //Ticket.AgregaTotales("Efectivo Devuelto:", lista.FirstOrDefault().q.ImporteCambio);
                //Ticket.TextoIzquierda(String.Empty);
                //ClsTicket.LineasCaracter("*");
                //Ticket.TextoCentro((from q in ParametrosTicket where q.NombreDeParametro == "TextoFinal" select q.ValorDeParametro).FirstOrDefault());
                //ClsTicket.LineasCaracter("*");

                //Ticket.NombreDeTicket = String.Format("Ticket{0}{1}.txt", lista.FirstOrDefault().q.FolioDeOperacion, DateTime.Now.ToString("yyyyMMdd"));
                //Ticket.RutaDeTicket = System.Web.HttpContext.Current.Server.MapPath(ClsConfiguracion.Tickets);

                //Ticket.ImprimirTiket("POS-58", ref Resultado);
                //if (!Resultado.Resultado)
                //{
                //    ClsBitacora.GeneraBitacora(1, 1, "ImprimirTiket", Resultado.Mensaje);
                //}
                Resultado.Adicional = TicketObj;// Ticket.RegresaTextoTicket();
            }
            catch(Exception e)
            {
                ClsBitacora.GeneraBitacora(1, 1, "GeneraTicket", String.Format(Recursos.Textos.Bitacora_TextoDeError, e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
            }

            return Resultado;
        }
    }
}