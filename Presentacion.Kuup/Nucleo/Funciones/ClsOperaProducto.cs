using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Funciones.Kuup.Adicionales;
using Funciones.Kuup.CodigoDeBarras;
using Mod.Entity;
using Negocio.Kuup.Clases;
using Presentacion.Kuup.Models;

namespace Presentacion.Kuup.Nucleo.Funciones
{
    public class ClsOperaProducto : Models.ProductoModel
    {
        public bool ManejaProveedor
        {
            get { return validaProveedor(); }
        }
        private bool validaProveedor()
        {
            using (DBKuupEntities db = new DBKuupEntities())
            {
                return (from q in db.Parametro where q.PAR_NOM_PARAMETRO == "ManejaProveedor" select q.PAR_VALOR_PARAMETRO).FirstOrDefault() == "SI";
            }
        }

        public ClsAdicional.ClsResultado Insert(ProductoModel Producto, bool GeneraCodigoDeBarras,List<MayoreoProducto> Mayoreo)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            using (DBKuupEntities db = new DBKuupEntities())
            {
                ClsSequence Sequence = new ClsSequence(db.Database);
                Producto.NumeroDeProducto = Sequence.SQ_Producto();
                Producto.CodigoDeBarras = (GeneraCodigoDeBarras ? MoCodigoDeBarras.ArmaNumeroDeBarras(Producto.NumeroDeProducto, "Kuup") : Producto.CodigoDeBarras);
                if (!Producto.Existe())
                {
                    if (Producto.Insert())
                    {
                        if (GeneraCodigoDeBarras)
                        {
                            MoCodigoDeBarras ResultadoCodigo = (new MoCodigoDeBarras()).GeneraCodigoDeBarras(Producto.NumeroDeProducto, "Kuup", Producto.CodigoDeBarras);
                            Resultado = ResultadoCodigo.Resultado;
                            if (!Resultado.Resultado)
                            {
                                Producto.Delete();
                            }
                        }
                        if (Mayoreo.Count() > 0)
                        {
                            if(Mayoreo.Exists(x => x.NombreDeProducto == Producto.NombreDeProducto))
                            {
                                if (Mayoreo.Find(x => x.NombreDeProducto == Producto.NombreDeProducto).CveAplicaMayoreo == 1)
                                {
                                    ClsConfiguraMayoreos configuraMayoreos = new ClsConfiguraMayoreos();
                                    configuraMayoreos.NumeroDeMayoreo = 1;
                                    configuraMayoreos.NumeroDeProducto = Producto.NumeroDeProducto;
                                    configuraMayoreos.CodigoDeBarras = Producto.CodigoDeBarras;
                                    configuraMayoreos.CveDeAplicaPaquetes = 1;
                                    configuraMayoreos.CantidadMinima = Mayoreo.Find(x => x.NombreDeProducto == Producto.NombreDeProducto).CantidadMinimaMayoreo;
                                    configuraMayoreos.CantidadMaxima = null;
                                    configuraMayoreos.PrecioDeMayoreo = Mayoreo.Find(x => x.NombreDeProducto == Producto.NombreDeProducto).PrecioMayoreo;
                                    configuraMayoreos.Insert();
                                }
                            }
                        }
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
            return Resultado;
        }
        public ClsAdicional.ClsResultado RegistraMayoreo(short NumeroDeProducto, String CodigoDeBarras, List<ClsConfiguraMayoreos> configuraMayoreos)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            if (configuraMayoreos.Count() > 0)
            {
                using(DBKuupEntities db = new DBKuupEntities())
                {
                    using(var Transaccion = db.Database.BeginTransaction())
                    {
                        try
                        {
                            List<ClsConfiguraMayoreos> Previos = (from q in ClsConfiguraMayoreos.getList() where q.NumeroDeProducto == NumeroDeProducto && q.CodigoDeBarras == CodigoDeBarras select q).ToList();
                            if (Previos.Count() > 0)
                            {
                                foreach(var p in Previos.OrderBy(x => x.NumeroDeMayoreo))
                                {
                                    if (!p.Delete())
                                    {
                                        Resultado.Resultado = false;
                                        Resultado.Mensaje = "Ocurrio un error al realizar la carga de Precios de Mayoreo";
                                        break;
                                    }
                                }
                            }
                            if (Resultado.Resultado)
                            {
                                foreach (var Mayoreo in configuraMayoreos.OrderBy(x => x.NumeroDeMayoreo))
                                {
                                    Mayoreo.db = db;
                                    Mayoreo.NumeroDeProducto = NumeroDeProducto;
                                    Mayoreo.CodigoDeBarras = CodigoDeBarras;
                                    Mayoreo.CveDeAplicaPaquetes = 1;
                                    if (!Mayoreo.Insert())
                                    {
                                        Resultado.Resultado = false;
                                        Resultado.Mensaje = "No fue posible realizar la carga de precios de mayoreo";
                                        break;
                                    }
                                }
                            }
                            if (Resultado.Resultado)
                            {
                                Transaccion.Commit();
                            }
                            else
                            {
                                Transaccion.Rollback();
                            }
                        }
                        catch(Exception e)
                        {
                            Transaccion.Rollback();
                            Resultado.Resultado = false;
                            Resultado.Mensaje = Recursos.Textos.Bitacora_TextoTryCatchGenerico;
                            ClsBitacora.GeneraBitacora(1, 1, "RegistraMayoreo", String.Format(Recursos.Textos.Bitacora_TextoDeError, e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                        }
                    }
                }
            }
            return Resultado;
        }
        public List<String> ListaDeDatosAImportar()
        {
            if (ManejaProveedor)
            {
                return new List<String>() {
                Recursos.Textos.Producto_CodigoDeBarras,
                Recursos.Textos.Producto_NombreDeProducto,
                Recursos.Textos.Producto_Descripcion,
                Recursos.Textos.Producto_CantidadDeProductoTotal,
                Recursos.Textos.Producto_PrecioUnitario,
                Recursos.Textos.Producto_CveAviso,
                Recursos.Textos.Producto_CveCorreoSurtido,
                Recursos.Textos.Producto_NumeroDeProveedor,
                Recursos.Textos.Producto_CantidadMinima,
                Recursos.Textos.Producto_CveAplicaMayoreo,
                Recursos.Textos.Producto_CantidadMinimaMayoreo,
                Recursos.Textos.Producto_PrecioMayoreo
            };
            }
            return new List<String>() {
                Recursos.Textos.Producto_CodigoDeBarras,
                Recursos.Textos.Producto_NombreDeProducto,
                Recursos.Textos.Producto_Descripcion,
                Recursos.Textos.Producto_CantidadDeProductoTotal,
                Recursos.Textos.Producto_PrecioUnitario,
                Recursos.Textos.Producto_CveAviso,
                Recursos.Textos.Producto_CveCorreoSurtido,
                Recursos.Textos.Producto_CantidadMinima,
                Recursos.Textos.Producto_CveAplicaMayoreo,
                Recursos.Textos.Producto_CantidadMinimaMayoreo,
                Recursos.Textos.Producto_PrecioMayoreo
            };
        }
        public List<List<Object>> DatosDeEjemplo()
        {
            List<List<Object>> Lista = new List<List<object>>();
            if (ManejaProveedor)
            {
                Lista.Add(new List<object> {
                "000000",
                "Producto",
                "Alguna descripción del producto",
                "0",
                "0.0",
                "SI/NO",
                "SI/NO",
                "Nombre de proveedor",
                "0",
                "SI/NO",
                "0",
                "0.0"});
            }
            else
            {
                Lista.Add(new List<object> {
                "000000",
                "Producto",
                "Alguna descripción del producto",
                "0",
                "0.0",
                "SI/NO",
                "SI/NO",
                "0",
                "SI/NO",
                "0",
                "0.0"});
            }
            return Lista;
        }
        public ClsAdicional.ClsResultado ImportarCSVProducto(String Ruta, ref IEnumerable<Object> ListaDeProductos)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            return ClsAdicional.ClsArchivos.ImportarCSVConvierteAEntidad(Ruta, (x, y) => ValidaDatosAImportar(x, ref y), (x) => ConvierteListaDicAEntidad(x), ref ListaDeProductos);
        }
        public List<object> ConvierteListaDicAEntidad(Dictionary<String, String> Valores)
        {
            List<Object> ListaRegreso = new List<Object>();
            
            if (ManejaProveedor)
            {
                ListaRegreso.Add(new
                {
                    CodigoDeBarras = Valores["CodigoDeBarras"],
                    NombreDeProducto = Valores["NombreDeProducto"],
                    Descripcion = Valores["Descripcion"],
                    CantidadDeProductoTotal = Valores["CantidadDeProductoTotal"],
                    PrecioUnitario = Valores["PrecioUnitario"],
                    CveAviso = Valores["CveAviso"],
                    TextoAviso = Valores["TextoAviso"],
                    CveCorreoSurtido = Valores["CveCorreoSurtido"],
                    TextoCorreoSurtido = Valores["TextoCorreoSurtido"],
                    NumeroDeProveedor = Valores["NumeroDeProveedor"],
                    NombreDeProveedor = Valores["NombreDeProveedor"],
                    CantidadMinima = Valores["CantidadMinima"],
                    CveAplicaMayoreo = Valores["CveAplicaMayoreo"],
                    TextoAplicaMayoreo = Valores["TextoAplicaMayoreo"],
                    CantidadMinimaMayoreo = Valores["CantidadMinimaMayoreo"],
                    PrecioMayoreo = Valores["PrecioMayoreo"],
                    Observaciones = Valores["Observaciones"]
                });
            }
            else
            {
                ListaRegreso.Add(new
                {
                    CodigoDeBarras = Valores["CodigoDeBarras"],
                    NombreDeProducto = Valores["NombreDeProducto"],
                    Descripcion = Valores["Descripcion"],
                    CantidadDeProductoTotal = Valores["CantidadDeProductoTotal"],
                    PrecioUnitario = Valores["PrecioUnitario"],
                    CveAviso = Valores["CveAviso"],
                    TextoAviso = Valores["TextoAviso"],
                    CveCorreoSurtido = Valores["CveCorreoSurtido"],
                    TextoCorreoSurtido = Valores["TextoCorreoSurtido"],
                    CantidadMinima = Valores["CantidadMinima"],
                    CveAplicaMayoreo = Valores["CveAplicaMayoreo"],
                    TextoAplicaMayoreo = Valores["TextoAplicaMayoreo"],
                    CantidadMinimaMayoreo = Valores["CantidadMinimaMayoreo"],
                    PrecioMayoreo = Valores["PrecioMayoreo"],
                    Observaciones = Valores["Observaciones"]
                });
            }
            return ListaRegreso;
        }
        private ClsAdicional.ClsResultado ValidaDatosAImportar(List<List<String>> Info, ref List<Dictionary<String, String>> InfoDiccionarioRef)
        {
            List<ClsAdicional.ClsResultado> Respuesta = new List<ClsAdicional.ClsResultado>();
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            Dictionary<String, String> Registros = new Dictionary<String, String>();
            List<String> Mensajes = new List<String>();
            List<ClsProductos> NombresYCodigoDeBarras = new List<ClsProductos>();
            int row = 1, col = 0;
            String Campo = "";
            foreach (var reg in Info)
            {
                row++;
                if (reg.Count >= 11)
                {
                    Registros = new Dictionary<String, String>();
                    Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
                    Respuesta.Add(Resultado);
                    Mensajes = new List<String>();
                    col = -1;

                    col++;
                    Campo = Recursos.Textos.Producto_CodigoDeBarras;
                    Resultado = ValidaCodigoDeBarras(reg[col], row, ref Registros, ref NombresYCodigoDeBarras);
                    Respuesta.Add(Resultado);
                    Mensajes.Add(Resultado.Mensaje);

                    col++;
                    Campo = Recursos.Textos.Producto_NombreDeProducto;
                    Resultado = ValidaNombreDeProducto(reg[col], row, ref Registros, ref NombresYCodigoDeBarras);
                    Respuesta.Add(Resultado);
                    Mensajes.Add(Resultado.Mensaje);

                    col++;
                    Campo = Recursos.Textos.Producto_Descripcion;
                    Resultado = ClsAdicional.ClsValida.ValidaCampoStringCapacidad("Descripcion", Campo, false, 500, reg[col], row, ref Registros);
                    Respuesta.Add(Resultado);
                    Mensajes.Add(Resultado.Mensaje);

                    col++;
                    Campo = Recursos.Textos.Producto_CantidadDeProductoTotal;
                    Resultado = ClsAdicional.ClsValida.ValidaCampoNumericoYCapacidad("CantidadDeProductoTotal", Campo, true, 16, 0, reg[col], typeof(short), row, ref Registros);
                    Respuesta.Add(Resultado);
                    Mensajes.Add(Resultado.Mensaje);

                    col++;
                    Campo = Recursos.Textos.Producto_PrecioUnitario;
                    Resultado = ClsAdicional.ClsValida.ValidaCampoNumericoYCapacidad("PrecioUnitario", Campo, true, 16, 2, reg[col], typeof(decimal), row, ref Registros);
                    Respuesta.Add(Resultado);
                    Mensajes.Add(Resultado.Mensaje);

                    col++;
                    Campo = Recursos.Textos.Producto_CveAviso;
                    String CveAviso = reg[col];
                    Resultado = ClsAdicional.ClsValida.ValidaClave("TextoAviso", "CveAviso", Campo, 4, reg[col], row, ref Registros);
                    Respuesta.Add(Resultado);
                    Mensajes.Add(Resultado.Mensaje);

                    col++;
                    Campo = Recursos.Textos.Producto_CveCorreoSurtido;
                    Resultado = ClsAdicional.ClsValida.ValidaClave("TextoCorreoSurtido", "CveCorreoSurtido", Campo, 4, reg[col], row, ref Registros);
                    Respuesta.Add(Resultado);
                    Mensajes.Add(Resultado.Mensaje);

                    if (ManejaProveedor)
                    {
                        col++;
                        Campo = Recursos.Textos.Producto_NumeroDeProveedor;
                        //Resultado = ValidaNumeroDeProveedor(reg[col], row, ref Registros);
                        Respuesta.Add(Resultado);
                        Mensajes.Add(Resultado.Mensaje);
                    }

                    col++;
                    Campo = Recursos.Textos.Producto_CantidadMinima;
                    Resultado = ClsAdicional.ClsValida.ValidaCampoNumericoYCapacidad("CantidadMinima", Campo, (CveAviso == "SI"), 16, 0, reg[col], typeof(short), row, ref Registros);
                    Respuesta.Add(Resultado);
                    Mensajes.Add(Resultado.Mensaje);

                    col++;
                    Campo = Recursos.Textos.Producto_CveAplicaMayoreo;
                    String CveAplicaMayoreo = reg[col];
                    Resultado = ClsAdicional.ClsValida.ValidaClave("TextoAplicaMayoreo", "CveAplicaMayoreo", Campo, 4, reg[col], row, ref Registros);
                    Respuesta.Add(Resultado);
                    Mensajes.Add(Resultado.Mensaje);

                    col++;
                    Campo = Recursos.Textos.Producto_CantidadMinimaMayoreo;
                    Resultado = ClsAdicional.ClsValida.ValidaCampoNumericoYCapacidad("CantidadMinimaMayoreo", Campo, (CveAplicaMayoreo == "SI"), 16, 0, reg[col], typeof(short), row, ref Registros);
                    Respuesta.Add(Resultado);
                    Mensajes.Add(Resultado.Mensaje);

                    col++;
                    Campo = Recursos.Textos.Producto_PrecioMayoreo;
                    Resultado = ClsAdicional.ClsValida.ValidaCampoNumericoYCapacidad("PrecioMayoreo", Campo, (CveAplicaMayoreo == "SI"), 16, 2, reg[col], typeof(decimal), row, ref Registros);
                    Respuesta.Add(Resultado);
                    Mensajes.Add(Resultado.Mensaje);

                    Mensajes.RemoveAll(x => x == String.Empty);
                    String inicio = String.Empty;
                    if (Mensajes.Count > 0)
                    {
                        inicio = "* ";
                    }
                    Registros.Add("Observaciones", inicio + String.Join("<br>* ", Mensajes));
                }
                InfoDiccionarioRef.Add(Registros);
            }
            return ClsAdicional.ClsResultado.ValidaLista(Respuesta);
        }
        private ClsAdicional.ClsResultado ValidaCodigoDeBarras(String CodigoDeBarras, int row, ref Dictionary<String, String> Registros, ref List<ClsProductos> NombresYCodigoDeBarras)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            if (!String.IsNullOrEmpty(CodigoDeBarras))
            {
                if ((from q in ClsProductos.getList() where q.CodigoDeBarras.Trim() == CodigoDeBarras.Trim() select q.CodigoDeBarras).Count() == 0)
                {
                    if ((from q in NombresYCodigoDeBarras where q.CodigoDeBarras == CodigoDeBarras select q.CodigoDeBarras).Count() == 0)
                    {
                        Registros.Add("CodigoDeBarras", CodigoDeBarras);
                        NombresYCodigoDeBarras.Add(new ClsProductos() { CodigoDeBarras = CodigoDeBarras });
                    }
                    else
                    {
                        Resultado.Resultado = false;
                        Resultado.Mensaje = "El campo codigo de barras a registrar se encuetra en un registro previo dentro del archivo";
                        Registros.Add("CodigoDeBarras", CodigoDeBarras);
                    }
                }
                else
                {
                    Resultado.Resultado = false;
                    Resultado.Mensaje = "El campo codigo de barras a registrar se encuetra en un registro previo dentro del sistema";
                    Registros.Add("CodigoDeBarras", CodigoDeBarras);
                }
            }
            else
            {
                Resultado.Resultado = true;
                Resultado.Mensaje = "El campo codigo de barras sera generado por el Sistema";
                Registros.Add("CodigoDeBarras", String.Empty);
            }
            return Resultado;
        }
        private ClsAdicional.ClsResultado ValidaNombreDeProducto(String NombreDeProducto, int row, ref Dictionary<String, String> Registros, ref List<ClsProductos> NombresYCodigoDeBarras)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            if (!String.IsNullOrEmpty(NombreDeProducto))
            {
                if ((from q in ClsProductos.getList() where q.NombreDeProducto.Trim() == NombreDeProducto.Trim() select q.NombreDeProducto).Count() == 0)
                {
                    if ((from q in NombresYCodigoDeBarras where q.NombreDeProducto == NombreDeProducto select q.NombreDeProducto).Count() == 0)
                    {
                        Registros.Add("NombreDeProducto", NombreDeProducto);
                        String CodigoDeBarras = Registros["CodigoDeBarras"].ToString();
                        if (NombresYCodigoDeBarras.Count() == 0)
                        {
                            NombresYCodigoDeBarras.Add(new ClsProductos() { CodigoDeBarras = CodigoDeBarras, NombreDeProducto = NombreDeProducto });
                        }
                        else
                        {
                            NombresYCodigoDeBarras.Find(x => x.CodigoDeBarras == CodigoDeBarras).NombreDeProducto = NombreDeProducto;
                        }
                    }
                    else
                    {
                        Resultado.Resultado = false;
                        Resultado.Mensaje = "El campo Nombre de Producto a registrar se encuetra en un registro previo dentro del archivo";
                        Registros.Add("NombreDeProducto", NombreDeProducto);
                    }
                }
                else
                {
                    Resultado.Resultado = false;
                    Resultado.Mensaje = "El campo Nombre de Producto a registrar se encuetra en un registro previo dentro del sistema";
                    Registros.Add("NombreDeProducto", NombreDeProducto);
                }
            }
            else
            {
                Resultado.Resultado = false;
                Resultado.Mensaje = "El campo codigo de Nombre de Producto es requerido";
                Registros.Add("NombreDeProducto", String.Empty);
            }
            return Resultado;
        }

    }
}