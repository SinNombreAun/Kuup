using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Funciones.Kuup.Adicionales;
using Funciones.Kuup.CodigoDeBarras;
using Mod.Entity;
using Negocio.Kuup.Clases;
using Presentacion.Kuup.Models;
using Presentacion.Kuup.Nucleo.Motores;

namespace Presentacion.Kuup.Nucleo.Funciones
{
    public class ClsOperaVentaTotal : Models.VentaModel
    {
        public ClsAdicional.ClsResultado RegistroDeVenta(String ObjetoVenta)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, "Registro de Venta Correcto");
            List<Object> AvisaCantidad = new List<Object>();
            try
            {
                if (!String.IsNullOrEmpty(ObjetoVenta))
                {
                    List<ClsVentas> RegistrosDeVentas = ClsAdicional.Deserializar<List<ClsVentas>>(ObjetoVenta);
                    if (RegistrosDeVentas == null)
                    {
                        RegistrosDeVentas = new List<ClsVentas>();
                    }
                    using (DBKuupEntities db = new DBKuupEntities())
                    {
                        ClsSequence Sequence = new ClsSequence(db.Database);
                        ClsVentasTotales VentasTotales = new ClsVentasTotales();
                        VentasTotales.FolioDeOperacion = Sequence.SQ_FolioVenta();
                        VentasTotales.FechaDeOperacion = DateTime.Now;
                        VentasTotales.NumeroDeUsuario = MoSesion.NumeroDeUsuario;
                        VentasTotales.NombreDeCliente = String.Empty;
                        VentasTotales.CveEstatus = (byte)ClsEnumerables.CveEstatusGeneral.ACTIVO;
                        VentasTotales.ImporteBruto = (from q in RegistrosDeVentas select q.ImporteDeProducto).Sum();
                        VentasTotales.ImporteNeto = VentasTotales.ImporteBruto;

                        if (VentasTotales.Insert())
                        {
                            foreach (var Ventas in RegistrosDeVentas)
                            {
                                Ventas.FolioDeOperacion = VentasTotales.FolioDeOperacion;
                                if (Ventas.Insert())
                                {
                                    var Producto = (from q in db.Producto where q.PRO_NUM_PRODUCTO == Ventas.NumeroDeProducto select q).FirstOrDefault();
                                    if (Producto != null)
                                    {
                                        var Cantidad = Producto.PRO_CANT_PRODUCTO_TOTAL - Ventas.CantidadDeProducto;
                                        if (Cantidad >= 0)
                                        {
                                            ClsProductos Pro = new ClsProductos(Producto);
                                            Pro.CantidadDeProductoTotal = (short)(Pro.CantidadDeProductoTotal - Ventas.CantidadDeProducto);
                                            if (!Pro.Update())
                                            {
                                                Resultado.Resultado = false;
                                                Resultado.Mensaje = "No fue posible actualizar los titulos disponibles";
                                                break;
                                            }
                                            if (Producto.PRO_CVE_AVISO == 1)
                                            {
                                                if (Cantidad <= Producto.PRO_CAT_MINIMA)
                                                {
                                                    AvisaCantidad.Add(new { NumeroDeProducto = Producto.PRO_NUM_PRODUCTO, Mensaje = String.Format("El producto {0} esta proximo a terminarce Cantidad Actual {1}", Producto.PRO_NOM_PRODUCTO, Producto.PRO_CANT_PRODUCTO_TOTAL) });
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Resultado.Resultado = false;
                                            AvisaCantidad.Add(new { NumeroDeProducto = Producto.PRO_NUM_PRODUCTO, Mensaje = String.Format("El producto {0} no cuenta con la cantidad a vender Cantidad Actual: {1} Cantidad a Vender: {2}", Producto.PRO_NOM_PRODUCTO, Producto.PRO_CANT_PRODUCTO_TOTAL, Ventas.CantidadDeProducto) });
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
                        if (AvisaCantidad.Count() != 0)
                        {
                            Resultado.Adicional = String.Join("</ br>", AvisaCantidad);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Resultado.Resultado = false;
                Resultado.Mensaje = Recursos.Textos.Bitacora_TextoTryCatchGenerico;
                ClsBitacora.GeneraBitacora(1, 1, "RegistroDeVenta", String.Format(Recursos.Textos.Bitacora_TextoDeError, e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
            }
            return Resultado;
        }
    }
}